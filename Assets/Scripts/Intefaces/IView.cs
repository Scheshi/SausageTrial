using UnityEngine;

namespace Assets.Scripts.Intefaces
{
    public interface IView
    {
        Rigidbody Rigidbody { get; }
        Transform Transform { get; }
        bool IsGround { get; }
    }
}