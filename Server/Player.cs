using System;
using System.Collections.Generic;

using RiptideNetworking;

namespace Server
{
    public class Player
    {
        public static Dictionary<ushort, Player> list = new Dictionary<ushort, Player>();

        public ushort Id;
        public string Username;

        [MessageHandler(1)]
        private static void Name(ushort clientId, Message message)
        {
            Player newPlayer = new Player();
            newPlayer.Id = clientId;
            newPlayer.Username = message.GetString();

            list.Add(clientId, newPlayer);

            Console.WriteLine($"{newPlayer.Username} connected!");
        }
    }
}
