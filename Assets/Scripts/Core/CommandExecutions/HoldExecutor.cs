using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Strategy
{
    internal class HoldExecutor : CommandExecutor<IStopCommand>
    {
        private UnitMovement _unitMovement;
        private void Start()
        {
            if (!TryGetComponent<UnitMovement>(out _unitMovement)) _unitMovement = gameObject.AddComponent<UnitMovement>();
        }
        public override void ExecuteSpecific(IStopCommand command)
        {
            _unitMovement.Stop();
        }
    }
}
