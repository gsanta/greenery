using UnityEngine;

namespace Characters
{
    public interface IMoveAble
    {
        Vector3 GetPosition();
        void SetTarget(Vector3 position);
    }
}