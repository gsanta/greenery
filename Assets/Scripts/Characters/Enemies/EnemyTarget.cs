using System.Collections.Generic;
using UnityEngine;

namespace Characters.Enemies
{
    public class EnemyTarget : MonoBehaviour
    {
        private List<Player> _targetList;

        public Player GetClosest(Vector3 position, float maxRange)
        {
            Player closest = null;

            foreach (var target in _targetList)
            {
                var targetPosition = target.transform.position;
                if (!(Vector3.Distance(position, targetPosition) <= maxRange)) continue;
                
                if (closest == null)
                {
                    closest = target;
                }
                else
                {
                    if (Vector3.Distance(position, targetPosition) <=
                        Vector3.Distance(position, closest.transform.position))
                    {
                        closest = target;
                    }
                }
            }

            return closest;
        }

        public void RemoveTarget(Player target)
        {
            _targetList.Remove(target);
        }

        private Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}