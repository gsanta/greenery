namespace game.scene.tile
{
    public class TileModule
    {
        public TileMapBase TileMapBase { get; }

        public TileModule(TileMapBase tileMapBase)
        {
            TileMapBase = tileMapBase;
        }
    }
}