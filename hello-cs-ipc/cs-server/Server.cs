using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;

namespace cs_server
{
    class Server
    {
        private string PipeName;
        public Server(string PipeName)
        {
            this.PipeName = PipeName;
        }

        private bool KeepRunning
        {
            get
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Q)
                    return false;
                return true;
            }
        }

        private void StartServerThread()
        {
            Task.Factory.StartNew(() =>
            {
                do
                {
                    try
                    {
                        Console.WriteLine("Starting new server");

                        using (NamedPipeServerStream pipeServer =
                            new NamedPipeServerStream(PipeName, PipeDirection.InOut, 5, PipeTransmissionMode.Message))
                        {
                            Console.WriteLine("NamedPipeServerStream object created.");
                            StreamReader Reader = new StreamReader(pipeServer);
                            //StreamWriter Writer = new StreamWriter(pipeServer);
                            Console.WriteLine("Waiting for client connection...");
                            pipeServer.WaitForConnection();
                            Console.WriteLine("Connected...");
                            do
                            {
                                try
                                {
                                    Console.WriteLine("Waiting to read");
                                    // Reading from named-pipe
                                    string MsgFromClient = Reader.ReadLine();
                                    if (MsgFromClient != null)
                                    {
                                        Console.WriteLine("Msg From Client. ");
                                        Console.WriteLine(MsgFromClient);
                                    }
                                    else
                                    {
                                        Console.WriteLine(string.Format("Read Null string. ", MsgFromClient));
                                    }
                                }
                                // Catch the IOException that is raised if the pipe is broken
                                // or disconnected.
                                catch (IOException ex)
                                {
                                    Console.WriteLine(string.Format("IOException  ERROR: {0}", ex.Message));
                                    break;
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(string.Format("Exception ERROR: {0}", ex.Message));
                                    break;
                                }
                            } while (pipeServer.IsConnected);
                        }
                    }
                    catch (Exception ex1)
                    {
                        Console.WriteLine(string.Format("Exception outer loop - ", ex1.Message));
                    }
                } while (true);
            });
        }

        public void StartServer()
        {
            //NamedPipeServerStream Server = new NamedPipeServerStream("mytestpipe", PipeDirection.InOut, 5, PipeTransmissionMode.Message);
            StartServerThread();
            while (KeepRunning)
            {
                // Do nothing - wait for user to press 'q' key
            }
            Console.WriteLine("\nExiting Server Mode...");
        }
    }
}
