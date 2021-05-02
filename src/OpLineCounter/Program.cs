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
        // So.02.05.2021 16:39:26 -op- nach .\GitHub\OtmarP\OpLineCounter\src\OpLineCounter\
        // Mi.28.04.2021 09:35:40 -op- Cr (4.7.2)
        //--------------------------------------------

        static void Main(string[] args)
        {
            string path = @".\";
            string pattern = "*.*";
#if DEBUG
            path = @"C:\Dev\TFS\Git\OpTools\MSPartnerCenterDownloader\MSPartnerCenter\bin\Debug\DATA";
            path = @"C:\Dev\Temp\MSPartnerCenter\DATA\20210426";
            //path = @".\";
            pattern = "*_*_*_*_*.*";
#endif

            if (args.Length > 0) {
                pattern = args[0];
            }

            string trenn = "--------------------------------------------";
            Console.WriteLine(trenn);
            var dirList = System.IO.Directory.GetFiles(path, pattern, System.IO.SearchOption.TopDirectoryOnly);
            for (int i = 0; i < dirList.Length; i++)
            {
                string fullFileName = dirList[i];

                string fileName = System.IO.Path.GetFileName(fullFileName);


                var countLines = System.IO.File.ReadAllLines(fullFileName);

                // 20210428_121314_D_30_ 123456789012
                string disp = fileName;
                var len = 21;
                if (disp.Length > len)
                {
                    disp = disp.Substring(0, len ) ;
                }
                else
                {
                    disp = disp + "".PadRight(len);
                    disp = disp.Substring(0, len );
                }

                disp = disp.Trim() +""+ "".PadRight(len,' ');
                disp = disp.Substring(0, len);

                //+ " ............................................................................";
                int lines = countLines.Length - 1;
                string dispLines = lines.ToString();
                if (lines == 0) {
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
