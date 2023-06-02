using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy
{
    internal sealed class AttackCommandCreator : CommandCreator<IAttackCommand>
    {
        protected override void CreateSpecificCommand(Action<IAttackCommand> callback)
        {
            callback?.Invoke(new Attack("Stub target"));
        }
    }
}
