using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy
{
    public interface IUnitSettings
    {
        Sprite Icon { get; }
        string Name { get; }
        GameObject Prefab { get; }
        float ProductionTime { get; }
    }

    [CreateAssetMenu(fileName = nameof(UnitSettings), menuName = "Configs/" + nameof(UnitSettings), order = 0)]
    public class UnitSettings : ScriptableObject, IUnitSettings
    {
        [field: SerializeField] public string Name { get; protected set; }
        [field: SerializeField] public GameObject Prefab { get; protected set; }
        [field: SerializeField] public Sprite Icon { get; protected set; }
        [field: SerializeField, Range(0f, 60f)] public float ProductionTime { get; protected set; }
    }
}
