using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Media;

namespace UI.SyntaxBox
{
    public class RegexRule : ISyntaxRule
    {
        private Regex _regex = null;

        #region ISyntaxRule members
        // ...................................................................
        public int RuleId { get; set; }
        // ...................................................................
        /// <summary>
        /// The driver operation to apply (Line | Block | FullText).
        /// </summary>
        public DriverOperation Op { get; set; } = DriverOperation.None;
        // ...................................................................
        /// <summary>
        /// Matches the rule against the provided text.
        /// Used internally, shouldn't be called by user code.
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public IEnumerable<FormatInstruction> Match(string Text)
        {
            var regex = this.GetRegex();
            var matches = regex.Matches(Text);

            List<FormatInstruction> instructions = new List<FormatInstruction>();
            foreach (Match match in matches)
            {
                instructions.Add(new FormatInstruction
                {
                    RuleId = this.RuleId,
                    FromChar = match.Index,
                    Length = match.Length,
                    Foreground = Foreground,
                    Background = Background,
                    Outline = Outline
                });
            }
            return (instructions);

            //var instructions = matches
            //    .Select((x) => new FormatInstruction
            //    {
            //        FromChar = x.Position,
            //        Length = x.Length,

            //        RuleId = this.RuleId,
            //        Foreground = this.Foreground,
            //        Background = this.Background,
            //        Outline = this.Outline
            //    }).ToList();
            //return (instructions);


        }
        // ...................................................................
        #endregion

        #region Public members
        // ...................................................................
        /// <summary>
        /// Background brush
        /// </summary>
        public Brush Background { get; set; }
        // ...................................................................
        /// <summary>
        /// Foreground brush.
        /// </summary>
        public Brush Foreground { get; set; }
        // ...................................................................
        /// <summary>
        /// Outline pen
        /// </summary>
        public Pen Outline { get; set; }
        // ...................................................................
        /// <summary>
        /// The regex pattern to match.
        /// </summary>
        public string Pattern { get; set; }
        // ...................................................................
        #endregion

        #region Private members
        // ...................................................................
        private Regex GetRegex()
        {
            if (this._regex == null)
                this._regex = new Regex(this.Pattern);

            return (this._regex);
        }
        // ...................................................................
        #endregion
    }
}
