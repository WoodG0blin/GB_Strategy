using System;
using System.Runtime.CompilerServices;
using UniRx;

namespace Strategy
{
    public class SubscribtableProperty<T> : IAwaitable<T>
    {
        public class NewValueNotifier : Awaiter<SubscribtableProperty<T>, T>
        {
            public NewValueNotifier(SubscribtableProperty<T> awaitable) : base(awaitable) { }
            protected override T ReceiveResult() => _awaitable.Value;
            protected override IDisposable SubscribeOnAwaitable(Action<T> subscription) { _awaitable.SubscribeOnValueChange(subscription); return default;}
            protected override void UnSubscribeFromAwaitable(Action<T> onNewValue) => _awaitable.UnsubscribeOnValueChange(onNewValue);
        }

        protected T _value;
        protected Action<T> _onValueChange;

        public virtual T Value
        {
            get => _value;
            set
            {
                _value = value;
                _onValueChange?.Invoke(_value);
            }
        }
        public void SubscribeOnValueChange(Action<T> onValueChange) => _onValueChange += onValueChange;
        public void UnsubscribeOnValueChange(Action<T> onValueChange) => _onValueChange -= onValueChange;

        public IAwaiter<T> GetAwaiter() => new NewValueNotifier(this);
    }

    public class ReactivePropertyAsync<T> : ReactiveProperty<T>, IAwaitable<T>
    {
        public class NewValueNotifier : Awaiter<ReactivePropertyAsync<T>, T>
        {
            public NewValueNotifier(ReactivePropertyAsync<T> awaitable) : base(awaitable) { }
            protected override T ReceiveResult() => _awaitable.Value;
            protected override IDisposable SubscribeOnAwaitable(Action<T> subscription) => _awaitable.Subscribe(subscription);
        }
        public IAwaiter<T> GetAwaiter() => new NewValueNotifier(this);
    }

    public class SubscriptableTrigger : SubscribtableProperty<bool>
    {
        public override bool Value
        {
            get => _value;
            set
            {
                if (!_value.Equals(value))
                {
                    _value = value;
                    _onValueChange?.Invoke(_value);
                    _value = !value;
                }
            }
        }
    }

    public class SubscribtablePropertyWithEqualsCheck<T> : SubscribtableProperty<T>
    {
        public override T Value
        {
            get => _value;
            set
            {
                if (!_value.Equals(value))
                {
                _value = value;
                _onValueChange?.Invoke(_value);
                }
            }
        }
    }
}
