using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp.Models;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History : Window
    {
        private List<string> history = new List<string>();
        private TextBox searchInputTextBox;

        public History(List<string> history, TextBox SearchInputTextBox)
        {
            InitializeComponent();
            ConfigModel config = new ConfigModel("config.txt");
            Background = config.BackgroundColor;
            listBox.Background = config.BackgroundColor;
            this.history = history;
            this.searchInputTextBox = SearchInputTextBox;
            RenderHistory();
        }
        private void RenderHistory() {
            foreach (string item in this.history)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = item;

                textBlock.MouseDown += new MouseButtonEventHandler((s, e) => {
                    this.searchInputTextBox.Text = textBlock.Text;
                });

                listBox.Items.Add(textBlock);
            }
        }
    }
}
