using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotifyIntrusion
{
    public class Logger
    {
        private static string path = @"C:\Users\Florin\documents\visual studio 2015\Projects\NotifyIntrusion\NotifyIntrusion\LogFile.txt";

        public void LogInfo(string message)
        {
            File.AppendAllText(path, message + Environment.NewLine);
        }

        public void LogError(string message)
        {
            File.AppendAllText(path, message + Environment.NewLine);
        }
    }
}
