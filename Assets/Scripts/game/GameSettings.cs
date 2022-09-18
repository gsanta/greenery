
namespace game
{
    public class GameSettings
    {
        private static GameSettings instance;

        public static GameSettings GetInstance()
        {
            if (instance == null)
            {
                instance = new GameSettings();
            }

            return instance;
        }

        public bool ShowEnemyFieldOfView { get; set; }
    }
}
