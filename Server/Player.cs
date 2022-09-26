using System;
using System.Collections.Generic;
using System.Linq;

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

        [MessageHandler((ushort)ClientToServerId.Name)]
        private static void NameHandler(ushort clientId, Message message)
        {
            Player newPlayer = new Player(clientId, message.GetString());

            List.Add(clientId, newPlayer);

            Console.WriteLine($"{newPlayer.Name} connected!");

            ushort[] ids = List.Keys.ToArray();

            Message spawnMessage = Message.Create(MessageSendMode.reliable, ServerToClientId.PlayerSpawned);
            spawnMessage.AddUShort(clientId);
            spawnMessage.AddString(newPlayer.Name);

            NetworkManager.Server.SendToAll(spawnMessage);
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

            Message moveMessage = Message.Create(MessageSendMode.unreliable, ServerToClientId.PlayerMovement);
            moveMessage.AddUShort(clientId);
            moveMessage.AddInt(player.X);
            moveMessage.AddInt(player.Y);

            NetworkManager.Server.SendToAll(moveMessage);
        }
    }
}
