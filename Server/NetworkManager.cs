using RiptideNetworking;
using RiptideNetworking.Utils;

namespace Server
{
    public enum ClientToServerId : ushort
    {
        Connect = 1,
        Input
    }

    public enum ServerToClientId : ushort
    {
        Spawn = 1,
        Movement,
        Disconnect
    }

    public static class NetworkManager
    {
        public static RiptideNetworking.Server Server;

        public static int BytesReceivedPerSecond = 0;
        public static int BytesSentPerSecond = 0;
        public static int BytesReceivedCounter = 0;
        public static int BytesSentCounter = 0;

        private static float tickrate = 0.1f;
        private static float timer1 = 0f;
        private static float timer2 = 0f;

        public static void Load()
        {
            RiptideLogger.Initialize(MainScreen.Log, true);

            Server = new();
            Server.Start(1234, 100);

            Server.ClientDisconnected += (s, e) => {
                Player.List.Remove(e.Id);

                Message disconnectMessage = Message.Create(MessageSendMode.reliable, ServerToClientId.Disconnect);
                disconnectMessage.AddUShort(e.Id);

                NetworkManager.BytesSentCounter += disconnectMessage.WrittenLength;

                Server.SendToAll(disconnectMessage);
            };

            Server.MessageReceived += (s, e) => {
                BytesReceivedCounter += e.Message.WrittenLength;
            };
        }

        public static void Update(float dt)
        {
            timer1 += dt;
            timer2 += dt;

            if (timer1 > tickrate)
            {
                Server.Tick();

                timer1 = 0;
            }

            if (timer2 > 1)
            {
                BytesReceivedPerSecond = BytesReceivedCounter;
                BytesReceivedCounter = 0;

                BytesSentPerSecond = BytesSentCounter;
                BytesSentCounter = 0;

                timer2 = 0;
            }
        }

        public static void Unload()
        {
            Server.Stop();
        }
    }
}
