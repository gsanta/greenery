
using game.character.characters.enemy;

namespace game.character.enemy
{
    public interface EnemyDecorator
    {
        public string Name { get; }

        public void Apply(Enemy enemy);

        public void Remove(Enemy enemy);
    }
}
