using System;
using System.Collections.Generic;

using Raylib_cs;
using RiptideNetworking;

namespace Client
{
    public class Player
    {
        public static Dictionary<ushort, Player> list = new Dictionary<ushort, Player>();

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

            Message message = Message.Create(MessageSendMode.unreliable, ClientToServerId.Input);
            message.AddBools(inputs, false);
            NetworkManager.Client.Send(message);

            for (int i = 0; i < inputs.Length; i++)
                inputs[i] = false;
        }

        public static void Draw()
        {
            foreach (KeyValuePair<ushort, Player> entry in list)
            {
                Raylib.DrawRectangle(entry.Value.X, entry.Value.Y, 25, 25, Color.RED);
            }
        }

        [MessageHandler((ushort)ServerToClientId.Players)]
        private static void PlayersHandler(Message message)
        {
            ushort[] ids = message.GetUShorts();

            foreach (ushort id in ids)
            {
                Player newPlayer = new Player(id, "George");

                list.TryAdd(id, newPlayer);
            }

            Console.WriteLine("Player added");
        }

        [MessageHandler((ushort)ServerToClientId.PlayerSpawned)]
        private static void PlayerSpawnedHandler(Message message)
        {
            ushort id = message.GetUShort();

            Player newPlayer = new Player(id, message.GetString());

            list.Add(id, newPlayer);

            Console.WriteLine("Player added");
        }

        [MessageHandler((ushort)ServerToClientId.PlayerMovement)]
        private static void PlayerMovementHandler(Message message)
        {
            ushort id = message.GetUShort();

            Player player = list[id];

            player.X = message.GetInt();
            player.Y = message.GetInt();
        }
    }
}
