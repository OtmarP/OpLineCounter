using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpLineCounter
{
    class Program
    {
        //--------------------------------------------
        // Mo.06.12.2021 19:27:59 -op- Lines/File
        // So.28.11.2021 09:10:35 -op- Graphic output, with |
        // Do.25.11.2021 18:50:03 -op- class DirEntry
        // Di.23.11.2021 19:56:54 -op- loop in memory, dann Anzeige
        // Fr.12.11.2021 19:54:27 -op- Statistik-Data: Min/Max
        // So.07.11.2021 17:51:03 -op- SummenZeile # Lines/File
        // Mo.26.07.2021 17:37:02 -op- SummenZeile  # Files # Lines
        // Do.22.07.2021 20:12:35 -op- mit SubDir /s
        // So.02.05.2021 16:39:26 -op- nach .\GitHub\OtmarP\OpLineCounter\src\OpLineCounter\
        // Mi.28.04.2021 09:35:40 -op- Cr (4.7.2)
        //--------------------------------------------
        // ToDo:
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
            //path = @"C:\temp\Data";
            //path = @".\";
            pattern = "*_*_*_*_*.*";
            //pattern = "202101*.*";
            //pattern = "*_*_D_*_*.*";
            //pattern = "*_*_D_30_*.*";
            //pattern = "*_*_D_90_*.*";
            //pattern = "*_*_W_*_*.*";
            //pattern = "*_*_W_06_*.*";
            //pattern = "*_*_W_12_*.*";
            //pattern = "*_*_H_72_*.*";
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
            if (subDir)
            {
                sd = System.IO.SearchOption.AllDirectories;
            }

            int sumLines = 0;
            int sumFiles = 0;
            int min0Lines = int.MaxValue;
            int minLines = int.MaxValue;
            int maxLines = int.MinValue;

            int len = 21;
            int lenCounter = 12;

            Console.WriteLine("Path: " + path + " Pattern: " + pattern + " SubDir: " + subDir);
            string trenn = "-------------------------------------------------------------------------------";
            //              00000000011111111112222222222333333333344444444445555555555666666666677777777770
            //              12345678901234567890123456789012345678901234567890123456789012345678901234567890
            //              20210428_121314_D_30_ 123456789012 ||||||||||||||||||||||||||||||||||||||||||||
            //              20210430_000700_W_12_          302 ||||||||||||
            //              SQLUnitTest1.cs               2838 ||||||||||||||||||||||||||||||||||||
            //              --------------------------------------------
            //              Files: 1234                   1234 Lines
            //              Files: 363                    9278 Lines
            //              Files: 255                   28041 Lines, 109.96 Lines/File, Min:2/0 Max:305
            //Console.WriteLine(trenn);   // --------------------------------------------

            List<DirEntry> list = new List<DirEntry>();

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
                if (lines == 0)
                {
                    min0Lines = 0;
                }
                else
                {
                    if (lines < minLines)
                    {
                        minLines = lines;
                    }
                    if (lines > maxLines)
                    {
                        maxLines = lines;
                    }
                }
                string dispLines = lines.ToString();
                if (lines == 0)
                {
                    dispLines = ".";
                }
                dispLines = dispLines.PadLeft(lenCounter);
                string graph = "|";
                //Console.WriteLine(disp + " " + dispLines + " " + graph);
                var de = new DirEntry();
                de.Name = disp; // + " " + dispLines + " " + graph;
                de.LinesNum = lines;
                de.Lines = dispLines;
                de.Graph = graph;
                list.Add(de);
                sumFiles++;
            }

            // Display
            Console.WriteLine(trenn);   // --------------------------------------------
            foreach (var item in list)
            {
                //              00000000011111111112222222222333333333344444444445555555555666666666677777777770
                //              12345678901234567890123456789012345678901234567890123456789012345678901234567890
                //              20210428_121314_D_30_ 123456789012 ||||||||||||||||||||||||||||||||||||||||||||
                //                                                 min                                        max
                //                                                 |                                          |
                int grLen = 1;
                if (item.LinesNum != 0) {
                    var iber = maxLines - minLines + 1 + 0.0;
                    var dotsAll = 44.0;
                    var dotsPerCnt = dotsAll / iber;
                    var x = item.LinesNum - minLines + 1+0.0;
                    var len1 = (x * dotsPerCnt);
                    grLen = (int)len1;
                }
                string dispGraph = "|";
                dispGraph = new String('|', grLen);
                if (dispGraph == "") {
                    dispGraph = "|";
                }
                //Console.WriteLine(item.Name + " " + item.Lines + " " + item.Graph);
                Console.WriteLine(item.Name + " " + item.Lines + " " + dispGraph);  // + " " + grLen.ToString()
            }
            Console.WriteLine(trenn);   // --------------------------------------------

            string disp2 = "Files: " + sumFiles.ToString() + "" + "".PadRight(len, ' ');
            disp2 = disp2.Substring(0, len);
            string dispLines2 = sumLines.ToString();
            dispLines2 = dispLines2.PadLeft(lenCounter);
            string dispLpF = "";
            string dispMinMax = "";
            if (sumLines != 0)
            {
                var nLpF = sumLines / (double)sumFiles;
                dispLpF = ", " + nLpF.ToString("#0.00", System.Globalization.CultureInfo.InvariantCulture) + " Lines/File";

                dispMinMax = ", Min:" + minLines.ToString();
                if (min0Lines == 0)
                {
                    dispMinMax += "/0";
                }
                dispMinMax += " Max:" + maxLines.ToString(); ;
                // ("#,##0.00")
            }
            Console.WriteLine(disp2 + " " + dispLines2 + " Lines" + dispLpF + dispMinMax);

#if DEBUG
            Console.WriteLine("Weiter mit jeder Taste...");
            Console.ReadKey();
#endif
        }
    }

    class DirEntry
    {
        public string Name { get; set; }
        public int LinesNum { get; set; }
        public string Lines { get; set; }
        public string Graph { get; set; }
    }
}
