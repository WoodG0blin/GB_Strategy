using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Strategy
{
    internal interface IUserControlsModel
    {
        ReactivePropertyAsync<ISelectable> CurrentSelected { get; }
        ReactivePropertyAsync<Vector3> LeftClickPosition { get; }
        ReactivePropertyAsync<Vector3> RightClickPosition { get; }
        ReactivePropertyAsync<IDamagable> TargetSelected { get; }
    }

    internal class UserControlsModel : IUserControlsModel
    {
        public ReactivePropertyAsync<ISelectable> CurrentSelected => _currentSelected;
        public ReactivePropertyAsync<Vector3> LeftClickPosition => _leftClickPosition;
        public ReactivePropertyAsync<Vector3> RightClickPosition => _rightClickPosition;
        public ReactivePropertyAsync<IDamagable> TargetSelected => _targetSelected;

        private ReactivePropertyAsync<ISelectable> _currentSelected;
        private ReactivePropertyAsync<Vector3> _leftClickPosition;
        private ReactivePropertyAsync<Vector3> _rightClickPosition;
        private ReactivePropertyAsync<IDamagable> _targetSelected;

        public UserControlsModel()
        {
            _currentSelected = new ReactivePropertyAsync<ISelectable>();
            _leftClickPosition = new ReactivePropertyAsync<Vector3>();
            _rightClickPosition = new ReactivePropertyAsync<Vector3>();
            _targetSelected = new ReactivePropertyAsync<IDamagable>();
        }
    }
}
