using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WizardsPlatformer;

namespace Strategy
{
    internal class UserControlsModel
    {
        public SubscribtableProperty<ISelectable> CurrentSelected { get; private set; }
        public UserControlsModel()
        {
            CurrentSelected = new SubscribtableProperty<ISelectable>();
        }
    }
}
