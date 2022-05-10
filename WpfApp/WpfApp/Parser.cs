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
            line = line.Trim();
            if (line.Length == 0)
            {
                return new LineModel(TypeOfLine.EMPTY, "");
            }

            if (line.Trim().Length > 3)
            {
                if (line.Substring(0, 3) == "###")
                {
                    return new LineModel(TypeOfLine.H3, line.Substring(3).Trim());
                }
            }
            if (line.Trim().Length == 3)
            {
                if (line.Substring(0, 3) == "```")
                {
                    return new LineModel(TypeOfLine.PRE, line.Substring(3).Trim());
                }
            }
            if (line.Length > 2)
            {
                switch (line.Substring(0, 2))
                {
                    case "##":
                        return new LineModel(TypeOfLine.H2, line.Substring(2).Trim());
                    case "=>":
                        return new LineModel(TypeOfLine.LINK, line.Substring(2).Trim());
                }
            }
            if (line.Length > 1)
            {
                switch (line.Substring(0, 1))
                {
                    case "#":
                        return new LineModel(TypeOfLine.H1, line.Substring(1).Trim());
                    case ">":
                        return new LineModel(TypeOfLine.QUOTE, line.Substring(1).Trim());
                    case "*":
                        return new LineModel(TypeOfLine.LIST, line.Substring(1).Trim());

                }
            }
            return new LineModel(TypeOfLine.LINE, line);
        }
        public List<LineModel> FromFile(string fileName)
        {
            StreamReader sr = new StreamReader(fileName);
            List<LineModel> lines = new List<LineModel>();
            bool isPreLine = false;
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                LineModel parsedLine = ParseLine(line);
                if (parsedLine.LineType == TypeOfLine.PRE)
                {
                    isPreLine = !isPreLine;
                    continue;
                }
                if (isPreLine)
                {
                    lines.Add(new LineModel(TypeOfLine.PRE, line));
                }
                else
                    lines.Add(parsedLine);
            }
            return lines;
        }
    }
}
