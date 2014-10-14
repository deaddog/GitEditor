using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GitEditor
{
    public partial class Form1 : Form
    {
        private string filepath;
        private bool isCrLf;

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
