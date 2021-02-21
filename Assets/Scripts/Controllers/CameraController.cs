using Assets.Scripts.Intefaces;
using UnityEngine;


namespace  Assets.Scripts.Controllers
{
    public class CameraController : IExecutable
    {
        private Transform _playerTransform;
        private Transform _camera;
        private Vector3 _offset;

        public CameraController(Transform transform, Camera camera, Vector3 offset)
        {
            _playerTransform = transform;
            _offset = offset;
            _camera = camera.transform;
            _camera.position = _playerTransform.position + _offset;
            _camera.LookAt(_playerTransform);
        }

        public void Execute()
        {
            _camera.position = new Vector3(
                    _playerTransform.position.x + _offset.x, _camera.position.y, _camera.position.z);
        }

        public void Dispose()
        {
            _playerTransform = null;
            _camera = null;
            _offset = default;
        }
    }
}
