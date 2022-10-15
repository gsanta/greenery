
using game.scene.grid;
using UnityEngine;

public class DebugContainer : MonoBehaviour
{
    public static DebugContainer Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public GridVisualizer gridVisualizer;
}
