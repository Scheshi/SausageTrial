using System;
using Assets.Scripts.Intefaces;
using UnityEngine;


namespace Assets.Scripts.Controllers
{
    public class InputController : IDisposable, IExecutable
    {
        private Action<Vector3> _onThrow = delegate(Vector3 vector) { };
        private Action<Vector3> _onTarget = delegate(Vector3 vector3) { };
        private Vector3 _currentPointClick;
        private Vector3 _direction;

        public void AddSlickController(SlickController controller)
        {
            _onThrow += controller.Throw;
        }

        public void RemoveSlickController(SlickController controller)
        {
            _onThrow -= controller.Throw;
        }

        public void AddTargetController(TargetController controller)
        {
            _onTarget += controller.Target;
        }

        public void RemoveTargetController(TargetController controller)
        {
            _onTarget -= controller.Target;
        }


    public void Execute()
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0)) _currentPointClick = Input.mousePosition;
            if (Input.GetMouseButton(0))
            {
                _direction = Input.mousePosition - _currentPointClick;
                _onTarget.Invoke(-_direction);
            }

            if (Input.GetMouseButtonUp(0))
            {
                _onThrow.Invoke(-_direction);
            }
#else
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _currentPointClick = touch.position;
                        break;
                    case TouchPhase.Moved:
                    case TouchPhase.Stationary:
                        _direction = touch.position - (Vector2)_currentPointClick;
                        _onTarget.Invoke(-_direction);
                        break;
                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        _onThrow.Invoke(-_direction);
                        break;
                }
            }
#endif
        }

        public void Dispose()
        {
            _onThrow = delegate(Vector3 vector) { };
            _onTarget = delegate(Vector3 vector) { };
        }
    }
}