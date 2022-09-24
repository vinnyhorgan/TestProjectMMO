using Raylib_cs;

namespace Client
{
    static class Program
    {
        public static void Main(string[] args)
        {
            Raylib.SetTraceLogLevel(TraceLogLevel.LOG_NONE);
            Raylib.InitWindow(800, 600, "Project MMO");

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();

                Raylib.ClearBackground(Color.SKYBLUE);

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }
    }
}
