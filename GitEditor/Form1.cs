using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GitEditor
{
    public partial class Form1 : Form
    {
        private string filepath;
        private bool isCrLf;

        private static string shaRegex(string name = null)
        {
            if (name == null || name.Length == 0)
                return "[a-z0-9]{7,}";
            else
                return "(?<" + name + ">" + shaRegex(null) + ")";
        }
        private static bool isRebase(string text)
        {
            return Regex.IsMatch(text, "^pick " + shaRegex());
        }

        public Form1()
        {
            InitializeComponent();

            box1.KeyDown += (s, e) => KeyDown(e);
        }

        public Form1(string filepath)
            : this()
        {
            string text;
            if (!File.Exists(filepath))
            {
                this.filepath = null;
                text = string.Empty;
            }
            else
            {
                this.filepath = filepath;
                text = File.ReadAllText(filepath);
            }
            isCrLf = text.Contains("\r\n");

            if (!isCrLf)
                text = text.Replace("\n", "\r\n");

            preProcess(ref text);

            this.box1.Text = text;
        }

        private void preProcess(ref string text)
        {
            if (isRebase(text))
            {
                List<string> lines = new List<string>(text.Split(new string[] { "\r\n" }, StringSplitOptions.None));
                List<RebaseAction> actions = new List<RebaseAction>();

                {
                    RebaseAction action;
                    while (RebaseAction.TryParse(lines[0], out action))
                    {
                        actions.Add(action);
                        lines.RemoveAt(0);
                    }
                }

                for (int i = 0; i < actions.Count; i++)
                {
                    var match = Regex.Match(actions[i].Message, "^fix for (?<fixing>[a-z0-9]{7,})");
                    if (match.Success)
                    {
                        var fix = actions[i];
                        string find = match.Groups["fixing"].Value;
                        for (int j = 0; j < i; j++)
                            if (actions[j].IsSha(find))
                            {
                                fix.Action = RebaseAction.Actions.fixup;
                                actions.RemoveAt(i);
                                actions.Insert(j + 1, fix);

                                break;
                            }
                    }
                }

                lines.InsertRange(0, actions.Select(x => x.ToString()));
                actions.Clear();

                text = string.Join("\r\n", lines);
                box1.Coloring = new RebaseColoring();
            }
            else
                box1.Coloring = new PatchColoring();
        }

        private void KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            string text = box1.Text;
            if (!isCrLf)
                text = text.Replace("\r\n", "\n");

            if (filepath != null)
                File.WriteAllText(filepath, text);
        }
    }
}
