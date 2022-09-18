using game.character.ability.field_of_view;
using game.character.characters.enemy;
using game.character.characters.player;
using UnityEngine;

namespace game.character.enemy
{
    public class FovVisualDecorator : MonoBehaviour, EnemyDecorator
    {
        [SerializeField] private FieldOfViewVisualizer fieldOfViewPrefab;

        public static bool IsOn = false;

        public static bool PrevIsOn = false;

        public static string DecoratorName = "Fov Visual";

        public string Name => "Fov Visual";

        private PlayerStore _playerStore;

        public void Construct(PlayerStore playerStore)
        {
            _playerStore = playerStore;
        }

        public void Apply(Enemy enemy)
        {
            var fieldOfViewVisualizer = Instantiate(fieldOfViewPrefab, new Vector3(0, 0, 0), transform.rotation);
            fieldOfViewVisualizer.Construct(enemy.FieldOfView, enemy, _playerStore);
            enemy.AddDestroyable(fieldOfViewVisualizer.gameObject);
        }

        public void Remove(Enemy enemy)
        {
            var fovVisualizer = enemy.GetDestroyables().Find((destroyable) => destroyable.GetComponent<FieldOfViewVisualizer>() != null);

            if (fovVisualizer)
            {
                enemy.RemoveDestoyable(fovVisualizer);
                Destroy(fovVisualizer);
            }
        }
    }
}
