using game.scene;
using game.scene.level;
using System;

namespace game.character.characters.player
{
    public class PlayerManager
    {
        private PlayerFactory _playerFactory;

        PlayerStore _playerStore;

        private FollowCamera _camera;

        private LevelStore _levelStore;

        public PlayerManager(PlayerFactory playerFactory, PlayerStore playerStore, LevelStore levelStore, FollowCamera camera)
        {
            _playerFactory = playerFactory;
            _levelStore = levelStore;
            _playerStore = playerStore;

            _playerStore.OnActivePlayerChange += HandleActivePlayerChange;
            _camera = camera;
        }

        public void Init()
        {
            var level = _levelStore.ActiveLevel;
            PathNode randomNode = level.Grid.GetRandomNode();
            var worldPos = level.Grid.GetWorldPosition(randomNode.X, randomNode.Y);
            _playerFactory.Create(worldPos, CharacterType.Cow);

            randomNode = level.Grid.GetRandomNode();
            worldPos = level.Grid.GetWorldPosition(randomNode.X, randomNode.Y);
            var player = _playerFactory.Create(worldPos, CharacterType.Cow);
            _playerStore.SetActivePlayer(player);
            //var player2 = _playerFactory.Create(new Vector3(0, 0, 0));
            //player2.GetComponent<SpriteRenderer>().material = new Material(Shader.Find("Shader Graphs/Outline Shader"));

            //player2.GetComponent<SpriteRenderer>().material.SetColor("_OutlineColor", new Color(1, 1, 1, 1));
            //player2.GetComponent<SpriteRenderer>().material.SetFloat("_OutlineThickness", 1f);
        }

        private void HandleActivePlayerChange(object sender, EventArgs args)
        {
            _camera.SetTarget(_playerStore.GetActivePlayer());
        }
    }
}
