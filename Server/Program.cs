using Raylib_cs;
using ImGuiNET;

namespace Server
{
    class Program
    {
        public static void Main(string[] args)
        {
            const int screenWidth = 800;
            const int screenHeight = 600;

            Raylib.SetTraceLogLevel(TraceLogLevel.LOG_NONE);
            Raylib.SetConfigFlags(ConfigFlags.FLAG_MSAA_4X_HINT);
            Raylib.InitWindow(screenWidth, screenHeight, "Project MMO Server");
            Raylib.SetTargetFPS(60);
            Raylib.SetExitKey(KeyboardKey.KEY_NULL);

            NetworkManager.Load();

            ImguiController imgui = new ImguiController();

            ImGuiIOPtr io = ImGui.GetIO();
            io.ConfigFlags = ImGuiConfigFlags.DockingEnable;

            imgui.Load(screenWidth, screenHeight);

            MainScreen mainScreen = new MainScreen();

            while (!Raylib.WindowShouldClose())
            {
                float dt = Raylib.GetFrameTime();

                NetworkManager.Update(dt);

                mainScreen.Update(dt);

                imgui.Update(dt);

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

            Raylib.CloseWindow();
        }
    }
}
