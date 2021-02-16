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

            switch (line.LineType)
            {
                case TypeOfLine.EMPTY:
                    break;
                case TypeOfLine.LINE:
                    textBlock.FontSize = 12;
                    break;
                case TypeOfLine.LINK:
                    textBlock.FontSize = 12;
                    textBlock.Foreground = Brushes.Blue;
                    textBlock.MouseDown += new MouseButtonEventHandler((s, e) => search.Text = textBlock.Text.Split(' ')[0]);
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
