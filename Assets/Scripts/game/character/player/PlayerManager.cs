using game.scene.level;

namespace game.character.characters.player
{
    public class PlayerManager
    {
        private PlayerFactory _playerFactory;

        public PlayerManager(PlayerFactory playerFactory)
        {
            _playerFactory = playerFactory;
        }

        public void Start(Level level)
        {
            PathNode randomNode = level.Grid.GetRandomNode();
            var worldPos = level.Grid.GetWorldPosition(randomNode.X, randomNode.Y);
            _playerFactory.Create(worldPos, CharacterType.Cow);

            //var player2 = _playerFactory.Create(new Vector3(0, 0, 0));
            //player2.GetComponent<SpriteRenderer>().material = new Material(Shader.Find("Shader Graphs/Outline Shader"));

            //player2.GetComponent<SpriteRenderer>().material.SetColor("_OutlineColor", new Color(1, 1, 1, 1));
            //player2.GetComponent<SpriteRenderer>().material.SetFloat("_OutlineThickness", 1f);
        }
    }
}
