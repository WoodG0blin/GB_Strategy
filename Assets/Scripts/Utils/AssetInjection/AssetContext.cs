using System;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(AssetContext), menuName = "Strategy/"+nameof(AssetContext), order = 0)]
public class AssetContext : ScriptableObject
{
    [SerializeField] private GameObject[] _unitPrefabs;
    public GameObject GetObjectOfType(Type targetType, string name = null)
    {
        for (int i = 0; i<_unitPrefabs.Length; i++)
        {
            var o = _unitPrefabs[i];
            if (o.GetType().IsAssignableFrom(targetType))
            {
                if (name == null || o.name == name) return o;
            }
        }
        return null;
    }
}
