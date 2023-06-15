using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Strategy
{
    public class ProgressModel
    {
        public IObservable<IUnitProducer> Producer { get; private set; }

        [Inject]
        private void Init(IObservable<ISelectable> selectable)
        {
            Producer = selectable
                .Select(s => s as Component)
                .Select(s => s?.gameObject.GetComponent<IUnitProducer>());
        }
    }
}
