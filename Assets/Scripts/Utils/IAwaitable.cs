using System;
using System.Net.Sockets;
using System.Runtime.CompilerServices;

namespace Strategy
{
    public interface IAwaitable<T>
    {
        IAwaiter<T> GetAwaiter();
    }

    public interface IAwaiter<TAwaited> : INotifyCompletion
    {
        bool IsCompleted { get; }
        TAwaited GetResult();
    }

    public abstract class Awaiter<T, TResult> : IAwaiter<TResult> where T : IAwaitable<TResult>
    {
        protected T _awaitable;
        private TResult _result;
        private Action _continuation;
        private bool _isCompleted;

        public Awaiter(T awaitable)
        {
            _awaitable = awaitable;
            SubscribeOnAwaitable(OnNewValue);
        }

        private void OnNewValue(TResult value)
        {
            UnSubscribeFromAwaitable(OnNewValue);
            _result = ReceiveResult();
            _isCompleted = true;
            _continuation?.Invoke();
        }

        protected abstract TResult ReceiveResult();
        protected abstract void SubscribeOnAwaitable(Action<TResult> subscription);
        protected abstract void UnSubscribeFromAwaitable(Action<TResult> onNewValue);

        public void OnCompleted(Action continuation)
        {
            if (_isCompleted) continuation?.Invoke();
            else _continuation = continuation;
        }
        public bool IsCompleted => _isCompleted;
        public TResult GetResult() => _result;
    }
}