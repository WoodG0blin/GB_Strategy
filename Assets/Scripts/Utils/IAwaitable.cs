using System;
using System.Runtime.CompilerServices;
using UnityEngine;

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
        private IDisposable _subscription;
        private bool next = false;

        public Awaiter(T awaitable)
        {
            _awaitable = awaitable;
            _subscription = SubscribeOnAwaitable(OnNewValue);
        }

        private void OnNewValue(TResult value)
        {
            if (!next)
            {
                next = true;
                return;
            }
            UnSubscribeFromAwaitable(OnNewValue);
            _result = ReceiveResult();
            _subscription?.Dispose();
            _isCompleted = true;
            _continuation?.Invoke();
        }

        protected abstract TResult ReceiveResult();
        protected abstract IDisposable SubscribeOnAwaitable(Action<TResult> subscription);
        protected virtual void UnSubscribeFromAwaitable(Action<TResult> onNewValue) { }

        public void OnCompleted(Action continuation)
        {
            if (_isCompleted) continuation?.Invoke();
            else _continuation = continuation;
        }
        public bool IsCompleted => _isCompleted;
        public TResult GetResult() => _result;
    }
}