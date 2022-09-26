namespace Client
{
    public class MainScreen : Screen
    {
        public override void Load()
        {

        }

        public override void Update(float dt)
        {
            Player.Update(dt);
        }

        public override void Draw()
        {
            Player.Draw();
        }
    }
}
