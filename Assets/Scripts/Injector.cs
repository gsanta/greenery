using Items;
using Players;
using UnityEngine;

public class Injector : MonoBehaviour
{
    [SerializeField]
    private DrawScript drawScript;

    [SerializeField]
    private Player player;

    [SerializeField] private BallSpawner ballSpawner;

    private readonly ItemStore<Ball> _ballStore = new();

    private void Awake()
    {
        player.Construct(drawScript);
        player.GetComponent<ItemPickup>().Construct(_ballStore);
        ballSpawner.Construct(_ballStore);
    }
}
