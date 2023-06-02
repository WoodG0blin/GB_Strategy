using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy
{
    public interface ICommandExecutor
    {
        IEnumerable<Type> CommandTypes { get; }
        void Execute(ICommand command);
    }
}
