using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static PlasticPipe.PlasticProtocol.Messages.NegotiationCommand;

namespace Strategy
{
    internal class ProduceUnitExecutor : CommandExecutor<IProduceUnitCommand>
    {
        [SerializeField] private Transform _unitsContainer;
        private int _creationDelayMiliseconds = 1000;
        public override void ExecuteSpecific(IProduceUnitCommand command)
        {
            CreateUnit(command.Prefab);
        }

        private async Task CreateUnit(GameObject prefab)
        {
            await Task.Delay(_creationDelayMiliseconds);
            Instantiate(prefab, SetUnitPosition(), Quaternion.identity, _unitsContainer);
        }

        private Vector3 SetUnitPosition() =>
            new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
    }
}
