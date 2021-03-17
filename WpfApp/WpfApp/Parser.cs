using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Models;
using System.IO;

namespace WpfApp
{
    class Parser
    {
        private LineModel ParseLine(string line) {
            if (line.Trim().Length == 0)
            {
                return new LineModel(TypeOfLine.EMPTY, "");
            }
            if (line.Trim().Length > 1)
            {
                if (line.Substring(0, 1) == "#" && line[1] != '#') //h1
                {
                    return new LineModel(TypeOfLine.H1, line.Substring(1).Trim());
                }
                else if (line.Substring(0, 1) == ">") //quote
                {
                    return new LineModel(TypeOfLine.QUOTE, line.Substring(1).Trim());
                }
                else if (line.Substring(0, 1) == "*") //list
                {
                    return new LineModel(TypeOfLine.LIST, line.Substring(1).Trim());
                }
            }
            if (line.Trim().Length > 2) {
                if (line.Substring(0, 2) == "##" && line[2] != '#') //h2
                {
                    return new LineModel(TypeOfLine.H2, line.Substring(2).Trim());
                }
                else if (line.Substring(0, 2) == "=>") //link
                {
                    return new LineModel(TypeOfLine.LINK, line.Substring(2).Trim());
                }
            }
            if (line.Trim().Length > 3)
            {
                if (line.Substring(0, 3) == "###") //h3
                {
                    return new LineModel(TypeOfLine.H3, line.Substring(3).Trim());
                }
                else if (line.Substring(0, 3) == "```") //pre
                {
                    return new LineModel(TypeOfLine.PRE, line.Substring(3).Trim());
                }
            }
            return new LineModel(TypeOfLine.LINE, line);
        }
        public List<LineModel> FromFile(string fileName)
        {
            StreamReader sr = new StreamReader(fileName);
            List<LineModel> lines = new List<LineModel>();
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                lines.Add(ParseLine(line));
                Console.WriteLine(ParseLine(line));
            }
            return lines;
        }
    }
}
