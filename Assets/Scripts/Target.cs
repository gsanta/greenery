using System;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private static List<Target> targetList;

    public static Target GetClosest(Vector3 position, float maxRange)
    {
        Target closest = null;

        foreach (var target in targetList)        
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

    public static void RemoveTarget(Target target)
    {
        targetList.Remove(target);
    }
    
    private void Awake()
    {
        if (targetList == null)
        {
            targetList = new List<Target>();
        }
        
        targetList.Add(this);
    }

    private Vector3 GetPosition()
    {
        return transform.position;
    }
}
