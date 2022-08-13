﻿using UnityEngine;

namespace game.weapon.bomb
{
    public class BombLifeTime : MonoBehaviour
    {
        public float lifeTime = 0.3f;

        private void Start()
        {
            Destroy(gameObject, lifeTime);
        }
    }
}
