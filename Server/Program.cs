using System;
using System.Threading;

using RiptideNetworking;
using RiptideNetworking.Utils;

namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            RiptideLogger.Initialize(Console.WriteLine, true);

            RiptideNetworking.Server server = new();
            server.Start(1234, 10);

            while (true)
            {
                server.Tick();

                Thread.Sleep(10);
            }
        }
    }
}
