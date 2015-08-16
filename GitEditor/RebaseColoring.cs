using FastColoredTextBoxNS;
using System.Drawing;
using System.Text.RegularExpressions;

namespace GitEditor
{
    public class RebaseColoring : IColoring
    {
        private TextStyle comment = new TextStyle(Brushes.Orange, null, FontStyle.Regular);

        private TextStyle pick = new TextStyle(Brushes.Lime, null, FontStyle.Regular);
        private TextStyle reword = new TextStyle(Brushes.LimeGreen, null, FontStyle.Regular);
        private TextStyle edit = new TextStyle(Brushes.Turquoise, null, FontStyle.Regular);
        private TextStyle squash = new TextStyle(Brushes.DeepSkyBlue, null, FontStyle.Regular);
        private TextStyle fixup = new TextStyle(Brushes.RoyalBlue, null, FontStyle.Regular);
        private TextStyle exec = new TextStyle(Brushes.DarkViolet, null, FontStyle.Regular);

        public void Update(FastColoredTextBox textbox, TextChangedEventArgs e)
        {
            textbox.Range.ClearStyle(comment, pick, reword, edit, squash, fixup, exec);

            for (int i = 0; i < textbox.LinesCount; i++)
            {
                var r = textbox.GetLine(i); ;
                if (r.IsEmpty)
                    continue;

                if (r.Text.StartsWith("#"))
                {
                    r.SetStyle(comment);
                    continue;
                }

                var match = Regex.Match(r.Text, "^[^ ]*");
                if (!match.Success)
                    continue;

                switch (match.Value)
                {
                    case "p":
                    case "pick":
                        r.SetStyle(pick); break;

                    case "r":
                    case "reword":
                        r.SetStyle(reword); break;

                    case "e":
                    case "edit":
                        r.SetStyle(edit); break;

                    case "s":
                    case "squash":
                        r.SetStyle(squash); break;

                    case "f":
                    case "fixup":
                        r.SetStyle(fixup); break;

                    case "x":
                    case "exec":
                        r.SetStyle(exec); break;
                }
            }
        }
    }
}
