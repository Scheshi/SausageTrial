using System;

namespace Assets.Scripts.Intefaces
{
    public interface IController
    {
        Action onDeath { get; set; }
    }
}