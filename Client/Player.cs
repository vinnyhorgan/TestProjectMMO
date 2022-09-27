using System;
using System.Collections.Generic;

using Raylib_cs;
using RiptideNetworking;

namespace Client
{
    public class Player
    {
        public static Dictionary<ushort, Player> List = new Dictionary<ushort, Player>();

        static bool[] inputs = new bool[4];

        public ushort Id;
        public string Name;
        public int X = 0;
        public int Y = 0;

        public Player(ushort id, string name)
        {
            Id = id;
            Name = name;
        }

        public static void Update(float dt)
        {
            if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
                inputs[0] = true;

            if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
                inputs[1] = true;

            if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
                inputs[2] = true;

            if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
                inputs[3] = true;

            Message inputMessage = Message.Create(MessageSendMode.unreliable, ClientToServerId.Input);
            inputMessage.AddBools(inputs, false);

            NetworkManager.Client.Send(inputMessage);

            for (int i = 0; i < inputs.Length; i++)
                inputs[i] = false;
        }

        public static void Draw()
        {
            foreach (KeyValuePair<ushort, Player> entry in List)
            {
                Raylib.DrawRectangle(entry.Value.X, entry.Value.Y, 25, 25, Color.RED);
            }
        }

        [MessageHandler((ushort)ServerToClientId.Spawn)]
        private static void SpawnHandler(Message message)
        {
            ushort id = message.GetUShort();
            string name = message.GetString();

            Player newPlayer = new Player(id, name);

            List.Add(id, newPlayer);
        }

        [MessageHandler((ushort)ServerToClientId.Disconnect)]
        private static void DisconnectHandler(Message message)
        {
            ushort id = message.GetUShort();

            List.Remove(id);
        }

        [MessageHandler((ushort)ServerToClientId.Movement)]
        private static void MovementHandler(Message message)
        {
            ushort id = message.GetUShort();

            if (List.ContainsKey(id))
            {
                Player player = Player.List[id];

                player.X = message.GetInt();
                player.Y = message.GetInt();
            }
        }
    }
}
