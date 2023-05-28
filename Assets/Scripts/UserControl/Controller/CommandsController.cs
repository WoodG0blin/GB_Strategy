using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Strategy
{
    internal class CommandsController : IDisposable
    {
        private ICommandExecutor _currentExecutor;
        private IControlsUIView _view;
        public CommandsController(IControlsUIView view)
        {
            _view = view;
            _view.OnCommand += OnCommand;
        }

        public void Dispose()
        {
            _view.OnCommand -= OnCommand;
            _view = null;
        }

        public void OnSelectedChanged(ISelectable selected)
        {
            _view.Clear();
            _currentExecutor = selected as ICommandExecutor;
            _view.SetLayout(_currentExecutor == null ? null : _currentExecutor.CommandTypes);
        }

        private void OnCommand(Type commandType)
        {
            ICommand temp = null;
            if (commandType.IsAssignableFrom(typeof(IProduceUnitCommand))) temp = new ProduceBaseUnit();
            if (commandType.IsAssignableFrom(typeof(IAttackCommand))) temp = new Attack("stub target");
            if (commandType.IsAssignableFrom(typeof(IMoveCommand))) temp = new Move(new Vector3(Random.Range(-10,10), 0, Random.Range(-10,10)));
            if (commandType.IsAssignableFrom(typeof(IPatrolCommand))) temp = new Patrol();
            if (commandType.IsAssignableFrom(typeof(IStopCommand))) temp = new Hold();

            _currentExecutor.Execute(temp);
        }
    }
}
