using System;

using Raylib_cs;
using ImGuiNET;
using Humper;

namespace Client
{
    public class MainScreen : Screen
    {
        public static World World = new World(800, 600);

        Player player = new Player(100, 100);

        public override void Load()
        {

        }

        public override void Update(float dt)
        {
            player.Update(dt);

            if (ImGui.Begin("Test"))
            {
                ImGui.Text("Hello!");
            }
        }

        public override void Draw()
        {
            Raylib.DrawText("FPS: " + Raylib.GetFPS(), 10, 10, 30, Color.WHITE);

            player.Draw();
        }
    }
}
