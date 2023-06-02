using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy
{
    public interface ICommandExecutor
    {
        void Execute(ICommand command);
    }
}
