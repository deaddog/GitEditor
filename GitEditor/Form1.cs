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
            if (!File.Exists(filepath))
            {
                this.filepath = null;
            }
            else
            {
                this.filepath = filepath;
                string text = File.ReadAllText(filepath);
                isCrLf = text.Contains("\r\n");

                if (!isCrLf)
                    text = text.Replace("\n", "\r\n");

                this.box1.Text = text;
            }
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
