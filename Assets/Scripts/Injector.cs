using UnityEngine;

public class Injector : MonoBehaviour
{
    [SerializeField]
    private DrawScript drawScript;

    [SerializeField]
    private Player player;

    private void Start()
    {
        player.Construct(drawScript);
    }
}
