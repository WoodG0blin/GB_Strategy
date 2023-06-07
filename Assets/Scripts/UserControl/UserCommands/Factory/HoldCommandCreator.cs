using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Strategy
{
    internal sealed class HoldCommandCreator : CommandCreator<IStopCommand>
    {
        protected override void CreateSpecificCommand(Action<IStopCommand> callback)
        {
            callback?.Invoke(new Hold());
        }
    }
}
