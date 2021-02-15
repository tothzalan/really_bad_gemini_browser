using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Models
{
    class LineModel
    {
        public TypeOfLine LineType { get; private set; }
        public string Content { get; private set; }

        public LineModel(TypeOfLine lineType, string content) {
            Content = content;
            LineType = lineType;
        }

        public override string ToString()
        {
            return $"{LineType} {Content}";
        }
    }
}
