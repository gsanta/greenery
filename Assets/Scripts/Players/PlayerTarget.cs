using System.Collections.Generic;
using UnityEngine;

namespace Players
{
    public class PlayerTarget : MonoBehaviour
    {
        private List<PlayerTarget> _targetList;

        public PlayerTarget GetClosest(Vector3 position, float maxRange)
        {
            PlayerTarget closest = null;

            foreach (var target in _targetList)        
            {
                if (Vector3.Distance(position, target.GetPosition()) <= maxRange)
                {
                    if (closest == null)
                    {
                        closest = target;
                    }
                    else
                    {
                        if (Vector3.Distance(position, target.GetPosition()) <=
                            Vector3.Distance(position, closest.GetPosition()))
                        {
                            closest = target;
                        }
                    }
                }
            }

            return closest;
        }

        public void RemoveTarget(PlayerTarget playerTarget)
        {
            _targetList.Remove(playerTarget);
        }

        private Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}
