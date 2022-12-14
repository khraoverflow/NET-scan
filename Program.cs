using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;

namespace net_scan
{
    class Program
    {

        static int thread_count = 100;
        static string range;
        static string subnet;
        static string start_sub;
        static string end_sub;
        static string start_address;
        static string end_address;
        static IPAddress host;
        
        static int port;
        static int timeout;

        static Dictionary<string,string> ParamsName = new Dictionary<string, string>();
        static void Main(string[] args)
        {

            ParamsName.Add("-th","Thread count, default 200");
            ParamsName.Add("-p","the Port number to scan for in hosts discovery mode");
            ParamsName.Add("-t","timeout in seconds , default 2s");
            ParamsName.Add("-r","range to scan in hosts scan mode, exemple 10.0.128-254.1-254");
            ParamsName.Add("-h","ip address of the target host to scan ");
            ParamsName.Add("-pr","ports range to scan on port scanner mode, default 1-65535");

            Console.WriteLine("==================================================================");
            Console.WriteLine("                  ----     NET scanner     ----");
            Console.WriteLine("==================================================================");
            //Console.WriteLine("threads Subnet start_addr end_addrss port timeout");
            //Console.WriteLine("exemple: ");
            //Console.WriteLine("khra-scan.exe 200 192.168.1. 1 254 445 5");
            //Console.WriteLine("==================================================================");
            
            /*Console.WriteLine("new format");
            Console.WriteLine("string Subnet,string start_sub, string end_sub ,string start_address,string end_address,int port,int timeout");
            Console.WriteLine("exemple");
            Console.WriteLine("khra-scan.exe 200 192.168. 135 235 1 254 445 3 ");
            Console.WriteLine("==================================================================");
            */

            //HostScan scanner = new HostScan("192.168.1.", "1", "12",445,5);

            //HostScan scanner = new HostScan(args[1], args[2], args[3], int.Parse(args[4]), int.Parse(args[5]));

          

           // HostScan scanner = new HostScan(args[1], args[2], args[3], args[4], args[5], int.Parse(args[6]), int.Parse(args[7]));

   

            //scanner.start(int.Parse(args[0]));
            if (args.Length == 0)
            {
                ShowHelp();
                return;
            }
            ParseMode(args);
            //HostScanner();
        }

        static void ParseMode(string[] args)
        {
            string mode = args[0].ToLower();
            
              /*if(args.Length < 8)
            {
                Console.WriteLine("arguments error");
                return;
            }*/

            switch (mode)
            {
                case "hosts":
                    Console.WriteLine("starting host discovery ...");
                    ParseParamVal(args);
                    HostScanner();
                    break;
                case "ports":
                    Console.WriteLine("starting port scanner :");
                    ParseParamForPortScan(args);
                    PortScanner();
                    break;
                case "-h" :
                case "--help":
                    ShowHelp();
                    break;
                case "-test":
                    Debugging(args);
                    break;
                default:
                    Console.WriteLine("mode {0} not found try -h or--help",args[0]);
                    break;
            }
        }

        static void Debugging(string[] args)
        {
            range = ParamValue(args,"-r",true,"0");
               //TESTS
            Console.WriteLine("threads: " + thread_count);
            Console.WriteLine("port: " + port);
            Console.WriteLine("timeout: " + timeout);
            //Console.WriteLine("range: " + range);

        }
        static void ShowHelp()
        {
            
            Console.WriteLine("                       Hosts discovery mode ");
            Console.WriteLine("     use 'khra-scan.exe hosts' to scan for hosts with the following arguments: ");
            Console.WriteLine("");
            Console.WriteLine("     command exemple: ");
            Console.WriteLine("     khra-scan.exe hosts -r 192.168.1-2.1-254 -p 445");
            Console.WriteLine("     => scans from 192.168.1.1 to 192.168.2.254");
            Console.WriteLine("        on port 445");
            Console.WriteLine("        with 200 threads (default)");
            Console.WriteLine("        2 seconds timeout (default)");
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine("                         argumentes");
            Console.WriteLine("");
            foreach (var param in ParamsName)
            {
                Console.WriteLine("  " + param.Key + "  " + param.Value);
            }
        }

        private static void ParseParamVal(string[] args)
        {
            /*Console.WriteLine("threads: " + ParamValue(args,"-Th"));
            Console.WriteLine("range: " + ParamValue(args,"-R"));*/

            //thread count
            if (!int.TryParse(ParamValue(args,"-th",false,"200"),out thread_count))
            {
                Console.WriteLine(" [!] thread count should be an integer");
                return;
            }
            
            
            //port
            if(!int.TryParse(ParamValue(args,"-p",true,"0"),out port))
            {
                Console.WriteLine(" [!] port number must be integer");
                return;
            }
            //timeout
            if(!int.TryParse(ParamValue(args,"-t",false,"2"),out timeout))
            {
                Console.WriteLine(" [!] timeoute number must be integer");
                return;
            }
            //range 

            ParseRange(ParamValue(args,"-r",true,"0"));

            
         

        }

        private static void ParseParamForPortScan(string[] args)
        {
            /*Console.WriteLine("threads: " + ParamValue(args,"-Th"));
            Console.WriteLine("range: " + ParamValue(args,"-R"));*/

            //thread count
            if (!int.TryParse(ParamValue(args, "-th", false, "200"), out thread_count))
            {
                Console.WriteLine(" [!] thread count should be an integer");
                return;
            }

            //port
            if (!int.TryParse(ParamValue(args, "-p", true, "0"), out port))
            {
                Console.WriteLine(" [!] port number must be integer");
                return;
            }

            //host
            if (!IPAddress.TryParse(ParamValue(args, "-h", true, "0"), out host))
            {
                Console.WriteLine(" [!] host address not valid");
                return;
            }

            //timeout
            if (!int.TryParse(ParamValue(args, "-t", false, "2"), out timeout))
            {
                Console.WriteLine(" [!] timeoute number must be integer");
                return;
            }
        }

        static void HostScanner()
        {
            HostScan scanner = new HostScan(subnet,start_sub,end_sub,start_address,end_address,port,timeout);
            scanner.start(thread_count);
            while (!scanner.finished)
            {
                Thread.Sleep(100);
            }

        }

        static void PortScanner()
        {
            PortScan scanner = new PortScan(host.ToString(), port, thread_count, timeout);
            scanner.start();
            while (!scanner.finished)
            {
                Thread.Sleep(100);
            }
        }

        static string ParamValue(string[] args,string param,bool IsMand,string deflt)
        {
            if(Array.IndexOf(args,param) == -1 )
            {
                if(IsMand)
                {
                    Console.WriteLine(" [!] Failed to find mandotory argument {0} for: {1}",param, ParamsName[param]);
                    return"";
                }
               return deflt;
            }
            return args[Array.IndexOf(args,param) + 1];
        }

        static void ParseRange(string range)
        {
            // stupid but works ? aint stupid ...lazy ? maybe xD
            try
            {
                subnet = range.Split('.')[0]+'.'+range.Split('.')[1]+'.';
                start_address = range.Split('.')[3].Split('-')[0];
                end_address = range.Split('.')[3].Split('-')[1];
                if(range.Split('.')[2].Contains('-'))
                {
                    start_sub = range.Split('.')[2].Split('-')[0];
                    end_sub = range.Split('.')[2].Split('-')[1];
                }
                else
                {
                    start_sub =  end_sub = range.Split('.')[2];
                   
                }
               
                
            }
            catch
            {
                Console.WriteLine(" [!] address range wrong check help for correct format");
            }
            
        }
    }
}
