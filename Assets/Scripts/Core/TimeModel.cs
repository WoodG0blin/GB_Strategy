using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Strategy
{
    internal class TimeModel : ITimeModel, ITickable
    {
        private ReactiveProperty<float> _gameTime = new ReactiveProperty<float>();
        public IObservable<int> GameTime => _gameTime.Select(f => (int)f);

        public void Tick()
        {
            _gameTime.Value += Time.deltaTime;
        }
    }
}
