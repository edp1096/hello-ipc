using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_server
{
    class Program
    {
        private const string PipeName = "helloNamedPipe";
        static void Main(string[] args)
        {
            Console.WriteLine("Running in SERVER mode");
            Console.WriteLine("Press 'q' to exit");
            Server ServerInstance = new Server(PipeName);
            ServerInstance.StartServer();
        }
    }
}
