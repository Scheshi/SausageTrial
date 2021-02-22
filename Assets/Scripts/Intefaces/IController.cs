using System;
using UnityEngine;

namespace Assets.Scripts.Intefaces
{
    public interface IController : IDisposable, IExecutable
    {
        Action onDeath { get; set; }

        void Throw(Vector3 direction);
    }
}