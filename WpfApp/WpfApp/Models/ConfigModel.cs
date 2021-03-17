using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media;

namespace WpfApp.Models
{
    class ConfigModel
    {
        public Brush  BackgroundColor { get; private set; }
        public Brush ForegroundColor { get; private set; }
        public Brush LinkColor { get; private set; }

        public ConfigModel(string path) {
            SetDefault();
            if (!File.Exists(path))
            {
                Console.WriteLine("Config file does not exist!");
                Console.WriteLine("Using default config");
                SetDefault();
            }
            else {
               ReadInConfig(path);
            }
        }
        private void ReadInConfig(string path) {
            StreamReader sr = new StreamReader(path);
            while (!sr.EndOfStream)
            {
                string[] line = sr.ReadLine().Split(':');
                BrushConverter bc = new BrushConverter();
                if (line.Length == 2)
                {
                    Brush lineColor;
                    try
                    {
                        lineColor = (Brush)bc.ConvertFrom(line[1]);
                    }
                    catch
                    {
                        continue;
                    }
                    switch (line[0])
                    {
                        case "backgroundcolor":
                            this.BackgroundColor = lineColor;
                            break;
                        case "foregroundcolor":
                            this.ForegroundColor = lineColor;
                            break;
                        case "linkcolor":
                            this.LinkColor = lineColor;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        private void SetDefault() {
            BrushConverter bc = new BrushConverter();
            this.BackgroundColor = (Brush)bc.ConvertFrom("#FFFFFF");
            this.ForegroundColor = (Brush)bc.ConvertFrom("#000000");
            this.LinkColor = (Brush)bc.ConvertFrom("#0000FF");
        }
    }
}
