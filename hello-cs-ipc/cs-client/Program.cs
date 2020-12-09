using System;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_client
{
    class Program
    {
        static void Main(string[] args)
        {
            //Client
            var client = new NamedPipeClientStream(".", "helloNamedPipe", PipeDirection.InOut);
            client.Connect();
            StreamReader reader = new StreamReader(client);
            StreamWriter writer = new StreamWriter(client);

            while (true)
            {
                string input = Console.ReadLine();
                if (String.IsNullOrEmpty(input)) break;
                writer.WriteLine(input);
                writer.Flush();
            }

            Console.WriteLine("Bye");
            client.Close();
        }
    }
}
