using System.Collections.Generic;

using RiptideNetworking;

using Raylib_cs;

namespace Server
{
    public class Player
    {
        public static Dictionary<ushort, Player> List = new Dictionary<ushort, Player>();

        public ushort Id;
        public string Name;
        public int X = 0;
        public int Y = 0;

        public Player(ushort id, string name)
        {
            Id = id;
            Name = name;
        }

        [MessageHandler((ushort)ClientToServerId.Connect)]
        private static void ConnectHandler(ushort clientId, Message message)
        {
            Player newPlayer = new Player(clientId, message.GetString());

            List.Add(clientId, newPlayer);

            MainScreen.Log($"{newPlayer.Name} connected!");

            foreach (KeyValuePair<ushort, Player> entry in List)
            {
                Message playersMessage = Message.Create(MessageSendMode.reliable, ServerToClientId.Spawn);
                playersMessage.AddUShort(entry.Value.Id);
                playersMessage.AddString(entry.Value.Name);
                playersMessage.AddInt(entry.Value.X);
                playersMessage.AddInt(entry.Value.Y);

                NetworkManager.BytesSentCounter += playersMessage.WrittenLength;

                NetworkManager.Server.Send(playersMessage, clientId);
            }

            Message newPlayerMessage = Message.Create(MessageSendMode.reliable, ServerToClientId.Spawn);
            newPlayerMessage.AddUShort(newPlayer.Id);
            newPlayerMessage.AddString(newPlayer.Name);

            NetworkManager.BytesSentCounter += newPlayerMessage.WrittenLength;

            NetworkManager.Server.SendToAll(newPlayerMessage, clientId);
        }

        [MessageHandler((ushort)ClientToServerId.Input)]
        private static void InputHandler(ushort clientId, Message message)
        {
            bool[] inputs = message.GetBools(4);

            Player player = List[clientId];

            float dt = Raylib.GetFrameTime();

            if (inputs[0])
                player.Y -= (int)(500 * dt);

            if (inputs[1])
                player.X -= (int)(500 * dt);

            if (inputs[2])
                player.Y += (int)(500 * dt);

            if (inputs[3])
                player.X += (int)(500 * dt);

            Message moveMessage = Message.Create(MessageSendMode.unreliable, ServerToClientId.Movement);
            moveMessage.AddUShort(clientId);
            moveMessage.AddInt(player.X);
            moveMessage.AddInt(player.Y);

            NetworkManager.BytesSentCounter += moveMessage.WrittenLength;

            NetworkManager.Server.SendToAll(moveMessage);
        }
    }
}
