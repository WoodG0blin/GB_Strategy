using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using Zenject;

namespace Strategy
{
    public class UserControlManager : MonoBehaviour
    {
        [SerializeField] private Transform input;
        [SerializeField] private Transform levelObjectUI;
        [SerializeField] private Transform controlsPanel;

        private InputController _inputController;
        private LevelObjectUIView _levelObjectUIView;
        [Inject] private IUserControlsModel _userControlsModel;
        [Inject] private ICommandsModel _commandsModel;
        private CommandsController _commandsController;

        private void Start()
        {
            _inputController = new InputController(input.GetComponent<InputView>());
            _levelObjectUIView = levelObjectUI.GetComponent<LevelObjectUIView>();
            _commandsController = new CommandsController(controlsPanel.GetComponent<IControlsUIView>(), _commandsModel);

            _inputController.OnSelection += s => _userControlsModel.CurrentSelected.Value = s;
            _inputController.OnLeftClick += lp => _userControlsModel.LeftClickPosition.Value = lp;
            _inputController.OnRightClick += rp => _userControlsModel.RightClickPosition.Value = rp;
            _inputController.OnRightSelection += rs => _userControlsModel.TargetSelected.Value = rs;

            _userControlsModel.CurrentSelected.SubscribeOnValueChange(s => _levelObjectUIView.DisplaySelected(s));
            _userControlsModel.CurrentSelected.SubscribeOnValueChange(s => _commandsController.OnSelectedChanged(s));
        }
    }
}
