using System.Collections.Generic;

using ImGuiNET;

namespace Server
{
    public class MainScreen
    {
        public static List<string> ConsoleBuffer = new List<string>();

        private int oldLineCount = 0;

        private string commandBuffer = "";

        public static void Log(string text)
        {
            ConsoleBuffer.Add(text);
        }

        public void Load()
        {

        }

        public void Update(float dt)
        {

        }

        public void Draw()
        {
            if (ImGui.BeginMainMenuBar())
            {
                if (ImGui.BeginMenu("Help"))
                {
                    if (ImGui.MenuItem("About"))
                    {

                    }

                    ImGui.EndMenu();
                }

                ImGui.EndMainMenuBar();
            }

            if (ImGui.Begin("Console"))
            {
                foreach (string line in ConsoleBuffer)
                {
                    ImGui.Text(line);
                    ImGui.Separator();

                    if (ConsoleBuffer.Count > oldLineCount)
                    {
                        ImGui.SetScrollY(ConsoleBuffer.Count * 20);
                        oldLineCount = ConsoleBuffer.Count;
                    }
                }
            }
            ImGui.End();

            if (ImGui.Begin("Controls"))
            {
                ImGui.Button("Restart");
                ImGui.Button("Stop");

                ImGui.InputText("Command", ref commandBuffer, 100);
            }
            ImGui.End();

            if (ImGui.Begin("Players"))
            {
                foreach (KeyValuePair<ushort, Player> player in Player.List)
                {
                    ImGui.Text(player.Value.Id.ToString());
                    ImGui.SameLine();
                    ImGui.Text(player.Value.Name);
                    ImGui.Separator();
                }
            }
            ImGui.End();
        }

        public void Unload()
        {

        }
    }
}
