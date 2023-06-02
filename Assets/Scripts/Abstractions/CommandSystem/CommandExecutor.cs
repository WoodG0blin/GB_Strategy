using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy
{
    public abstract class CommandExecutor<T> : MonoBehaviour, ICommandExecutor where T : ICommand
    {
        public void Execute(ICommand command)
        {
            if(typeof(T).IsAssignableFrom(command.GetType())) ExecuteSpecific((T)command);
        }
        public abstract void ExecuteSpecific(T command);
    }
}
