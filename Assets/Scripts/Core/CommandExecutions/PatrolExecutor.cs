using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy
{
    internal class PatrolExecutor : CommandExecutor<IPatrolCommand>
    {
        public override void ExecuteSpecific(IPatrolCommand command)
        {
            Debug.Log($"{transform.name} starts on patrol from {transform.position} to {command.PatrolTarget}");
        }
    }
}
