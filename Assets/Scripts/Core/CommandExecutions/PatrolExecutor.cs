using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Strategy
{
    internal class PatrolExecutor : CommandExecutor<IPatrolCommand>
    {
        private NavMeshAgent _navMeshAgent;
        private List<Vector3> _patrolPoints;
        private int _patrolPointCounter = 0;
        private Vector3 _target;
        private void Start()
        {
            if (!TryGetComponent<NavMeshAgent>(out _navMeshAgent)) _navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
            _patrolPoints= new List<Vector3>();
        }
        public override void ExecuteSpecific(IPatrolCommand command)
        {
            _patrolPoints = new List<Vector3>();
            _patrolPoints.Add(new Vector3(transform.position.x, 0, transform.position.z));
            _patrolPoints.Add(command.PatrolTarget);
            ChangeDestination();
        }

        private void ChangeDestination()
        {
            _patrolPointCounter++;
            if(_patrolPointCounter >= _patrolPoints.Count) _patrolPointCounter = 0;
            _target = _patrolPoints[_patrolPointCounter];
            _navMeshAgent.SetDestination(_target);
        }

        private void FixedUpdate()
        {
            if(ReachedDestination(new Vector3(transform.position.x, 0, transform.position.z)) && _patrolPoints.Count > 1) ChangeDestination();
        }

        private bool ReachedDestination(Vector3 position) => Vector3.SqrMagnitude(_target - position) < Mathf.Pow(_navMeshAgent.stoppingDistance, 2)+0.1f;
    }
}
