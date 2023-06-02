using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy
{
    internal interface ICommandCreator
    {
        void Cancel();
        ICommandCreator CreateCommand(ICommandExecutor executor, Action<ICommand> callback);
    }

    internal abstract class CommandCreator<T> : ICommandCreator where T : ICommand
    {
        protected Action<ICommand> _callback;
        public ICommandCreator CreateCommand(ICommandExecutor executor, Action<ICommand> callback)
        {
            var specifiedExecutor = executor as CommandExecutor<T>;
            if (specifiedExecutor != null)
            {
                _callback = callback;
                CreateSpecificCommand(type => callback.Invoke(type));
                return this;
            }
            return null;
        }

        protected abstract void CreateSpecificCommand(Action<T> callback);
        public virtual void Cancel() => _callback = null;
    }
}
