using System;
using System.Numerics;

using Raylib_cs;

using Humper;
using Humper.Responses;

namespace Client
{
    public class Player
    {
        int speed = 200;
        IBox collider;

        public Player(float x, float y)
        {
            collider = MainScreen.World.Create(x, y, 25, 25);
        }

        public void Update(float dt)
        {
            Vector2 velocity = new Vector2();

            if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
                velocity.Y = -1;
            else if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
                velocity.Y = 1;

            if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
                velocity.X = -1;
            else if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
                velocity.X = 1;

            collider.Move(collider.X + velocity.X * speed * dt, collider.Y + velocity.Y * speed * dt, (collision) => CollisionResponses.Slide);
        }

        public void Draw()
        {
            Raylib.DrawRectangle((int)collider.X, (int)collider.Y, (int)collider.Width, (int)collider.Height, Color.RED);
        }
    }
}
