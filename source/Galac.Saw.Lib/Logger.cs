using LibGalac.Aos.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Galac.Saw.Lib {
    public enum LoggerEnvironment {
        Development,
        Production
    }

    public static class Logger {
        private const string TAB = "\t";
        public static string Ruta {
            get; private set;
        }
        public static LoggerEnvironment LoggerEnvironment {
            get; private set;
        }

        public static void Initialize(string ruta,LoggerEnvironment loggerEnvironment) {
            Ruta = ruta;
            LoggerEnvironment = loggerEnvironment;
        }

        public static void Log(string textToLog) {
            if(LoggerEnvironment == LoggerEnvironment.Production && string.IsNullOrEmpty(Ruta)) {
                return;
            }
            if(!LibString.IsNullOrEmpty(textToLog)) {
                switch(LoggerEnvironment) {
                    case LoggerEnvironment.Development:
                        Debug.WriteLine(textToLog);
                        break;
                    case LoggerEnvironment.Production:
                        LibIO.WriteLineInFile(Ruta,textToLog,true);
                        break;
                    default:
                        break;
                }
            }
        }

        public static void Log(params string[] values) {
            if(values.Length == 0)
                return;
            string valuesSeparatedByTab = string.Empty;
            foreach(var parameter in values) {
                valuesSeparatedByTab += parameter + TAB;
            }
            Log(valuesSeparatedByTab);
        }

        public static void Table(string caption,string[] columns,params string[] values) {
            Log(caption);
            string table = string.Empty;
            if(columns.Length == 0 || values.Length > columns.Length) {
                return;
            }
            Log(CreateTable(columns,values));
        }

        public static string CreateTable(string[] columns,params string[] values) {
            List<string> listOfValues = new List<string>(values);
            listOfValues = listOfValues.Select(t => t.Trim().CenterString(1)).ToList();
            columns = columns.Select(t => t.Trim().CenterString(1)).ToArray();
            string table = string.Empty;
            string header = "|";
            string rowValues = "|";
            string dashedRow = string.Empty;
            CenterAndAlign(columns,listOfValues);
            header += string.Join("|",columns) + "|";
            dashedRow += string.Join("",header.Select(t => "-"));
            rowValues += string.Join("|",listOfValues) + "|";
            table = dashedRow + Environment.NewLine +
                    header + Environment.NewLine +
                    dashedRow + Environment.NewLine +
                    rowValues + Environment.NewLine +
                    dashedRow;
            return table;
        }

        private static void CenterAndAlign(string[] columns,List<string> listOfValues) {
            for(int i = 0;i < columns.Length;i++) {
                if(i < listOfValues.Count) {
                    int numberOfSpaces = (int)Math.Ceiling((decimal)(columns[i].Length - listOfValues[i].Length) / 2);
                    if(listOfValues[i].Length < columns[i].Length) {
                        listOfValues[i] = listOfValues[i].CenterString(numberOfSpaces);
                    } else {
                        numberOfSpaces = -numberOfSpaces;
                        columns[i] = columns[i].CenterString(numberOfSpaces);
                        if(columns[i].Length < listOfValues[i].Length) {
                            columns[i] = columns[i].PadRight(Math.Abs(columns[i].Length - listOfValues[i].Length),' ');
                        } else if(columns[i].Length > listOfValues[i].Length) {
                            listOfValues[i] = listOfValues[i].PadRight(Math.Abs(listOfValues[i].Length - columns[i].Length),' ');
                        }
                    }
                } else {
                    listOfValues.Add(string.Empty.CenterString(columns[i].Length - 1));
                }
            }
        }
    }


    public static class StringExtensions {
        public static string CenterString(this string originalString,int numberOfSpaces) {
            string spaces = string.Empty;
            for(int i = 0;i < numberOfSpaces;i++) {
                spaces += " ";
            }
            originalString = spaces + originalString + spaces;
            return originalString;
        }
    }

}
