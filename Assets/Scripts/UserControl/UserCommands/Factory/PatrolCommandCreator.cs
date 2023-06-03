using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Strategy
{
    internal sealed class PatrolCommandCreator : CommandCreator<IPatrolCommand>
    {
        protected override void CreateSpecificCommand(Action<IPatrolCommand> callback)
        {
            callback?.Invoke(new Patrol());
        }
    }
}
