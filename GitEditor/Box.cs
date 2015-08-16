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
        private IColoring coloring;

        public IColoring Coloring
        {
            get { return coloring; }
            set
            {
                ClearStylesBuffer();
                this.coloring = value;
            }
        }

        public Box() : base() { this.TextChanged += Box_TextChanged; }

        void Box_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (coloring != null)
                coloring.Update(this, e);
        }
    }
}
