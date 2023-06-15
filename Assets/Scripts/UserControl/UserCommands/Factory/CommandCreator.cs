using System;
using System.Threading;
using Zenject;
using UnityEngine;
using UniRx;
using System.Threading.Tasks;
using System.Data;

namespace Strategy
{
    internal interface ICommandCreator
    {
        void Cancel();
        ICommandCreator CreateCommand(ICommandExecutor executor, Action<ICommand> callback);
    }

    internal abstract class CommandCreator<T> : ICommandCreator where T : ICommand
    {
        public ICommandCreator CreateCommand(ICommandExecutor executor, Action<ICommand> callback)
        {
            var specifiedExecutor = executor as CommandExecutor<T>;
            if (specifiedExecutor != null)
            {
                CreateSpecificCommand(type => callback.Invoke(type));
                return this;
            }
            return null;
        }

        protected abstract void CreateSpecificCommand(Action<T> callback);
        public virtual void Cancel() { }
    }

    internal abstract class CancellableCommandCreator<T, TObserver, TArgument> : CommandCreator<T> where T : ICommand where TObserver : IObservable<TArgument>
    {
        [Inject] private TObserver _subscribtableProperty;
        private CancellationTokenSource _cancellationTokenSource;

        protected override async void CreateSpecificCommand(Action<T> callback)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            try
            {
                callback?.Invoke(SetCallbackArgument(
                    await _subscribtableProperty.First().ToTask(_cancellationTokenSource.Token)
                    ));
            }
            catch { }
        }

        private T SetCallbackArgument(TArgument argument) =>
            argument == null ? default(T) : NewCommand(argument);

        protected abstract T NewCommand(TArgument argument);
        public override void Cancel()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }
        }
    }
}
