using System.Collections.Generic;

namespace Client
{
    public static class ScreenManager
    {
        private static Screen currentScreen;

        private static List<Screen> screenStack = new List<Screen>();

        public static void Switch(Screen screen)
        {
            if (currentScreen != null)
                currentScreen.Unload();

            currentScreen = screen;
            currentScreen.Load();
        }

        public static void Push(Screen screen)
        {

        }

        public static void Pop()
        {

        }

        public static void Update(float dt)
        {
            currentScreen.Update(dt);
        }

        public static void Draw()
        {
            currentScreen.Draw();
        }
    }
}
