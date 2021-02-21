using Assets.Scripts.Intefaces;
using UnityEngine;


namespace Views
{
    public class Target : MonoBehaviour
    {
        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if(!other.TryGetComponent<Slick>(out _))
                Destroy(gameObject);
        }
    }
}