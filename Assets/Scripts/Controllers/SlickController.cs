using System;
using Assets.Scripts.Intefaces;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Assets.Scripts.Controllers
{
    public class SlickController : IController
    {
    private IView _slickView;
    private Camera _camera;

    private Vector3 _currentPointClick;

    public Action onDeath { get; set; } = delegate { };

    public SlickController(IView view, Camera camera)
    {
        _slickView = view;
        _camera = camera;
    }

    public void Throw(Vector3 direction)
    {
        if (_slickView.IsGround)
        {
            _slickView.Rigidbody.AddForce((_slickView.Transform.forward * direction.z + _slickView.Transform.right *
                -direction.x + _slickView.Transform.up * direction.y) / 5, ForceMode.Impulse);
        }
    }


    public void Dispose()
    {
        _camera = null;
        _currentPointClick = default;
        var oldView = _slickView;
        _slickView = null;
        Object.Destroy(oldView.Transform.gameObject);
        onDeath = null;
    }

    public void Execute()
    {
        var distance = _camera.farClipPlane;
        if (_slickView.Transform.position.y <=
            _camera.ViewportToWorldPoint(new Vector3(0, 0, distance)).y)
        {
            onDeath.Invoke();
        }
    }
    }
}