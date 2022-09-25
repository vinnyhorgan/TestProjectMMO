using System;
using System.Numerics;

using Raylib_cs;
using Humper;
using Humper.Responses;
using RiptideNetworking;

namespace Client
{
    public class Player
    {
        // int speed = 200;
        IBox collider;

        bool[] inputs = new bool[4];

        public Player(float x, float y)
        {
            collider = MainScreen.World.Create(x, y, 25, 25);

            Message message = Message.Create(MessageSendMode.reliable, 1);
            message.AddString("Jhonny Be Goode");
            NetworkManager.Client.Send(message);
        }

        public void Update(float dt)
        {
            // Vector2 velocity = new Vector2();

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

            // Console.WriteLine("BYTES: " + message.WrittenLength);

            NetworkManager.Client.Send(message);

            for (int i = 0; i < inputs.Length; i++)
                inputs[i] = false;

            // collider.Move(collider.X + velocity.X * speed * dt, collider.Y + velocity.Y * speed * dt, (collision) => CollisionResponses.Slide);
        }

        public void Draw()
        {
            Raylib.DrawRectangle((int)collider.X, (int)collider.Y, (int)collider.Width, (int)collider.Height, Color.RED);
        }
    }
}
