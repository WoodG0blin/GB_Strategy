using System;
using UniRx;

namespace Strategy
{
    public abstract class Subscribtable<T, TValue>: IObservable<TValue> where T : IObservable<TValue>
    {
        protected T observable;

        public IDisposable Subscribe(IObserver<TValue> observer) => observable.Subscribe(observer);
    }

    public abstract class SubscribtableStateless<T> : Subscribtable<Subject<T>, T>
    {
        private Subject<T> _innerValue = new Subject<T>();
        public SubscribtableStateless() => observable = _innerValue;
        public T Value { set => _innerValue.OnNext(value); }
    }
    public abstract class SubscribtableStatefull<T> : Subscribtable<ReactiveProperty<T>, T>
    {
        private ReactiveProperty<T> _innerValue = new ReactiveProperty<T>();
        public SubscribtableStatefull() => observable = _innerValue;
        public T Value { get => _innerValue.Value; set => _innerValue.Value = value; }
    }
}
