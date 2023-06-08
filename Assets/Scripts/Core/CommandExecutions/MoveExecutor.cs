using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Strategy
{
    internal class MoveExecutor : CommandExecutor<IMoveCommand>
    {
        private NavMeshAgent _navMeshAgent;
        private void Start()
        {
            if(!TryGetComponent<NavMeshAgent>(out _navMeshAgent)) _navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
        }
        public override void ExecuteSpecific(IMoveCommand command)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(command.Direction);
        }
    }
}
