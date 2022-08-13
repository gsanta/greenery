using game.scene;
using game.scene.level;
using UnityEngine;

namespace game.character.characters.player
{
    public class PlayerManager
    {
        private PlayerStore _playerStore;

        private FollowCamera _camera;

        private PlayerFactory _playerFactory;

        public PlayerManager(PlayerFactory playerFactory, PlayerStore playerStore, FollowCamera camera)
        {
            _playerFactory = playerFactory;
            _playerStore = playerStore;
            _camera = camera;
        }

        public void Start(Level level)
        {
            _playerFactory.Create(level.Grid.Graph.GetRandomWorldPosition());
            
            var player2 = _playerFactory.Create(new Vector3(0, 0, 0));
            player2.GetComponent<SpriteRenderer>().material = new Material(Shader.Find("Shader Graphs/Outline Shader"));

            player2.GetComponent<SpriteRenderer>().material.SetColor("_OutlineColor", new Color(1, 1, 1, 1));
            player2.GetComponent<SpriteRenderer>().material.SetFloat("_OutlineThickness", 1f);


            ActivatePlayer(0);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                ChangePlayer();
            }
        }

        private void ChangePlayer()
        {
            var players = _playerStore.GetAll();
            var index = players.FindIndex((player) => player.IsActive);

            if (index != -1)
            {
                players[index].IsActive = false;

                ActivatePlayer(index == players.Count - 1 ? 0 : index + 1);
            }
        }

        private void ActivatePlayer(int index)
        {
            var players = _playerStore.GetAll();

            var newPlayer = players[index];
            newPlayer.IsActive = true;
            _camera.SetTarget(newPlayer);
        }
    }
}
