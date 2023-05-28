using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy
{
    internal class AttackExecutor : CommandExecutor<IAttackCommand>
    {
        public override void ExecuteSpecific(IAttackCommand command)
        {
            Debug.Log($"{transform.name} attacks {command.Target}");
        }
    }
}
