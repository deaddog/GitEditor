using System;
using System.Text.RegularExpressions;

namespace GitEditor
{
    public class RebaseAction
    {
        private Actions action;
        private string sha;
        private string message;

        public Actions Action
        {
            get { return action; }
            set { action = value; }
        }
        public string Sha
        {
            get { return sha; }
        }
        public string Message
        {
            get { return message; }
        }

        public RebaseAction(Actions action, string sha, string message)
        {
            this.action = action;
            this.sha = sha;
            this.message = message;
        }

        public bool IsSha(string sha)
        {
            int len = Math.Min(sha.Length, this.sha.Length);
            return sha.Substring(0, len).Equals(this.sha.Substring(0, len));
        }

        public override string ToString()
        {
            return action.ToString() + " " + sha + " " + message;
        }

        public static bool TryParse(string line, out RebaseAction action)
        {
            string actions = string.Join("|", Enum.GetNames(typeof(Actions)));
            Regex regex = new Regex("(?<action>" + actions + ") (?<sha>[a-z0-9]{7,}) (?<message>[^\r\n]*)");
            var match = regex.Match(line);

            if (!match.Success)
            {
                action = null;
                return false;
            }
            else
            {
                Actions a;
                Enum.TryParse(match.Groups["action"].Value, out a);
                string sha = match.Groups["sha"].Value;
                string message = match.Groups["message"].Value;

                action = new RebaseAction(a, sha, message);
                return true;
            }
        }

        public enum Actions
        {
            pick, reword, edit, squash, fixup, exec
        }
    }
}
