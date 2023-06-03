using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Strategy
{
    internal sealed class ProduceUnitCommandCreator : CommandCreator<IProduceUnitCommand>
    {
        protected override void CreateSpecificCommand(Action<IProduceUnitCommand> callback)
        {
            callback?.Invoke(new ProduceBaseUnit());
        }
    }
}
