using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Strategy
{
    public interface IUnitProducer
    {
        IReadOnlyReactiveCollection<IUnitProductionTask> Queue { get; }
        void Cancel(int index);
    }
}
