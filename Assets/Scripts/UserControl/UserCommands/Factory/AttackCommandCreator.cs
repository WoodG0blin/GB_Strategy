using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Strategy
{
    internal sealed class AttackCommandCreator : CommandCreator<IAttackCommand>
    {
        [Inject] private SubscribtableProperty<IDamagable> _target;
        protected override void CreateSpecificCommand(Action<IAttackCommand> callback)
        {
            _target.SubscribeOnValueChange(OnTargetSet);
        }

        private void OnTargetSet(IDamagable target)
        {
            _callback?.Invoke(target != null ? new Attack(target.ToString()) : null);
            _target.UnsubscribeOnValueChange(OnTargetSet);
            _callback = null;
        }
    }
}
