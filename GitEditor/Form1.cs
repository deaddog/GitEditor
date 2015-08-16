using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

                for (int i = 0; i < lines.Count; i++)
                {
                    var fixMatch = Regex.Match(lines[i], "^pick " + shaRegex("fix") + " fix for " + shaRegex("fixing"));
                    if (fixMatch.Success)
                    {
                        string find = fixMatch.Groups["fixing"].Value;
                        for (int j = 0; j < i; j++)
                            if (lines[j].StartsWith("pick " + find))
                            {
                                lines.Insert(j + 1, "fixup " + lines[i].Substring(5));
                                lines.RemoveAt(i + 1);

                                break;
                            }
                    }
                }

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
