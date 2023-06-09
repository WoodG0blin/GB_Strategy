using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Strategy
{
    internal interface ICommandsModel
    {
        event Action<ICommandExecutor> OnCommandChosen;
        event Action OnCommandCanceled;
        event Action OnCommandIssued;

        void OnCommandButtonClicked(ICommandExecutor executor);
        void OnSelectionChanged();
    }

    internal class CommandsModel : ICommandsModel
    {
        public event Action<ICommandExecutor> OnCommandChosen;
        public event Action OnCommandIssued;
        public event Action OnCommandCanceled;

        private CommandFactory _commandsFactory;
        private ICommandExecutor _executor;

        private bool _pending;

        [Inject]
        private void Init(CommandFactory factory)
        {
            _commandsFactory = factory;
        }

        public void OnCommandButtonClicked(ICommandExecutor executor)
        {
            if (_pending) CancelProcess();

            _executor = executor;
            _pending = true;
            OnCommandChosen?.Invoke(executor);

            _commandsFactory.CreateCommand(executor, OnCommandCreated);
        }

        public void OnSelectionChanged()
        {
            _pending = false;
            CancelProcess();
        }

        private void OnCommandCreated(ICommand command)
        {
            if(command != null) _executor.Execute(command);
            _pending = false;
            OnCommandIssued?.Invoke();
        }

        private void CancelProcess()
        {
            _commandsFactory.Cancel();
            OnCommandCanceled?.Invoke();
        }
    }
}
