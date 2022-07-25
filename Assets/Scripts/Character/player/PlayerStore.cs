using System.Collections.Generic;
using Characters;
using UnityEngine;

namespace Character.player
{
    public class PlayerStore : MonoBehaviour, ICharacterStore<ICharacter>
    {
        private readonly List<Player> _players = new();

        public List<ICharacter> GetAll()
        {
            return _players.ConvertAll<ICharacter>(player => player);
        }

        public void Add(ICharacter player)
        {
            _players.Add((Player) player);
        }

        public void Remove(ICharacter player)
        {
            _players.Remove((Player) player);
        }

        public Player GetActivePlayer()
        {
            return _players[0];
        }

        public void DestroyAll()
        {
            foreach (var player in _players)
            {
                Destroy(player);
            }
        }
    }
}