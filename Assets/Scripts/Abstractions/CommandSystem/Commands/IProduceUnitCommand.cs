using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy
{
    public interface IProduceUnitCommand : ICommand
    {
        [InjectAsset("Unit")] GameObject Prefab { get; }
        //GameObject Prefab { get; }
    }
}
