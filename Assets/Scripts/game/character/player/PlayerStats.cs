using UnityEngine;

namespace game.character.player
{
    public class PlayerStats
    {
        public int Life { get; set; }

        public int MaxLife { get; private set; }
        
        public PlayerStats(int life)
        {
            Life = life;
            MaxLife = life;
        }
    }
}