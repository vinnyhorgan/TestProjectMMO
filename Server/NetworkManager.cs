using System;

using RiptideNetworking.Utils;

namespace Server
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
        public static RiptideNetworking.Server Server;

        static float tickrate = 0.1f;
        static float timer = 0f;

        public static void Load()
        {
            RiptideLogger.Initialize(Console.WriteLine, true);

            Server = new();
            Server.Start(1234, 10);

            Server.ClientDisconnected += (s, e) => Player.list.Remove(e.Id);
        }

        public static void Update(float dt)
        {
            timer += dt;

            if (timer > tickrate)
            {
                Server.Tick();
                Console.WriteLine("Ticked!");

                timer = 0;
            }
        }

        public static void Unload()
        {
            Server.Stop();
        }
    }
}
