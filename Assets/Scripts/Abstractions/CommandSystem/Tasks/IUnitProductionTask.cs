using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy
{
    public interface IUnitProductionTask
    {
        string UnitName { get; }
        float TimeLeft { get; }
        float ProductionTime { get; }
        Sprite Icon { get; }
    }
}
