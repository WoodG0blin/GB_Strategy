using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using static PlasticPipe.PlasticProtocol.Messages.NegotiationCommand;

namespace Strategy
{
    internal class ProduceUnitExecutor : CommandExecutor<IProduceUnitCommand>, IUnitProducer
    {
        [SerializeField] private Transform _unitsContainer;
        [SerializeField] private int _maxUnitsInQueue = 5;

        public IReadOnlyReactiveCollection<IUnitProductionTask> Queue => _queue;
        private ReactiveCollection<IUnitProductionTask> _queue;

        public void Cancel(int index)
        {
            RemoveAt(index);
        }

        private void Update()
        {
            if (_queue.Count == 0) return;

            var innerTask = (UnitProductionTask)_queue[0];
            innerTask.TimeLeft -= Time.deltaTime;

            if(innerTask.TimeLeft <= 0)
            {
                RemoveAt(0);
                Instantiate(innerTask.Prefab, SetUnitPosition(), Quaternion.identity, _unitsContainer);
            }
        }

        private void RemoveAt(int v)
        {
            for(int i = 0; i< _queue.Count-1; i++) _queue[i] = _queue[i+1];
            _queue.RemoveAt(_queue.Count - 1);
        }

        public override void ExecuteSpecific(IProduceUnitCommand command)
        {
            if (_queue.Count < _maxUnitsInQueue) _queue.Add(new UnitProductionTask(command.UnitSettings));
        }

        private Vector3 SetUnitPosition() =>
            new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
    }
}
