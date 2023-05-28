using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy
{
    public abstract class CommandExecutor<T> : MonoBehaviour, ICommandExecutor where T : ICommand
    {
        //[field: SerializeField] public virtual AssetContext Context { get; protected set; }
        public virtual IEnumerable<Type> CommandTypes { get => new Type[] { typeof(T) }; }
        public void Execute(ICommand command) => ExecuteSpecific((T)command);
        public abstract void ExecuteSpecific(T command);
    }
}
