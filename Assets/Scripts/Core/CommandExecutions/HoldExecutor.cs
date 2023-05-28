using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy
{
    internal class HoldExecutor : CommandExecutor<IStopCommand>
    {
        public override void ExecuteSpecific(IStopCommand command)
        {
            Debug.Log($"{transform.name} holds position");
        }
    }
}
