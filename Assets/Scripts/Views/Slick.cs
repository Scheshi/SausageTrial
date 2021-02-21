using Assets.Scripts.Intefaces;
using UnityEngine;
using Views;


[RequireComponent(typeof(Rigidbody))]
public class Slick : MonoBehaviour, IView
{
    private Rigidbody _rigidbody;
    private Transform _transform;
    private bool _isGround;

    public Rigidbody Rigidbody => _rigidbody;
    public Transform Transform => _transform;
    public bool IsGround => _isGround;

    private void Awake()
    {
        TryGetComponent(out _rigidbody);
        _transform = transform;
    }

    private void OnCollisionEnter(Collision other)
    {
        _isGround = true;
    }

    private void OnCollisionExit(Collision other)
    {
        _isGround = false;
    }

    private void OnDestroy()
    {
        _rigidbody = null;
        _transform = null;
        _isGround = default;
    }
}
