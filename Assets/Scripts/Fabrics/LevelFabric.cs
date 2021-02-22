using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Fabrics
{
    public class LevelFabric : IDisposable
    {
        private Transform _parentObject;
        
        
        public void Contruct(GameObject prefab, int piecesCount, Vector3 startPosition, float minHeight, float maxHeight, float minWidth, float maxWidth, int voidChance)
        {
            _parentObject = new GameObject("levelLayer").transform;
            var currentPosition = startPosition;
            for (int i = 0; i < piecesCount; i++)
            {
                var piece = Object.Instantiate(prefab, currentPosition, Quaternion.identity, _parentObject);
                var offset = piece.transform.localScale.x;
                currentPosition.x -= offset;
                piece.transform.localScale = new Vector3(Random.Range(minWidth, maxWidth),
                    Random.Range(minHeight, maxHeight),
                    piece.transform.localScale.z);
                if (Random.Range(0, 101) <= voidChance)
                {
                    currentPosition.x -= Random.Range(minWidth, maxWidth);
                }
            }
        }

        public void Dispose()
        {
            Object.Destroy(_parentObject.gameObject);
        }
    }
}