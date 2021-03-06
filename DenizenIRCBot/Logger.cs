﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DenizenIRCBot
{
    public class Logger
    {
        public static bool Debugging = false;

        public static Object Locker = new Object();

        public static void Output(LogType type, string info)
        {
            DateTime now = DateTime.Now;
            string prefix = now.Year + "/" + Utilities.FormatNumber(now.Month) + "/" + Utilities.FormatNumber(now.Day)
                + " " + Utilities.FormatNumber(now.Hour) + ":" + Utilities.FormatNumber(now.Minute) + ":" + Utilities.FormatNumber(now.Second);
            string outp = null;
            ConsoleColor color = ConsoleColor.White;

            switch (type)
            {
                case LogType.INFO:
                    outp = prefix + " [INFO] " + info;
                    color = ConsoleColor.White;
                    break;
                case LogType.DEBUG:
                    if (Debugging)
                    {
                        outp = prefix + " [DEBUG] " + info;
                        color = ConsoleColor.Gray;
                    }
                    break;
                case LogType.ERROR:
                    outp = prefix + " [INFO] " + info;
                    color = ConsoleColor.Yellow;
                    break;
                default:
                    outp = prefix + " [UNKNOWN LOGTYPE] " + info;
                    color = ConsoleColor.Yellow;
                    break;
            }

            if (outp != null)
            {
                // TODO: Log to file? (ASync?)
                lock (Locker)
                {
                    Console.ForegroundColor = color;
                    Console.WriteLine(outp);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
    }

    public enum LogType: byte
    {
        INFO = 1,
        DEBUG = 2,
        ERROR = 3
    }
}
