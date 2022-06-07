using System;
using NDesk.Options;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace khra_scan
{
    class Program
    {
        static void Main(string[] args)
        {

            bool show_help = false;
            List<string> names = new List<string>();
            int repeat = 1;

            var p = new OptionSet() {
            { "n|name=", "the {NAME} of someone to greet.",
              v => names.Add (v) },
            { "r|repeat=",
                "the number of {TIMES} to repeat the greeting.\n" +
                    "this must be an integer.",
              (int v) => repeat = v },
            { "h|help",  "show this message and exit",
              v => show_help = v != null },
        };

            List<string> extra;
            try
            {
                extra = p.Parse(args);
            }
            catch (OptionException e)
            {
                Console.Write("error: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try 'khra-scan.exe --help' for more information.");
                return;
            }

            if (show_help)
            {
                //ShowHelp(p);
                ShowHelp(p);
                return;
            }

            foreach(string name in names)
            {
                Console.WriteLine(name);
            }

            /////      old code 

            //Console.WriteLine("==================================================================");
            //Console.WriteLine("string Subnet,string start_sub, string end_sub ,string start_address,string end_address,int port,int timeout");
            //Console.WriteLine("exemple");
            //Console.WriteLine("khra-scan.exe 200 192.168. 135 235 1 254 445 3 ");
            //Console.WriteLine("==================================================================");

            //if ( args.Length != 8 )
            //{
            //    Console.WriteLine("");
            //    Console.WriteLine(" [!] some args are missing");
            //    Environment.Exit(0);
            //}
            //HostScan scanner = new HostScan(args[1], args[2], args[3], args[4], args[5], int.Parse(args[6]), int.Parse(args[7]));

            //scanner.start(int.Parse(args[0]));
        }
        static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("==================================================================");
            Console.WriteLine("string Subnet,string start_sub, string end_sub ,string start_address,string end_address,int port,int timeout");
            Console.WriteLine("exemple");
            Console.WriteLine("khra-scan.exe 200 192.168. 135 235 1 254 445 3 ");
            Console.WriteLine("==================================================================");
            p.WriteOptionDescriptions(Console.Out);
            Console.WriteLine("==================================================================");


        }
        static void HostScan()
        {
            // default values
        }

        static void PortScan()
        {
            //will eventually do it xD
        }
    }
}
