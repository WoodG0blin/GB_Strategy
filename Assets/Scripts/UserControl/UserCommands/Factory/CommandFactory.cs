using System;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy
{
    internal class CommandFactory
    {  
        private List<ICommandCreator> _creators;
        private ICommandCreator _currentCreator;
        public CommandFactory()
        {
            _creators= new List<ICommandCreator>();
            _creators.Add(new AttackCommandCreator());
            _creators.Add(new MoveCommandCreator());
        }
        public void CreateCommand(ICommandExecutor executor, Action<ICommand> callback)
        {
            for (int i = 0; i < _creators.Count; i++)
            {
                _currentCreator = _creators[i].CreateCommand(executor, callback);
                if (_currentCreator != null) break;
            }
        }

        public void Cancel() => _currentCreator?.Cancel();
    }
}
