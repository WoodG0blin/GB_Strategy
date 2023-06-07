using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Strategy
{
    internal interface IUserControlsModel
    {
        SubscribtableProperty<ISelectable> CurrentSelected { get; }
        SubscribtableProperty<Vector3> LeftClickPosition { get; }
        SubscribtableProperty<Vector3> RightClickPosition { get; }
        SubscribtableProperty<IDamagable> TargetSelected { get; }
    }

    internal class UserControlsModel : IUserControlsModel
    {
        public SubscribtableProperty<ISelectable> CurrentSelected { get; private set; }
        public SubscribtableProperty<Vector3> LeftClickPosition { get; private set; }
        public SubscribtableProperty<Vector3> RightClickPosition { get; private set; }
        public SubscribtableProperty<IDamagable> TargetSelected { get; private set; }
        public UserControlsModel()
        {
            CurrentSelected = new SubscribtableProperty<ISelectable>();
            LeftClickPosition = new SubscribtableProperty<Vector3>();
            RightClickPosition = new SubscribtableProperty<Vector3>();
            TargetSelected = new SubscribtableProperty<IDamagable>();
        }
    }
}
