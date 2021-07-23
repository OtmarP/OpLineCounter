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

            Console.WriteLine("Path: " + path + " Pattern: " + pattern + " SubDir: " + subDir);
            string trenn = "--------------------------------------------";
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
                    //throw;
                }

                // 20210428_121314_D_30_ 123456789012
                string disp = fileName;
                var len = 21;
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
                string dispLines = lines.ToString();
                if (lines == 0)
                {
                    dispLines = ".";
                }
                dispLines = dispLines.PadLeft(12);
                Console.WriteLine(disp + " " + dispLines);
            }

            Console.WriteLine(trenn);
#if DEBUG
            Console.ReadKey();
#endif
        }
    }
}
