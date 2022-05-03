
using EasyEditor.Controls;
using EasyEditorLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using UI.SyntaxBox;

namespace EasyEditor
{
    
    public partial class MainWindow : Window
    {
        FileSys fileSys = new FileSys();
        public string LAST_TEXT = "";
        public string LAST_LAN = "";

        Controls.Editor editor;

        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += (o, e) =>
            {
                wcb.window = this;
                res_win.window = this;
                new WinDragMove(this, b_move);

                //new WindowBlureffect(this, WindowBlureffect.AccentState.ACCENT_ENABLE_BLURBEHIND);

            };

            {


                editor = new Controls.Editor();
                editros.Children.Clear();
                editros.Children.Add(editor);
                editor.UpdateCountLines += UpdateDebug;


                fileSys.LocalPath = "Syntax";
                fileSys.fileSystemEventHandlerLocal += (o,loc, e) =>
                {
                    this.Dispatcher.BeginInvoke(new Action(() => 
                    {
                        if (editros.Children.Count == 0)
                            return;

                        //editor = (editros.Children[0] as Controls.Editor);
                        //editor.UpdateCountLines += UpdateDebug;


                        LAST_TEXT = editor.Text;
                        editros.Children.Clear();


                        editor = new Controls.Editor();
                        editor.languages = LAST_LAN;
                        editor.Loaded += (ddo, de) =>
                        {
                            editor.Text = LAST_TEXT;
                        };
                        editros.Children.Add(editor);

                        UpdateListCommand();

                    }));
                };
                fileSys.InFileSystemWatcher();
            }
            {

                UpdateListCommand();



                Lang.LeftButtonDown(this, (p) =>
                {
                    multi_file_events.VerticalAlignment = VerticalAlignment.Bottom;
                    multi_file_events.Margin = new Thickness(p.X, 0, 0, buttom_border.Height - multi_file_events.ActualHeight);
                    
                    
                    multi_file_events.SetHideAll("LANG");
                });

                _file_.LeftButtonDown(this, (p) =>
                {
                    multi_file_events.Margin = new Thickness(p.X, b_move.Height, 0, 0);

                    multi_file_events.SetHideAll("FILE");
                    multi_file_events.VerticalAlignment = VerticalAlignment.Top;
                });

                _terminal_.LeftButtonDown(this, (p) =>
                {
                    multi_file_events.Margin = new Thickness(p.X, b_move.Height, 0, 0);

                    multi_file_events.SetHideAll("TERMINAL");
                    multi_file_events.VerticalAlignment = VerticalAlignment.Top;
                });
                

            }

            {


                //SyntaxJson syntaxJson = new SyntaxJson();
                //syntaxJson.languages = Languages.csharp;
                //syntaxJson.add("test", new Syntax()
                //{
                //    IsRegex = true,
                //    Foreground = "#ffff",
                //    Keywords = "[0-9]+",
                //    DriverOperation = DriverOperation.Line.ToString(),

                //});
                //syntaxJson.Save();


            }
            
        }
        public void UpdateListCommand()
        {
            ChSelList chSelList = new ChSelList();
            chSelList.Add("Open", () =>
            {
                // Написать свою*
                OpenFileDialog theDialog = new OpenFileDialog();
                theDialog.Title = "Open";
                theDialog.Filter = "All files (*.*)|*.*";
                if (theDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string filename = theDialog.FileName;
                    editor.Text = File.ReadAllText(filename);
                    UpdateDebug();
                }

            });
            chSelList.Add("Save", () =>
            {

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "Open";
                saveFileDialog.Filter = "All files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string filename = saveFileDialog.FileName;

                    if (editor != null)
                    {
                        File.WriteAllText(filename, editor.Text);
                    }

                }
            });
            multi_file_events.Add("FILE", chSelList);

            chSelList = new ChSelList();
            chSelList.Add("Console", () =>
            {
                Process.Start("CMD");

            });
            chSelList.Add("B", () => { });
            chSelList.Add("C", () => { });
            multi_file_events.Add("TERMINAL", chSelList);

            chSelList = new ChSelList();

            foreach (string item in Directory.GetFiles(fileSys.LocalDirectory()).Add("Text") )
            {
                chSelList.Add(Regex.Replace(new FileInfo(item).Name, @"\.[\s\S]+", ""), (s) =>
                {
                    
                    LAST_TEXT = editor.Text;
                    editros.Children.Clear();
                    editor = new Controls.Editor();
                    editor.languages = s;
                    editor.Loaded += (ddo, de) =>
                    {
                        editor.Text = LAST_TEXT;
                    };
                    editros.Children.Add(editor);

                    LangLabel.Content = editor.languages.ToString();
                });
            }
            multi_file_events.Add("LANG", chSelList);
            multi_file_events.chadd();
        }
        void UpdateDebug()
        {
            if(editor == null)
            {
                count_lines.Content = $"Char:0 Lines:0";
                return;
            }
            count_lines.Content = $"Char:{editor.Text.Length} Lines:{editor.LineCount}";
        }
         
        public void SetMargin(int m)
        {
            grid_main_.Margin = new Thickness(m);
        }
    }

}
