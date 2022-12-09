using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace net_scan
{
    class PortScan
    {
        private class isTcpPortOpen
        {
            public TcpClient MainClient { get; set; }
            public bool tcpOpen { get; set; }
        }


        private string subnet;
        private int start_port;
        private int end_port;
        private int port;
        private int timeout;
        private int running_threads;
        private int current;
        private int thread_count;

        private IPAddress ip;
       
        public PortScan(string host, int port, int threadcount, int timeout)
        {
            this.end_port = port;
            if (!IPAddress.TryParse(host, out ip))
            {
                Console.WriteLine(" [!] provided address is not valid");
                return;
            }
            this.start_port = 0;
            this.timeout = timeout;
            this.thread_count = threadcount;
        }
        public void start()
        {
            running_threads = 0;
            current = start_port;
            


            for (int i = 0; i < thread_count; i++)
            {
                Thread thread = new Thread(new ThreadStart(Scan));
                thread.Start();
                running_threads++;
            }
        }

        
        public void Scan()
        {

            int current_port;
            while((current_port = Counter()) != -1 )
            {
                
                Thread.Sleep(5);
                try
                {
                    
                    Connect(ip.ToString(), current_port, timeout);
                    
                }
                catch (Exception)
                {

                    continue;
                }
                Console.WriteLine();
                Console.WriteLine("[+] TCP Port {0} open on {1} ", current_port, ip.ToString());
            }
            if (Interlocked.Decrement(ref running_threads) == 0)
            {
                Console.WriteLine("==================================================================");
                Console.WriteLine("                        --- Done !! ---");
            }
        }


        private static readonly object lck = new object();
        public int Counter()
        {
            lock(lck)
            {
                if (end_port > current)
                {
                    Interlocked.Increment(ref current);
                    return current;
                }
                return -1;
            }
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
            state.tcpOpen = ar.AsyncWaitHandle.WaitOne(timeout * 1000, false);


            if (state.tcpOpen == false || newClient.Connected == false)
            {
                throw new Exception();

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
