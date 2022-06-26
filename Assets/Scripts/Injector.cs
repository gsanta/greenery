using GameInfo;
using Items;
using Players;
using UnityEngine;

public class Injector : MonoBehaviour
{
    [SerializeField] private GameInfoStore gameInfoStore;

    [SerializeField] private Player player;

    [SerializeField] private BallSpawner ballSpawner;

    private readonly ItemStore<Ball> _ballStore = new();

    private void Awake()
    {
        player.GetComponent<ItemPickup>().Construct(_ballStore, gameInfoStore);
        player.GetComponent<LineDrawer>().Construct(gameInfoStore);
        ballSpawner.Construct(_ballStore);
    }
}
