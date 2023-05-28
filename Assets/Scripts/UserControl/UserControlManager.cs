using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using WizardsPlatformer;

namespace Strategy
{
    public class UserControlManager : MonoBehaviour
    {
        [SerializeField] private Transform input;
        [SerializeField] private Transform levelObjectUI;
        [SerializeField] private Transform controlsPanel;

        private InputController _inputController;
        private LevelObjectUIView _levelObjectUIView;
        private SubscribtableProperty<ISelectable> _currentSelected;
        private UserControlsModel _userControlsModel;
        private CommandsController _commandsController;

        private void Start()
        {
            _userControlsModel = new UserControlsModel();
            _inputController = new InputController(input.GetComponent<InputView>());
            _levelObjectUIView = levelObjectUI.GetComponent<LevelObjectUIView>();
            _commandsController = new CommandsController(controlsPanel.GetComponent<IControlsUIView>());


            _currentSelected = new SubscribtableProperty<ISelectable>();

            _inputController.OnSelection += s => _userControlsModel.CurrentSelected.Value = s;

            _userControlsModel.CurrentSelected.SubscribeOnValueChange(s => _levelObjectUIView.DisplaySelected(s));
            _userControlsModel.CurrentSelected.SubscribeOnValueChange(s => _commandsController.OnSelectedChanged(s));
            
        }
    }
}
