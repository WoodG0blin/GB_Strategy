using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy
{
    internal class ProduceBaseUnit : IProduceUnitCommand
    {
        [InjectAsset("Unit")] public GameObject Prefab { get; private set; }

        //[InjectAsset("Unit")] private GameObject _prefab;
    }
}
