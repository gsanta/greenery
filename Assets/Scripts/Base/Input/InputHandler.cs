
using System.Collections.Generic;
using UnityEngine;

namespace Base.Input
{
    public class InputHandler : MonoBehaviour
    {

        private List<InputListener> _listeners = new();

        private InputInfo _inputInfo = new InputInfo();

        private void Update()
        {

            _inputInfo = new InputInfo();

            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                _inputInfo.IsLeftButtonDown = true;
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.E))
            {
                _inputInfo.AddKeyDown(KeyCode.E);
            }

            FireEvents();
        }

        private void FireEvents()
        {
            if (_inputInfo.IsLeftButtonDown)
            {
                _listeners.ForEach(listener => {
                    if (!listener.IsDisabled)
                    {
                        listener.OnClick(_inputInfo);
                    }
                });
            }
        }

        public void AddListener(InputListener listener)
        {
            _listeners.Add(listener);
        }

        public void RemvoveListener(InputListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}
