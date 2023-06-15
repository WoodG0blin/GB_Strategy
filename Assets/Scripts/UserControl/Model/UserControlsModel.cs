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
        IObservable<ISelectable> CurrentSelected { get; }
        IObservable<Vector3> LeftClickPosition { get; }
        IObservable<Vector3> RightClickPosition { get; }
        IObservable<IDamagable> TargetSelected { get; }
    }

    internal class UserControlsModel : IUserControlsModel
    {
        public IObservable<ISelectable> CurrentSelected => currentSelected;
        public IObservable<Vector3> LeftClickPosition => leftClickPosition;
        public IObservable<Vector3> RightClickPosition => rightClickPosition;
        public IObservable<IDamagable> TargetSelected => targetSelected;

        public ObjectSelected currentSelected;
        public LeftClickPosition leftClickPosition;
        public RightClickPosition rightClickPosition;
        public TargetSelected targetSelected;

        public UserControlsModel()
        {
            currentSelected = new ObjectSelected();
            leftClickPosition = new LeftClickPosition();
            rightClickPosition = new RightClickPosition();
            targetSelected = new TargetSelected();
        }
    }
}
