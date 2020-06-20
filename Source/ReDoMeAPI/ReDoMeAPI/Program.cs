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
                Uri uri = new Uri("http://localhost:" + port.ToString());
                var host = new NancyHost(hostConfigs, uri);
                host.Start();
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
