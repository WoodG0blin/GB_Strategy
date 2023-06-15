using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy
{
    internal class UnitProductionTask : IUnitProductionTask
    {
        public string UnitName {get; private set;}
        public float TimeLeft {get; set;}
        public float ProductionTime {get; private set;}
        public Sprite Icon {get; private set;}
        public GameObject Prefab { get; private set; }

        public UnitProductionTask(IUnitSettings config)
        {
            UnitName = config.Name;
            ProductionTime = config.ProductionTime;
            Icon = config.Icon;
            Prefab = config.Prefab;
        }
    }
}
