using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Strategy
{
    internal sealed class ProduceUnitCommandCreator : CommandCreator<IProduceUnitCommand>
    {
        [Inject(Id = "Unit")] private IUnitSettings _unit;
        [Inject(Id = "NewUnit")] private IUnitSettings _newUnit;
        private IUnitSettings _currentUnit;
        protected override void CreateSpecificCommand(Action<IProduceUnitCommand> callback)
        {
            _currentUnit = SetUnit();
            callback?.Invoke(new ProduceBaseUnit(_currentUnit));
        }

        private IUnitSettings SetUnit() =>
            _currentUnit == _unit ? _newUnit : _unit;
            
    }
}
