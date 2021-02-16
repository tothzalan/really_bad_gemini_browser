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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp.Models;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        void ClearScreen() {
            renderGrid.Children.RemoveRange(0, renderGrid.Children.Count - 1);
        }

        private async void SeachButton_Click(object sender, RoutedEventArgs e)
        {
            if (SearchInputTextBox.Text.Trim().Length < 3)
            {
                MessageBox.Show("Wrong address!", "Please try again");
            }
            else
            {
                ClearScreen();
                Network network = new Network();
                await network.GetUri(SearchInputTextBox.Text.Trim());
                Parser parser = new Parser();
                List<LineModel> models = parser.FromFile("site.gemini");

                Render render = new Render(renderGrid, scrollView, SearchInputTextBox);
                render.Lines(models);

            }
        }
    }
}
