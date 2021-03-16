using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfApp.Models;

namespace WpfApp
{
    class Render
    {
        private Grid grid;
        private ScrollViewer view;
        private TextBox search;
        private ConfigModel config = new ConfigModel("config.txt");

        public Render(Grid grid, ScrollViewer view, TextBox search)
        {
            this.view = view;
            this.grid = grid;
            this.search = search;
        }
        private void RenderLine(LineModel line, int index)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = line.Content;
            textBlock.Foreground = config.ForegroundColor;

            switch (line.LineType)
            {
                case TypeOfLine.EMPTY:
                    break;
                case TypeOfLine.LINE:
                    textBlock.FontSize = 12;
                    break;
                case TypeOfLine.LINK:
                    textBlock.FontSize = 12;
                    textBlock.Foreground = config.LinkColor;
                    string baseLink = search.Text;
                    string[] data = textBlock.Text.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    string link = data[0];
                    textBlock.Text = "";
                    if (data.Length > 1)
                        for(int i = 1; i < data.Length; i++)
                        {
                            textBlock.Text += $"{data[i].Trim()} ";
                        }
                    else
                        textBlock.Text = link;
                    textBlock.MouseDown += new MouseButtonEventHandler((s, e) => {
                        if (link[0] == '/')
                        {
                            search.Text = baseLink + link;
                        }
                        else if (!link.Contains("gemini://")) {
                            search.Text = baseLink + "/" + link;
                        }
                        else
                        {
                            search.Text = link;
                        }
                    });
                    break;
                case TypeOfLine.H1:
                    textBlock.FontSize = 24;
                    break;
                case TypeOfLine.H2:
                    textBlock.FontSize = 20;
                    break;
                case TypeOfLine.H3:
                    textBlock.FontSize = 16;
                    break;
                case TypeOfLine.LIST:
                    textBlock.FontSize = 13;
                    textBlock.Text = $"* {textBlock.Text}";
                    break;
                case TypeOfLine.QUOTE:
                    textBlock.FontSize = 12;
                    textBlock.FontStyle = FontStyles.Italic;
                    break;
            }
            
            Grid.SetRow(textBlock, index);
            RowDefinition row = new RowDefinition();
            grid.RowDefinitions.Add(row);
            grid.Children.Add(textBlock);
        }

        public void Lines(List<LineModel> lines)
        {
            int index = 0;
            foreach (LineModel line in lines)
            {
                if (line.LineType != TypeOfLine.PRE)
                {
                    RenderLine(line, index);
                    index++;
                }
            }
            this.view.Content = this.grid;
        }
    }
}
