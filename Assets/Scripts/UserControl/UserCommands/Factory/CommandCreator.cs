using System;
using System.Threading;
using Zenject;

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

    internal abstract class CancellableCommandCreator<T, TArgument> : CommandCreator<T> where T : ICommand
    {
        [Inject(Id = "RightClick")] private SubscribtableProperty<TArgument> _subscribtableProperty;
        private CancellationTokenSource _cancellationTokenSource;

        protected override async void CreateSpecificCommand(Action<T> callback)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            try
            {
                var argument = await _subscribtableProperty.WithCancellation(_cancellationTokenSource.Token);
                callback?.Invoke(SetCallbackArgument(argument));
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
