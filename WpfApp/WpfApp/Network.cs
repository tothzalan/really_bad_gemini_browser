using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp
{
    class Network
    {
        public bool GetUri(string uri)
        {
            if (uri.Substring(0, 5).ToLower() == "file:" && uri.Length > 6) {
                string filePath = uri.Substring(5);
                if (File.Exists(filePath))
                {
                    try
                    {
                        System.IO.File.Copy(filePath, Path.Combine(Directory.GetCurrentDirectory(), "site.gemini"), true);
                        return true;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Copy error!");
                        return false;
                    }

                }
                else {
                    MessageBox.Show("Couldn't find file!");
                    return false;
                }
            }
            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.FileName = "gemget";
                    process.StartInfo.Arguments = $"-o site.gemini {uri}";
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();
                    process.WaitForExit();
                    return true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }
    }
}
