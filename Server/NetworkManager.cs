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
        PlayerSpawned = 1,
        PlayerMovement,
        Players
    }

    public static class NetworkManager
    {
        public static RiptideNetworking.Server Server;

        private static float tickrate = 0.1f;
        private static float timer = 0f;

        public static void Load()
        {
            RiptideLogger.Initialize(MainScreen.Log, true);

            Server = new();
            Server.Start(1234, 100);

            Server.ClientDisconnected += (s, e) => Player.List.Remove(e.Id);
        }

        public static void Update(float dt)
        {
            timer += dt;

            if (timer > tickrate)
            {
                Server.Tick();

                timer = 0;
            }
        }

        public static void Unload()
        {
            Server.Stop();
        }
    }
}
