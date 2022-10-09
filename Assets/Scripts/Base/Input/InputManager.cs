
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Base.Input
{
    public class InputManager : MonoBehaviour
    {

        private List<InputHandler> _handlers = new();

        private InputInfo _inputInfo = new InputInfo();

        private void Update()
        {

            _inputInfo = new InputInfo();

            if (!IsPointerOverUIObject())
            {
                if (UnityEngine.Input.GetMouseButtonDown(0))
                {
                    _inputInfo.IsLeftButtonDown = true;
                }
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
                _handlers.ForEach(handler => {
                    if (!handler.IsDisabled)
                    {
                        handler.OnClick(_inputInfo);
                    }
                });
            }
        }

        public void AddHandler(InputHandler handler)
        {
            _handlers.Add(handler);
        }

        public InputHandler GetHandler(InputHandlerType type)
        {
            return _handlers.Find(handler => handler.Type == type);
        }

        public static bool IsPointerOverUIObject()
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = new Vector2(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 0;
        }
    }
}
