using System;
using System.Runtime.CompilerServices;

namespace Strategy
{
    public class SubscribtableProperty<T> : IAwaitable<T>
    {
        public class NewValueNotifier<TAwaited> : IAwaiter<TAwaited>
        {
            private readonly SubscribtableProperty<TAwaited> _subscribtableProperty;
            private TAwaited _result;
            private Action _continuation;
            private bool _isCompleted;

            public NewValueNotifier(SubscribtableProperty<TAwaited> subscribtableProperty)
            {
                _subscribtableProperty= subscribtableProperty;
                _subscribtableProperty.SubscribeOnValueChange(OnNewValue);
            }

            private void OnNewValue(TAwaited value)
            {
                _subscribtableProperty.UnsubscribeOnValueChange(OnNewValue);
                _result = _subscribtableProperty.Value;
                _isCompleted= true;
                _continuation?.Invoke();
            }
            public void OnCompleted(Action continuation)
            {
                if (_isCompleted) continuation?.Invoke();
                else _continuation = continuation;
            }
            public bool IsCompleted => _isCompleted;
            public TAwaited GetResult() => _result;
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

        public IAwaiter<T> GetAwaiter() => new NewValueNotifier<T>(this);
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
