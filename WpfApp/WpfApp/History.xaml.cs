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
        private List<string> UrlHistory = HistoryModel.UrlHistory;
        private TextBox searchInputTextBox;

        public History(TextBox SearchInputTextBox)
        {
            InitializeComponent();
            ConfigModel config = new ConfigModel("config.txt");
            Background = config.BackgroundColor;
            listBox.Background = config.BackgroundColor;
            this.searchInputTextBox = SearchInputTextBox;
            RenderHistory();
        }
        private void RenderHistory() {
            if(UrlHistory.Count == 0)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = "Your history is clear.";
                listBox.Items.Add(textBlock);
            }
            foreach (string item in this.UrlHistory)
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
