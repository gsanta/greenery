

using System.Collections.Generic;
using UnityEngine;

namespace Base.Input
{
    public struct InputInfo
    {

        private HashSet<KeyCode> downKeys;

        public bool IsLeftButtonDown { get; set; }

        public float xPos;

        public float yPos;

        public bool IsTabPressed { get; set; }

        public bool IsScrollUp { get; set; }

        public void AddKeyDown(KeyCode keyCode)
        {
            downKeys.Add(keyCode);
        }

        public bool IsKeyDown(KeyCode keyCode)
        {
            return downKeys.Contains(keyCode);
        }
    }
}
