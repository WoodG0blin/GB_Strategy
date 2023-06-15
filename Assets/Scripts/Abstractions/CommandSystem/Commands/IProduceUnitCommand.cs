using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy
{
    public interface IProduceUnitCommand : ICommand
    {
        IUnitSettings UnitSettings { get; }
    }
}
