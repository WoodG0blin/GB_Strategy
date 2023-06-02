using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Strategy
{
    internal interface IControlsUIView
    {
        event Action<ICommandExecutor> OnCommand;

        void Clear();
        void SetLayout(List<ICommandExecutor> commandTypes);
        void BlockCommands(ICommandExecutor executor);
        void UnBlockCommands();
    }

    internal class ControlsUIView : MonoBehaviour, IControlsUIView
    {
        [SerializeField] private Transform _controlsContainer;

        private Dictionary<Type, Button> _buttonsDictionary;

        public event Action<ICommandExecutor> OnCommand;

        void Start()
        {
            _buttonsDictionary = new Dictionary<Type, Button>();

            for(int i = 0; i < _controlsContainer.childCount; i++)
            {
                var _buttonGO = _controlsContainer.GetChild(i);
                var type = ConvertNameToType(_buttonGO.name);
                if(type != null) _buttonsDictionary.Add(type, _buttonGO.GetComponentInChildren<Button>());
            }
            Clear();
        }

        private Type ConvertNameToType(string name) =>
            name switch
            {
                "AttackCommand" => typeof(CommandExecutor<IAttackCommand>),
                "ProduceUnitCommand" => typeof(CommandExecutor<IProduceUnitCommand>),
                "MoveCommand" => typeof(CommandExecutor<IMoveCommand>),
                "PatrolCommand" => typeof(CommandExecutor<IPatrolCommand>),
                "HoldCommand" => typeof(CommandExecutor<IStopCommand>),
                _ => null
            };


        public void Clear()
        {
            foreach(Button b in _buttonsDictionary.Values) SetActiveButton(b, false);
        }

        private void SetActiveButton(Button b, bool active) => b.transform.parent.gameObject.SetActive(active);

        public void SetLayout(List<ICommandExecutor> commandTypes)
        {
            if (commandTypes == null || commandTypes.Count() == 0) return;

            foreach (var t in commandTypes)
            {
                var button = GetButtonByExecutorType(t);
                if(button != null)
                {
                    SetActiveButton(button, true);
                    button.onClick.AddListener(() => OnCommand?.Invoke(t));
                }
            }
        }
        private Button GetButtonByExecutorType(ICommandExecutor executorType) =>
             _buttonsDictionary.Where(kvp => kvp.Key.IsAssignableFrom(executorType.GetType())).FirstOrDefault().Value;

        public void BlockCommands(ICommandExecutor executor)
        {
            SetInteraction(true);
            GetButtonByExecutorType(executor).interactable = false;
        }

        public void UnBlockCommands() => SetInteraction(true);
        private void SetInteraction(bool on)
        {
            foreach (Button b in _buttonsDictionary.Values) b.interactable = on;
        }
    }
}
