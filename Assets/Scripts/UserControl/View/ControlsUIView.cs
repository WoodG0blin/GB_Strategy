using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Strategy
{
    internal interface IControlsUIView
    {
        event Action<Type> OnCommand;

        void Clear();
        void SetLayout(IEnumerable<Type> commandTypes);
        void BlockCommands();
        void UnBlockCommands();
    }

    internal class ControlsUIView : MonoBehaviour, IControlsUIView
    {
        [SerializeField] private Transform _controlsContainer;

        private Dictionary<Type, Button> _buttonsDictionary;

        public event Action<Type> OnCommand;

        void Start()
        {
            _buttonsDictionary = new Dictionary<Type, Button>();

            for(int i = 0; i < _controlsContainer.childCount; i++)
            {
                var _buttonGO = _controlsContainer.GetChild(i);
                _buttonsDictionary.Add(ConvertNameToType(_buttonGO.name), _buttonGO.GetComponentInChildren<Button>());
            }
            Clear();
        }

        private Type ConvertNameToType(string name) =>
            name switch
            {
                "AttackCommand" => typeof(IAttackCommand),
                "ProduceUnitCommand" => typeof(IProduceUnitCommand),
                "MoveCommand" => typeof(IMoveCommand),
                "PatrolCommand" => typeof(IPatrolCommand),
                "HoldCommand" => typeof(IStopCommand),
                _ => null
            };


        public void Clear()
        {
            foreach(Button b in _buttonsDictionary.Values) SetActiveButton(b, false);
        }

        private void SetActiveButton(Button b, bool active) => b.transform.parent.gameObject.SetActive(active);

        public void SetLayout(IEnumerable<Type> commandTypes)
        {
            if (commandTypes == null || commandTypes.Count() == 0) return;

            foreach (var t in commandTypes)
            {
                if (_buttonsDictionary.ContainsKey(t))
                {
                    SetActiveButton(_buttonsDictionary[t], true);
                    _buttonsDictionary[t].onClick.AddListener(() => OnCommand?.Invoke(t));
                }
            }
        }

        public void BlockCommands() => SetInteraction(false);
        public void UnBlockCommands() => SetInteraction(true);
        private void SetInteraction(bool on)
        {
            foreach (Button b in _buttonsDictionary.Values) b.interactable = on;
        }
    }
}
