using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy
{
    internal class MoveExecutor : CommandExecutor<IMoveCommand>
    {
        public override void ExecuteSpecific(IMoveCommand command)
        {
            Debug.Log($"{transform.name} moves to {command.Direction}");
        }
    }
}
