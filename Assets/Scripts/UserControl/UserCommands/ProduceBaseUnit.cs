using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy
{
    internal class ProduceBaseUnit : IProduceUnitCommand
    {
        public GameObject Prefab { get; private set; }

        public ProduceBaseUnit(GameObject unit) => Prefab = unit;
    }
}
