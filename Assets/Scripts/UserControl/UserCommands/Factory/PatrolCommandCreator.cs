using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Strategy
{
    internal sealed class PatrolCommandCreator : CommandCreator<IPatrolCommand>
    {
        [Inject(Id = "RightClickPosition")]
        private SubscribtableProperty<Vector3> _rightClickPosition;

        protected override void CreateSpecificCommand(Action<IPatrolCommand> callback)
        {
            _rightClickPosition.SubscribeOnValueChange(OnRightClick);
        }

        private void OnRightClick(Vector3 position)
        {
            _callback?.Invoke(position != Vector3.zero ? new Patrol(position) : null);
            _rightClickPosition.UnsubscribeOnValueChange(OnRightClick);
            _callback = null;
        }
    }
}
