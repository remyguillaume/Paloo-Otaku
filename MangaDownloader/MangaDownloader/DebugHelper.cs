using System;
using System.Diagnostics;

namespace MangaDownloader
{
    public class DebugHelper
    {
        public static void WriteLine(string s)
        {
            Console.WriteLine(s);
            Debug.WriteLine(s);
        }
    }
}