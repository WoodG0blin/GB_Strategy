using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        public CommandsModel()
        {
            _commandsFactory = new CommandFactory();
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
            _executor.Execute(command);
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
