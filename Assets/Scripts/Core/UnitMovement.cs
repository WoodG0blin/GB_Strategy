using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Strategy
{
    internal class UnitMovement : MonoBehaviour, IAwaitable<AsyncExtensions.Void>
    {
        internal class StopAwaiter : Awaiter<UnitMovement, AsyncExtensions.Void>
        {
            public StopAwaiter(UnitMovement awaitable) : base(awaitable) { }
            protected override AsyncExtensions.Void ReceiveResult() => new AsyncExtensions.Void();
            protected override IDisposable SubscribeOnAwaitable(Action<AsyncExtensions.Void> subscription)
            {
                _awaitable.OnStop += subscription;
                return default;
            }
            protected override void UnSubscribeFromAwaitable(Action<AsyncExtensions.Void> onNewValue) =>
                _awaitable.OnStop -= onNewValue;
        }

        public event Action<AsyncExtensions.Void> OnStop;
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;

        public async void MoveTo(Vector3 destination)
        {
            Stop();
            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(destination);
            _animator.SetTrigger("Walk");
            await this;
            _animator.SetTrigger("Idle");
        }

        public void Stop()
        {
            _navMeshAgent.isStopped = true;
            OnStop?.Invoke(new AsyncExtensions.Void());
        }
        public bool IsStopped => _navMeshAgent.isStopped;
       
        void Start()
        {
            if (!TryGetComponent<NavMeshAgent>(out _navMeshAgent)) _navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
            if (!TryGetComponent<Animator>(out _animator)) _animator = gameObject.AddComponent<Animator>();
        }

        void FixedUpdate()
        {
            if(!_navMeshAgent.pathPending)
            {
                if(_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                {
                    if(_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0)
                        OnStop?.Invoke(new AsyncExtensions.Void());
                }
            }
        }

        public IAwaiter<AsyncExtensions.Void> GetAwaiter() => new StopAwaiter(this);
    }
}
