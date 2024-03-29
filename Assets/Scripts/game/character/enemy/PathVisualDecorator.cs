﻿
using game.character.characters.enemy;
using game.scene.grid;
using game.scene.grid.path;
using UnityEngine;

namespace game.character.enemy
{
    public class PathVisualDecorator : MonoBehaviour, EnemyDecorator
    {
        [SerializeField] private PathVisualizer pathVisualizerPrefab;

        public static bool IsOn = false;

        public static bool PrevIsOn = false;

        public static string DecoratorName = "Path Visual";

        public string Name => "Path Visual";

        private LineRenderer lineRenderer;

        private void Start()
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.positionCount = 2;
        }

        public void Apply(Enemy enemy)
        {
            var pathVisualizer = Instantiate(pathVisualizerPrefab, new Vector3(0, 0, 0), transform.rotation);
            pathVisualizer.SetLevel(enemy.Level);
            pathVisualizer.SetPathMovement(enemy);
            enemy.AddDestroyable(pathVisualizer.gameObject);
        }

        public void Remove(Enemy enemy)
        {
            var pathVisualizer = enemy.GetDestroyables().Find((destroyable) => destroyable.GetComponent<PathVisualizer>() != null);

            if (pathVisualizer)
            {
                enemy.RemoveDestoyable(pathVisualizer);
                Destroy(pathVisualizer);
            }
        }
    }
}
