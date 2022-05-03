using EasyEditorLib;
using System;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using UI.SyntaxBox;

namespace EasyEditor.Controls
{
    /// <summary>
    /// Логика взаимодействия для Editor.xaml
    /// </summary>
    public partial class Editor : UserControl
    {

        SyntaxJson syntaxJson = new SyntaxJson();
        public string languages;
        public string LINE_COMMENT = "//";

        public Action UpdateCountLines;


        public string Text
        {
            get
            {
                return TextEditor.Text;
            }
            set
            {
                TextEditor.Text = value;
            }
        }
        public int LineCount
        {
            get
            {
                return TextEditor.LineCount;
            }
        }

        public Editor()
        {
            InitializeComponent();
            SetLanguages(languages); 

            {
                TextEditor.PreviewKeyDown += (o, e) =>
                {
                    if (UpdateCountLines != null)
                        UpdateCountLines();
                    if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.OemQuestion)
                    {
                        OnCommentCommand();
                       
                    }
                    if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.OemPeriod)
                    {
                        OnUncommentCommand();
                    }
                    
                };
                 
            }

            this.Loaded += (o, e) =>
            {
                //TextEditor.SetValue(SyntaxBox.EnabledProperty, false);
                SetLanguages(languages);
            };
        }
        private void SetLanguages(string lang)
        {
            Syntax_Config.Clear();
            syntaxJson.languages = lang;
            syntaxJson.Open();

            syntaxJson.EventOpen += (b) =>
            {
                if (b)
                {
                    if (syntaxJson.syntax == null)
                        return;
                    foreach (var item in syntaxJson.syntax)
                    {
                        try
                        {
                            if (!item.Value.IsRegex)
                            {
                                KeywordRule syntax_Config = new KeywordRule()
                                {
                                    Keywords = item.Value.Keywords,
                                    Op = DriverOperationType.GetType(item.Value.DriverOperation),
                                    Foreground = item.Value.Foreground.ToBrush(),
                                    Background = item.Value.Background.ToBrush(),

                                };

                                this.AddSyntax(syntax_Config);
                            }
                            else
                            {
                                RegexRule regexRule = new RegexRule
                                {
                                    Foreground = item.Value.Foreground.ToBrush(),
                                    Background = item.Value.Background.ToBrush(),
                                    Op = DriverOperationType.GetType(item.Value.DriverOperation),
                                    Pattern = item.Value.Keywords,

                                };
                                Syntax_Config.Add(regexRule);
                            }

                        }
                        catch (Exception)
                        { }

                    }
                    TextEditor.SetValue(SyntaxBox.EnabledProperty, true);
                }
                else
                {
                    TextEditor.SetValue(SyntaxBox.EnabledProperty, false);
                }
            };
            

            
        }
        public void AddSyntax(ISyntaxRule syntaxRule)
        {
            Syntax_Config.Add(syntaxRule);
        }

        public void OnCommentCommand()
        {
            Console.WriteLine("asdasdasd");

            int
                selStart = TextEditor.SelectionStart,
                selLength = TextEditor.SelectionLength,
                selEnd = selStart + selLength,
                firstLine = TextEditor.GetLineIndexFromCharacterIndex(selStart),
                lastLine = selLength > 0
                    ? TextEditor.GetLineIndexFromCharacterIndex(selStart + selLength - 1)
                    : firstLine;
            var affectedLines = TextEditor.Text.GetLines(firstLine, lastLine, out int totalLines).ToList();
            int selStartOffset = affectedLines[0].EndIndex - selStart;
            int selEndOffset = affectedLines[affectedLines.Count - 1].EndIndex - selEnd;
            int insPos = affectedLines
                .Select(FindLineStart)
                .Min();
            var indentedBlock = String.Join("", affectedLines
                .Select((line) => line.Text.Substring(0, insPos)
                    + LINE_COMMENT
                    + line.Text.Substring(insPos))
                .ToArray());
            StringBuilder sb = new StringBuilder();
            sb.Append(TextEditor.Text.Substring(0, affectedLines[0].StartIndex));
            sb.Append(indentedBlock);
            sb.Append(this.TextEditor.Text.Substring(affectedLines[affectedLines.Count - 1].StartIndex + affectedLines[affectedLines.Count - 1].Text.Length));
            this.TextEditor.Text = sb.ToString();
            var firstAffected = this.TextEditor.Text.GetLines(firstLine, firstLine, out totalLines).Single();
            var lastAffected = this.TextEditor.Text.GetLines(lastLine, lastLine, out totalLines).Single();
            selStart = firstAffected.EndIndex - selStartOffset;
            selEnd = lastAffected.EndIndex - selEndOffset;
            selLength = selEnd - selStart;
            this.TextEditor.Select(selStart, selLength);
        }
        public void OnUncommentCommand()
        {
            int
                selStart = this.TextEditor.SelectionStart,
                selLength = this.TextEditor.SelectionLength,
                selEnd = selStart + selLength,
                firstLine = this.TextEditor.GetLineIndexFromCharacterIndex(selStart),
                lastLine = selLength > 0
                    ? this.TextEditor.GetLineIndexFromCharacterIndex(selStart + selLength - 1)
                    : firstLine;
            var affectedLines = this.TextEditor.Text.GetLines(firstLine, lastLine, out int totalLines).ToList();
            int selStartOffset = affectedLines[0].EndIndex - selStart;
            int selEndOffset = affectedLines[affectedLines.Count - 1].EndIndex - selEnd;
            var unindentedBlock = String.Join("", affectedLines
                .Select((line) =>
                {
                    int start = FindLineStart(line);
                    if (line.Text.Substring(start).StartsWith(LINE_COMMENT))
                    {
                        return (line.Text.Substring(0, start)
                            + line.Text.Substring(start + LINE_COMMENT.Length));
                    }
                    return (line.Text);
                })
                .ToArray()); 
            StringBuilder sb = new StringBuilder();
            sb.Append(this.TextEditor.Text.Substring(0, affectedLines[0].StartIndex));
            sb.Append(unindentedBlock);
            sb.Append(this.TextEditor.Text.Substring(affectedLines[affectedLines.Count - 1].StartIndex + affectedLines[affectedLines.Count - 1].Text.Length));
            this.TextEditor.Text = sb.ToString();
            var firstAffected = this.TextEditor.Text.GetLines(firstLine, firstLine, out totalLines).Single();
            var lastAffected = this.TextEditor.Text.GetLines(lastLine, lastLine, out totalLines).Single();
            selStart = Math.Max(firstAffected.StartIndex, firstAffected.EndIndex - selStartOffset);
            selEnd = Math.Max(
                lastAffected.StartIndex,
                lastAffected.EndIndex - selEndOffset);
            selLength = selEnd - selStart;
            this.TextEditor.Select(selStart, selLength);
        }
        private static int FindLineStart(TextLine Line)
        {
            for (int i = 0; i < Line.Text.Length; i++)
            {
                if (!Char.IsWhiteSpace(Line.Text[i]))
                {
                    return (i);
                }
            }
            if (Line.Text.EndsWith(Environment.NewLine))
                return (Line.Text.Length - Environment.NewLine.Length);
            else
                return (Line.Text.Length);
        }
    }
}
