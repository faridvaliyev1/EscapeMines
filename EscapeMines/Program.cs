using EscapeMines.Extensions;
using EscapeMines.Repositories;
using System;
using System.IO;
using System.Reflection;

namespace EscapeMines
{
    class Program
    {
        static void Main(string[] args)
        {
            string path=Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\file.txt");

            Game gm = new Game();
            gm.Load(path);

            gm.Start();
            
            Console.ReadKey();
        }
    }
}

