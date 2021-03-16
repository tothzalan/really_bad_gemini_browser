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
            if (!File.Exists(path))
            {
                Console.WriteLine("Config file does not exist!");
                Console.WriteLine("Using default config");
                SetDefault();
            }
            else {
                bool configReadIn = ReadInConfig(path);
                if (!configReadIn)
                {
                    SetDefault();
                }
            }
        }
        private bool ReadInConfig(string path) {
            StreamReader sr = new StreamReader(path);
            while (!sr.EndOfStream)
            {
                string[] line = sr.ReadLine().Split(':');
                BrushConverter bc = new BrushConverter();
                if (line.Length == 2)
                {
                    try
                    {
                        bc.ConvertFrom(line[1]);
                    }
                    catch
                    {
                        return false;
                    }
                    switch (line[0])
                    {
                        case "backgroundcolor":
                            this.BackgroundColor = (Brush)bc.ConvertFrom(line[1]);
                            break;
                        case "foregroundcolor":
                            this.ForegroundColor = (Brush)bc.ConvertFrom(line[1]);
                            break;
                        case "linkcolor":
                            this.LinkColor = (Brush)bc.ConvertFrom(line[1]);
                            break;
                        default:
                            break;
                    }
                }
            }
            return true;
        }
        private void SetDefault() {
            BrushConverter bc = new BrushConverter();
            this.BackgroundColor = (Brush)bc.ConvertFrom("#f39189");
            this.ForegroundColor = (Brush)bc.ConvertFrom("#046582");
            this.LinkColor = (Brush)bc.ConvertFrom("#6e7582");
        }
    }
}
