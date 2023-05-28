using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Strategy
{
    internal interface IControlsUIView
    {
        event Action<Type> OnCommand;

        void Clear();
        void SetLayout(IEnumerable<Type> commandTypes);
    }

    internal class ControlsUIView : MonoBehaviour, IControlsUIView
    {
        [SerializeField] private Transform _controlsContainer;
        [SerializeField] private GameObject _controlPrefab;

        private List<GameObject> _buttons;

        public event Action<Type> OnCommand;

        void Start()
        {
            _buttons = new List<GameObject>();
        }


        public void SetLayout(IEnumerable<Type> commandTypes)
        {
            if (commandTypes == null || commandTypes.Count() == 0) return;

            foreach(var t in commandTypes)
            {
                Button button = Instantiate(_controlPrefab, _controlsContainer).transform.GetComponentInChildren<Button>();
                button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = SetCaption(t);
                _buttons.Add(button.transform.parent.gameObject);
                button.gameObject.SetActive(true);
                button.onClick.AddListener(() => OnCommand.Invoke(t));
            }
        }

        private string SetCaption(Type t)
        {
            string s = t.ToString();
            return s.Substring("Strategy.I".Length, s.Length - "Strategy.ICommand".Length);
        }

        public void Clear()
        {
            foreach(var t in _buttons) Destroy(t.gameObject);
            _buttons.Clear();
        }
    }
}
