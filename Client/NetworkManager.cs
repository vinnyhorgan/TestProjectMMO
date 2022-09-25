using System;

using RiptideNetworking;
using RiptideNetworking.Utils;

namespace Client
{
    public enum ClientToServerId : ushort
    {
        Name = 1,
        Input
    }

    public enum ServerToClientId : ushort
    {

    }

    public static class NetworkManager
    {
        public static RiptideNetworking.Client Client;

        static float tickrate = 0.1f;
        static float timer = 0f;

        public static void Load()
        {
            RiptideLogger.Initialize(Console.WriteLine, true);

            Client = new();
            Client.Connect("127.0.0.1:1234");
        }

        public static void Update(float dt)
        {
            timer += dt;

            if (timer > tickrate)
            {
                Client.Tick();
                Console.WriteLine("Ticked!");

                timer = 0;
            }
        }

        public static void Unload()
        {
            Client.Disconnect();
        }
    }
}
