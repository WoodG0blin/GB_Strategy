using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Strategy
{
    internal sealed class MoveCommandCreator : CommandCreator<IMoveCommand>
    {
        protected override void CreateSpecificCommand(Action<IMoveCommand> callback)
        {
            callback?.Invoke(new Move(new Vector3(Random.Range(-10,10), 0, Random.Range(-10, 10))));
        }
    }
}
