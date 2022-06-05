using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace khra_scan
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("threads Subnet start_addr end_addrss port timeout");
            //Console.WriteLine("exemple: ");
            //Console.WriteLine("khra-scan.exe 200 192.168.1. 1 254 445 5");
            //Console.WriteLine("==================================================================");
            Console.WriteLine("new format");
            Console.WriteLine("string Subnet,string start_sub, string end_sub ,string start_address,string end_address,int port,int timeout");
            Console.WriteLine("exemple");
            Console.WriteLine("khra-scan.exe 200 192.168. 135 235 1 254 445 3 ");
            Console.WriteLine("==================================================================");
            //HostScan scanner = new HostScan("192.168.1.", "1", "12",445,5);

            //HostScan scanner = new HostScan(args[1], args[2], args[3], int.Parse(args[4]), int.Parse(args[5]));

            HostScan scanner = new HostScan(args[1], args[2], args[3], args[4], args[5], int.Parse(args[6]), int.Parse(args[7]));

            //if (args.Length > 1)
            //    scanner.test();
            //else
            //    

            scanner.start(int.Parse(args[0]));
        }
    }
}
