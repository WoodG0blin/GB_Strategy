using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Strategy
{
    internal sealed class MoveCommandCreator : CommandCreator<IMoveCommand>
    {
        [Inject(Id = "RightClickPosition")]
        private SubscribtableProperty<Vector3> _rightClickPosition;

        protected override void CreateSpecificCommand(Action<IMoveCommand> callback)
        {
            _rightClickPosition.SubscribeOnValueChange(OnRightClick);
        }

        private void OnRightClick(Vector3 position)
        {
            _callback?.Invoke(position != Vector3.zero ? new Move(position) : null);
            _rightClickPosition.UnsubscribeOnValueChange(OnRightClick);
            _callback = null;
        }
    }
}
