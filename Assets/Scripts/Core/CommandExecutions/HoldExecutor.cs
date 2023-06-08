using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Strategy
{
    internal class HoldExecutor : CommandExecutor<IStopCommand>
    {
        private NavMeshAgent _navMeshAgent;
        private void Start()
        {
            if (!TryGetComponent<NavMeshAgent>(out _navMeshAgent)) _navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
        }
        public override void ExecuteSpecific(IStopCommand command)
        {
            _navMeshAgent.isStopped = true;
        }
    }
}
