using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Strategy
{
    internal class BaseBuilding : BaseLevelObject
    {
        [SerializeField] private Transform _unitsContainer;
    }
}
