using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Strategy
{
    internal class MoveExecutor : CommandExecutor<IMoveCommand>
    {
        private UnitMovement _unitMovement;
        private void Start()
        {
            if (!TryGetComponent<UnitMovement>(out _unitMovement)) _unitMovement = gameObject.AddComponent<UnitMovement>();
        }
        public override void ExecuteSpecific(IMoveCommand command)
        {
            _unitMovement.MoveTo(command.Direction);
        }
    }
}
