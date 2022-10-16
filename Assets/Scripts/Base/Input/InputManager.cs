
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Base.Input
{
    public class InputManager : MonoBehaviour
    {

        private List<InputHandler> _handlers = new();

        private InputInfo _inputInfo = new InputInfo();

        private InputInfo _prevInputInfo = new InputInfo();

        private void Update()
        {

            _prevInputInfo = _inputInfo;
            _inputInfo = new InputInfo();

            if (!IsPointerOverUIObject())
            {
                if (UnityEngine.Input.GetMouseButtonDown(0))
                {
                    _inputInfo.IsLeftButtonDown = true;
                }

                var pos = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                _inputInfo.xPos = pos.x;
                _inputInfo.yPos = pos.y;
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
                InvokeHandlers((handler) => handler.OnClick(_inputInfo));
            }

            if (_prevInputInfo.xPos != _inputInfo.xPos || _prevInputInfo.yPos != _inputInfo.yPos)
            {
                InvokeHandlers((handler) => handler.OnMouseMove(_inputInfo));
            }
        }

        private void InvokeHandlers(Action<InputHandler> Callback)
        {
            _handlers.ForEach(handler => {
                if (!handler.IsDisabled)
                {
                    Callback(handler);
                }
            });

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
