using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy
{
    internal class ProduceUnitExecutor : CommandExecutor<IProduceUnitCommand>
    {
        [SerializeField] private Transform _unitsContainer;
        public override void ExecuteSpecific(IProduceUnitCommand command)
        {
            Instantiate(command.Prefab, SetUnitPosition(), Quaternion.identity, _unitsContainer);
        }
        private Vector3 SetUnitPosition() =>
            new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
    }
}
