using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitEditor
{
    public class Box : FastColoredTextBox
    {
        private TextStyle add = new TextStyle(Brushes.Lime, null, FontStyle.Regular);
        private TextStyle remove = new TextStyle(Brushes.Red, null, FontStyle.Regular);
        private TextStyle comment = new TextStyle(Brushes.Orange, null, FontStyle.Regular);
        private TextStyle stuff = new TextStyle(Brushes.BlueViolet, null, FontStyle.Regular);

        public Box() : base() { this.TextChangedDelayed += Box_TextChangedDelayed; }

        void Box_TextChangedDelayed(object sender, TextChangedEventArgs e)
        {
            this.Range.ClearStyle(add, remove, comment, stuff);

            for (int i = 0; i < this.LinesCount; i++)
            {
                var r = this.GetLine(i); ;
                if (r.IsEmpty)
                    continue;
                switch (r.Text[0])
                {
                    case '+': r.SetStyle(add); break;
                    case '-': r.SetStyle(remove); break;
                    case '@': r.SetStyle(stuff); break;
                    case '#': r.SetStyle(comment); break;
                }
            }
        }
    }
}
