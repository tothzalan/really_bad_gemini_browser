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
            if (!File.Exists(path)) {
                Console.WriteLine("Config file does not exist!");
                Console.WriteLine("Using default config");
                BrushConverter bc = new BrushConverter();
                this.BackgroundColor = (Brush)bc.ConvertFrom("#f39189");
                this.ForegroundColor = (Brush)bc.ConvertFrom("#046582");
                this.LinkColor = (Brush)bc.ConvertFrom("#6e7582");
            }
        }
    }
}
