﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpLineCounter
{
    class Program
    {
        //--------------------------------------------
        // Mo.26.07.2021 17:37:02 -op- SummenZeile  # Files # Lines
        // Do.22.07.2021 20:12:35 -op- mit SubDir /s
        // So.02.05.2021 16:39:26 -op- nach .\GitHub\OtmarP\OpLineCounter\src\OpLineCounter\
        // Mi.28.04.2021 09:35:40 -op- Cr (4.7.2)
        //--------------------------------------------

        static void Main(string[] args)
        {
            string path = @".\";
            string pattern = "*.*";
            bool subDir = false;
#if DEBUG
            path = @"C:\Dev\TFS\Git\OpTools\MSPartnerCenterDownloader\MSPartnerCenter\bin\Debug\DATA";
            path = @"C:\Dev\Temp\MSPartnerCenter\DATA\20210426";
            path = @"C:\Dev\Temp\MSPartnerCenter\DATA";
            //path = @".\";
            pattern = "*_*_*_*_*.*";
#endif

            List<string> argList = new List<string>();
            for (int i = 0; i < args.Length; i++)
            {
                argList.Add(args[i]);
            }
            foreach (var item in argList)
            {
                if (item.Equals("/s", StringComparison.CurrentCultureIgnoreCase))
                {
                    subDir = true;
                    argList.Remove(item);
                    break;  //==========================>
                }
            }
            foreach (var item in argList)
            {
                pattern = item;
            }

            var sd = System.IO.SearchOption.TopDirectoryOnly;
            if (subDir) { sd = System.IO.SearchOption.AllDirectories; }

            int sumLines = 0;
            int sumFiles = 0;

            int len = 21;

            Console.WriteLine("Path: " + path + " Pattern: " + pattern + " SubDir: " + subDir);
            string trenn = "--------------------------------------------";
            //              00000000011111111112222222222333333333344444444445
            //              12345678901234567890123456789012345678901234567890
            //              20210428_121314_D_30_ 123456789012
            //              20210430_000700_W_12_          302
            //              SQLUnitTest1.cs               2838
            //              --------------------------------------------
            //              Files: 1234                   1234 Lines
            Console.WriteLine(trenn);
            var dirList = System.IO.Directory.GetFiles(path, pattern, sd);
            for (int i = 0; i < dirList.Length; i++)
            {
                string fullFileName = dirList[i];

                string fileName = System.IO.Path.GetFileName(fullFileName);

                string[] countLines = { };
                try
                {
                    countLines = System.IO.File.ReadAllLines(fullFileName);
                }
                catch (Exception ex)
                {
                    //
                }

                // 20210428_121314_D_30_ 123456789012
                string disp = fileName;
                if (disp.Length > len)
                {
                    disp = disp.Substring(0, len);
                }
                else
                {
                    disp = disp + "".PadRight(len);
                    disp = disp.Substring(0, len);
                }

                disp = disp.Trim() + "" + "".PadRight(len, ' ');
                disp = disp.Substring(0, len);

                //+ " ............................................................................";
                int lines = countLines.Length - 1;  // - Header in .csv
                sumLines += lines;
                string dispLines = lines.ToString();
                if (lines == 0)
                {
                    dispLines = ".";
                }
                dispLines = dispLines.PadLeft(12);
                Console.WriteLine(disp + " " + dispLines);
                sumFiles++;
            }

            Console.WriteLine(trenn);

            string disp2 = "Files: " + sumFiles.ToString() + "" + "".PadRight(len, ' ');
            disp2 = disp2.Substring(0, len);
            string dispLines2 = sumLines.ToString();
            dispLines2 = dispLines2.PadLeft(12);
            Console.WriteLine(disp2 +" "+ dispLines2 + " Lines");

#if DEBUG
            Console.WriteLine("Weiter mit jeder Taste...");
            Console.ReadKey();
#endif
        }
    }
}