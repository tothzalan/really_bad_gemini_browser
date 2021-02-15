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
        public async Task GetUri(string uri)
        {
            try
            {
                Process.Start("gemget", $"-o site.gemini {uri}");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            /*
            try
            {
                HttpClient client = new HttpClient();
                string responseBody = await client.GetStringAsync(uri);
                MessageBox.Show(responseBody);
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show("\nException Caught!");
                MessageBox.Show("Message :{0} ", e.Message);
            }
            */
        }
    }
}
