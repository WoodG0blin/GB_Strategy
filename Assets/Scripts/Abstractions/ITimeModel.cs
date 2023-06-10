using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy
{
    public interface ITimeModel
    {
        IObservable<int> GameTime { get; }
    }
}
