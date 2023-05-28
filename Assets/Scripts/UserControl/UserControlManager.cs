using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WizardsPlatformer;

namespace Strategy
{
    public class UserControlManager
    {
        private InputController _inputController;
        private LevelObjectUIView _levelObjectUIView;
        private SubscribtableProperty<ISelectable> _currentSelected;

        public UserControlManager(Transform input, Transform levelObjectUI)
        {
            _currentSelected = new SubscribtableProperty<ISelectable>();

            _inputController = new InputController(input.GetComponent<InputView>());
            _inputController.OnSelection += s => _currentSelected.Value = s;

            _levelObjectUIView = levelObjectUI.GetComponent<LevelObjectUIView>();

            _currentSelected.SubscribeOnValueChange(s => _levelObjectUIView.DisplaySelected(s));
        }
    }
}
