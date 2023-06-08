using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Strategy
{
    internal class PatrolExecutor : CommandExecutor<IPatrolCommand>
    {
        private UnitMovement _unitMovement;
        private List<Vector3> _patrolPoints;
        private int _patrolPointCounter = 0;
        private Vector3 _target;
        private void Start()
        {
            if (!TryGetComponent<UnitMovement>(out _unitMovement)) _unitMovement = gameObject.AddComponent<UnitMovement>();
            _patrolPoints = new List<Vector3>();
        }
        public override void ExecuteSpecific(IPatrolCommand command)
        {
            _patrolPoints = new List<Vector3>();
            _patrolPoints.Add(new Vector3(transform.position.x, 0, transform.position.z));
            _patrolPoints.Add(command.PatrolTarget);
            ChangeDestination();
        }

        private async void ChangeDestination()
        {
            _patrolPointCounter++;
            if(_patrolPointCounter >= _patrolPoints.Count) _patrolPointCounter = 0;
            _target = _patrolPoints[_patrolPointCounter];
            _unitMovement.MoveTo(_target);
            await _unitMovement;
            if(!_unitMovement.IsStopped) ChangeDestination();
        }
    }
}
