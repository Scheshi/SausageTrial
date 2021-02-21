using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Fabrics
{
    public class LevelFabric : IDisposable
    {
        private Transform _parentObject;
        
        
        public void Contruct(GameObject prefab, int piecesCount, Vector3 startPosition, float minHeight, float maxHeight, int voidChance)
        {
            _parentObject = new GameObject("levelLayer").transform;
            var offset = prefab.transform.localScale.x;
            var currentPosition = startPosition;
            for (int i = 0; i < piecesCount; i++)
            {
                var piece = Object.Instantiate(prefab, currentPosition, Quaternion.identity, _parentObject);
                currentPosition.x -= offset;
                piece.transform.localScale = new Vector3(offset,
                    Random.Range(minHeight, maxHeight),
                    piece.transform.localScale.z);
                if (Random.Range(0, 100) <= voidChance)
                {
                    currentPosition.x -= offset;
                }
            }
        }

        public void Dispose()
        {
            Object.Destroy(_parentObject.gameObject);
        }
    }
}