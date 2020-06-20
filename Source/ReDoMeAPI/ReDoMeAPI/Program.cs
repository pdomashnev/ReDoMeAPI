using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ReDoMeAPI.Options;
using Nancy.Hosting.Self;

namespace ReDoMeAPI
{
    static class Program
    {
        static void Main(string[] args)
        {
            Options.MainOptions.ConnectionString = Properties.Settings.Default.ConnectionString;
            Options.MainOptions.WebServicePort = Properties.Settings.Default.WebServicePort;

            Options.MainOptions.LogDebugData = true;
            Options.MainOptions.LogAuditData = true;
            Options.MainOptions.LogDirectory = "";

            try
            {
                HostConfiguration hostConfigs = new HostConfiguration();
                hostConfigs.UrlReservations.CreateAutomatically = true;
                int port = Options.MainOptions.WebServicePort;
                string url = "";
                if (Properties.Settings.Default.Mode == "https")
                    url = "https://localhost:" + port.ToString();
                else
                    url = "http://localhost:" + port.ToString();
                Uri uri = new Uri(url);
                var host = new NancyHost(hostConfigs, uri);
                host.Start();
                Console.WriteLine("API started");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                string Err = $"Ошибка инициализации web-службы: {e.Message}";
                Console.WriteLine(Err);
                Console.ReadLine();
            }
        }
    }
}
