using System;

using RiptideNetworking;
using RiptideNetworking.Utils;

namespace Client
{
    public static class NetworkManager
    {
        private static string ip = "127.0.0.1";
        private static ushort port = 1234;

        private static RiptideNetworking.Client client;

        public static void Load()
        {
            RiptideLogger.Initialize(Console.WriteLine, true);

            client = new();
            client.Connect($"{ip}:{port}");
        }

        public static void Update()
        {
            client.Tick();
        }

        public static void Unload()
        {
            client.Disconnect();
        }
    }
}
