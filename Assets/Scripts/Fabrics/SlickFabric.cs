using Assets.Scripts.Controllers;
using Assets.Scripts.Intefaces;
using UnityEngine;

namespace Assets.Scripts.Fabrics
{
    public class SlickFabric
    {
        public (IView, SlickController) Contruct(Vector3 startPosition, GameObject prefab, Camera camera)
        {
            var view = GameObject.Instantiate(prefab, startPosition, Quaternion.identity)
                .AddComponent<Slick>();
            var controller = new SlickController(view, camera);
            return (view, controller);
        }
    }
}