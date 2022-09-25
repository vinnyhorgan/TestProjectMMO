using Raylib_cs;
using ImGuiNET;

namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const int screenWidth = 640;
            const int screenHeight = 480;

            Raylib.SetTraceLogLevel(TraceLogLevel.LOG_NONE);
            Raylib.SetConfigFlags(ConfigFlags.FLAG_MSAA_4X_HINT);
            Raylib.InitWindow(screenWidth, screenHeight, "Project MMO Server");
            Raylib.SetTargetFPS(60);

            Raylib.InitAudioDevice();

            NetworkManager.Load();

            ImguiController imgui = new ImguiController();
            imgui.Load(screenWidth, screenHeight);

            ImGuiIOPtr io = ImGui.GetIO();
            io.ConfigFlags = ImGuiConfigFlags.DockingEnable;

            MainScreen mainScreen = new MainScreen();

            while (!Raylib.WindowShouldClose())
            {
                float dt = Raylib.GetFrameTime();

                NetworkManager.Update(dt);

                imgui.Update(dt);

                mainScreen.Update(dt);

                Raylib.BeginDrawing();

                Raylib.ClearBackground(Color.BLACK);

                ImGui.DockSpaceOverViewport();

                mainScreen.Draw();

                imgui.Draw();

                Raylib.EndDrawing();
            }

            NetworkManager.Unload();

            imgui.Dispose();

            mainScreen.Unload();

            Raylib.CloseAudioDevice();
            Raylib.CloseWindow();
        }
    }
}
