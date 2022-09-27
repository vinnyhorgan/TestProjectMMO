using System.Collections.Generic;

using ImGuiNET;

namespace Server
{
    public class MainScreen
    {
        public static List<string> ConsoleBuffer = new List<string>();

        private int oldLineCount = 0;

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
                if (ImGui.BeginMenu("Console"))
                {
                    if (ImGui.MenuItem("Clear Buffer"))
                    {
                        ConsoleBuffer.Clear();
                        oldLineCount = 0;
                    }

                    ImGui.EndMenu();
                }

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

            if (ImGui.Begin("Network Stats"))
            {
                ImGui.Text($"Bytes Received /s: {NetworkManager.BytesReceivedPerSecond}");
                ImGui.Text($"Bytes Sent /s: {NetworkManager.BytesSentPerSecond}");
                ImGui.Separator();
                ImGui.Text($"Bytes Moved /s: {NetworkManager.BytesReceivedPerSecond + NetworkManager.BytesSentPerSecond}");
            }
            ImGui.End();

            if (ImGui.Begin("Players"))
            {
                foreach (KeyValuePair<ushort, Player> player in Player.List)
                {
                    ImGui.Text(player.Value.Id.ToString());
                    ImGui.SameLine();
                    ImGui.Text(player.Value.Name);
                    ImGui.SameLine();

                    if (ImGui.Button("Kick"))
                    {
                        NetworkManager.Server.DisconnectClient(player.Value.Id);

                        Log($"Kicked {player.Value.Name}");
                    }

                    ImGui.Separator();
                }
            }
            ImGui.End();
        }
    }
}
