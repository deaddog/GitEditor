using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitEditor
{
    public interface IColoring
    {
        void Update(FastColoredTextBox textbox, TextChangedEventArgs e);
    }
}
