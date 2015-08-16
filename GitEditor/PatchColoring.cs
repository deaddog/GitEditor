using FastColoredTextBoxNS;
using System.Drawing;

namespace GitEditor
{
    public class PatchColoring : IColoring
    {
        private TextStyle add = new TextStyle(Brushes.Lime, null, FontStyle.Regular);
        private TextStyle remove = new TextStyle(Brushes.Red, null, FontStyle.Regular);
        private TextStyle comment = new TextStyle(Brushes.Orange, null, FontStyle.Regular);
        private TextStyle stuff = new TextStyle(Brushes.BlueViolet, null, FontStyle.Regular);

        public void Update(FastColoredTextBox textbox, TextChangedEventArgs e)
        {
            textbox.Range.ClearStyle(add, remove, comment, stuff);

            for (int i = 0; i < textbox.LinesCount; i++)
            {
                var r = textbox.GetLine(i); ;
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
