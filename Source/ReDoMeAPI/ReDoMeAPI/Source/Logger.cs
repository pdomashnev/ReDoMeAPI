using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReDoMeAPI
{
    //---------------------------------------------------------------------------------
    public class LogMessage
    {
        public string cMessage { set; get; }
        public string cSource { set; get; }
        public System.Diagnostics.EventLogEntryType Type { set; get; }
        public Exception Exception { set; get; }
        public LogMessage(string _Message, string _Source, System.Diagnostics.EventLogEntryType _Type, Exception _Exception = null)
        {
            cMessage = _Message;
            cSource = _Source;
            Type = _Type;
            Exception = _Exception;
        }
    }
    //---------------------------------------------------------------------------------
    public static class Logger
    {
        //---------------------------------------------
        public static void SaveDebugData(string _Data, string _DataName)
        {
            if (!Options.MainOptions.LogDebugData)
                return;
            string Path = "";
            string FileName = "";
            if (!String.IsNullOrEmpty(Options.MainOptions.LogDirectory))
                Path = System.IO.Path.Combine(Options.MainOptions.LogDirectory, DateTime.Now.ToString("yyyy_MM_dd"));
            else
                Path = System.IO.Path.Combine(Environment.CurrentDirectory, "Log", DateTime.Now.ToString("yyyy_MM_dd"));
            FileName = System.IO.Path.Combine(Path, $"{DateTime.Now.ToString("yyyy_MM_dd_HH24_mm_ss")}_{_DataName}.xml");
            if (!System.IO.Directory.Exists(Path))
                System.IO.Directory.CreateDirectory(Path);
            System.IO.StreamWriter file = null;
            file = System.IO.File.CreateText(FileName);
            file.WriteLine(_Data);
            file.Close();
        }
        //---------------------------------------------
        public static void AddMessageToLog(string _Message, string _Type, Exception _Exception = null)
        {
            try
            {
                string FileName = "";
                if (!String.IsNullOrEmpty(Options.MainOptions.LogDirectory))
                    FileName = System.IO.Path.Combine(Options.MainOptions.LogDirectory, DateTime.Now.ToString("yyyy_MM_dd") + "_Log.txt");
                else
                    FileName = System.IO.Path.Combine(Environment.CurrentDirectory, "Log", DateTime.Now.ToString("yyyy_MM_dd") + "_Log.txt");
                System.IO.StreamWriter file = null;
                if (!System.IO.File.Exists(FileName))
                    file = System.IO.File.CreateText(FileName);
                else
                    file = System.IO.File.AppendText(FileName);
                file.WriteLine($"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")}\t{_Type}\t{_Message}");
                if(_Exception != null)
                {
                    file.WriteLine($"Exception stack trace:\n {_Exception.StackTrace}");
                }
                file.Close();

                Console.WriteLine($"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")}\t{_Type}\t{_Message}");
            }
            catch (Exception e)
            {

            }
        }
        //---------------------------------------------
        public static void AddMessageToLog(LogMessage _Message)
        {
            if (_Message.Type == System.Diagnostics.EventLogEntryType.SuccessAudit &&
                !Options.MainOptions.LogAuditData)
                return;

            string mes = $"{_Message.cSource}: {_Message.cMessage}";
            AddMessageToLog(mes, _Message.Type.ToString(), _Message.Exception);
        }
        //---------------------------------------------
    }
}
