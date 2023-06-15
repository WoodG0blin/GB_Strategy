using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy
{
    internal class ProduceBaseUnit : IProduceUnitCommand
    {
        public IUnitSettings UnitSettings { get; private set; }
        public ProduceBaseUnit(IUnitSettings config) => UnitSettings = config;
    }
}
