using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;

namespace khra_scan
{
    class HostScan
    {
        private string subnet;
        private int start_address;
        private int end_address;
        private int port;
        private int timeout;
        private int running_threads;
        private int Ccount;

        private int Subcount;
        private int end_sub;
        private int start_sub;
        private class isTcpPortOpen
        {
            public TcpClient MainClient { get; set; }
            public bool tcpOpen { get; set; }
        }


        Random rn = new Random(5000);
        public HostScan(string Subnet,string start_sub, string end_sub ,string start_address,string end_address,int port,int timeout)
        {
            this.subnet = Subnet;
            this.start_address = int.Parse(start_address);
            this.end_address = int.Parse(end_address);
            this.start_sub = int.Parse(start_sub);
            this.end_sub = int.Parse(end_sub);
            this.port = port;
            this.timeout = timeout;
            this.Ccount = int.Parse(start_address);
        }

        public void start(int threadCount)
        {
            running_threads = 0;
            Ccount = start_address;
            Subcount = start_sub;


            for (int i = 0; i < threadCount; i++)
            {
                Thread thread = new Thread(new ThreadStart(scan));
                thread.Start();
                running_threads++;
            }
        }

        public void scan()
        {

            string current;

            while ((current = Current_Counter()) != "f")
            {
                
                Thread.Sleep(5);
                
               
                try
                {
                    Connect(subnet + current, port, timeout);

                }
                catch
                {
                    continue;
                }

                Console.WriteLine();
                Console.WriteLine("[+] TCP Port {0} open on {1} ", port, subnet+current) ;
            }
            

            if (Interlocked.Decrement(ref running_threads)==0)
            {
                Console.WriteLine("");
                Console.WriteLine("==================================================================");
                Console.WriteLine("                         ---  Done !! ---");
            }
        }

    
        public string Current_Counter()
        {
            if((end_sub - Subcount) >=0 )
            {
                if ((end_address - Ccount) > 0)
                {
                    Ccount++;
                    return Subcount.ToString() + "." + Ccount.ToString();
                }
                else if ((end_address - Ccount) ==0)
                {
                    Ccount = start_address;
                    Subcount++;
                    return Subcount.ToString() + "." + Ccount.ToString();
                }
            }
            
            return "f";
        }

        public int sub_counter()
        {
            if ((end_sub - Subcount) >= 0)
            {
                return Subcount++;
            }
            return -1;
        }


        public TcpClient Connect(string hostName, int port, int timeout)
        {
            var newClient = new TcpClient();

            var state = new isTcpPortOpen
            {
                MainClient = newClient,
                tcpOpen = true
            };
            
                IAsyncResult ar = newClient.BeginConnect(hostName, port, AsyncCallback, state);
                state.tcpOpen = ar.AsyncWaitHandle.WaitOne(timeout*1000, false);

            
            if (state.tcpOpen == false || newClient.Connected == false)
            {
                throw new Exception();

            }
            return newClient;
        }
        public TcpClient Connect2(string hostName, int port, int timeout)
        {
            var newClient = new TcpClient();

            using (var tcp = new TcpClient())
            {
                var ar = tcp.BeginConnect(hostName, port, null, null);
                using (ar.AsyncWaitHandle)
                {
                    //Wait 2 seconds for connection.
                    if (ar.AsyncWaitHandle.WaitOne(timeout*1000, false))
                    {
                        try
                        {
                            tcp.EndConnect(ar);
                            //Connect was successful.
                        }
                        catch
                        {
                            //EndConnect threw an exception.
                            //serv refused the connection.
                        }
                    }
                    else
                    {
                        //Console.WriteLine("timed out");
                    }
                }

            }
            return newClient;
        }

        void AsyncCallback(IAsyncResult asyncResult)
        {
            var state = (isTcpPortOpen)asyncResult.AsyncState;
            TcpClient client = state.MainClient;

            try
            {
                client.EndConnect(asyncResult);
            }
            catch
            {
                return;
            }

            if (client.Connected && state.tcpOpen)
            {
                return;
            }

            client.Close();
        }
    }
}
