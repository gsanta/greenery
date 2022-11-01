using game.scene;
using game.scene.grid;
using game.scene.level;
using game.weapon;
using System;

namespace game.character.characters.player
{
    public class PlayerManager
    {
        private PlayerFactory _playerFactory;

        PlayerStore _playerStore;

        private FollowCamera _camera;

        private LevelStore _levelStore;

        private ScopedTileRenderer _tileRenderer;

        private WeaponHandler _weaponHandler;

        public PlayerManager(PlayerFactory playerFactory, PlayerStore playerStore, LevelStore levelStore, ScopedTileRenderer tileRenderer, FollowCamera camera, WeaponHandler weaponHandler)
        {
            _playerFactory = playerFactory;
            _levelStore = levelStore;
            _playerStore = playerStore;
            _tileRenderer = tileRenderer;
            _weaponHandler = weaponHandler;

            _playerStore.OnActivePlayerChange += HandleActivePlayerChange;
            _camera = camera;
        }

        public void Activate()
        {
            _tileRenderer.Show();
            _weaponHandler.SetActive(true);


            var level = _levelStore.ActiveLevel;
            PathNode randomNode = level.Grid.GetRandomNode();
            var worldPos = level.Grid.GetWorldPosition(randomNode.X, randomNode.Y);
            _playerFactory.Create(worldPos, CharacterType.Cow, level);

            randomNode = level.Grid.GetRandomNode();
            worldPos = level.Grid.GetWorldPosition(randomNode.X, randomNode.Y);
            var player = _playerFactory.Create(worldPos, CharacterType.Cow, level);
            _playerStore.SetCurrentPlayer(player);
            //var player2 = _playerFactory.Create(new Vector3(0, 0, 0));
            //player2.GetComponent<SpriteRenderer>().material = new Material(Shader.Find("Shader Graphs/Outline Shader"));

            //player2.GetComponent<SpriteRenderer>().material.SetColor("_OutlineColor", new Color(1, 1, 1, 1));
            //player2.GetComponent<SpriteRenderer>().material.SetFloat("_OutlineThickness", 1f);
        }

        private void HandleActivePlayerChange(object sender, EventArgs args)
        {
            _camera.SetTarget(_playerStore.GetCurrentPlayer());
        }
    }
}
