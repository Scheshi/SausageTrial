using System;
using UnityEngine;
using Views;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Controllers
{
    public class TargetController : IDisposable
    {
        private Rigidbody _targetPrefab;
        private Transform _barrel;
        private readonly float _maxCooldown;
        private float _currentCooldown;

        public TargetController(Rigidbody targetPrefab, Transform barrel, float cooldown)
        {
            _targetPrefab = targetPrefab;
            _barrel = barrel;
            _maxCooldown = cooldown;
        }

        public void Target(Vector3 direction)
        {
            _currentCooldown -= Time.deltaTime;
            if (_currentCooldown <= 0)
            {
                var test = Object.Instantiate(_targetPrefab, _barrel.position, Quaternion.identity);
                test.velocity = (_barrel.forward * direction.z + _barrel.right * -direction.x + _barrel.up * direction.y) / 5;
                test.gameObject.AddComponent<Target>();
                _currentCooldown = _maxCooldown;
            }
        }


        public void Dispose()
        {
            _targetPrefab = null;
            _barrel = null;
        }
    }
}