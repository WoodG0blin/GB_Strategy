using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Strategy
{
    internal class CommandFactory
    {
        private List<ICommandCreator> _creators;
        private ICommandCreator _currentCreator;


        public CommandFactory()
        {
            _creators = new List<ICommandCreator>();
        }

        [Inject]
        private void Init(AttackCommandCreator attack, MoveCommandCreator move, ProduceUnitCommandCreator produce, PatrolCommandCreator patrol, HoldCommandCreator hold)
        {
            _creators.Add(attack);
            _creators.Add(produce);
            _creators.Add(move);
            _creators.Add(patrol);
            _creators.Add(hold);
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
