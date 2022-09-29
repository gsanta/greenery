
using game.scene.grid;

namespace game.scene.level.obstacle
{
    public interface IObstacleCalculator
    {
        void Calculate(GridGraph<PathNode> grid);
    }
}
