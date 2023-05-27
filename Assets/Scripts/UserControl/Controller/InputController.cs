using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Strategy
{
    internal class InputController : IDisposable
    {
        private IInputView _inputView;
        private Camera _camera;
        private ISelectable _currentSelected;

        public event Action<ISelectable> OnSelection;
        public InputController(IInputView input)
        {
            _inputView = input;
            _inputView.OnLeftClick += OnLeftClick;
            _inputView.OnRightClick += OnRightClick;

            _camera = Camera.main;
        }

        public void Dispose()
        {
            _inputView.OnLeftClick -= OnLeftClick;
            _inputView.OnRightClick -= OnRightClick;
            _inputView = null;
            _camera = null;
        }

        private void OnLeftClick(Vector3 position)
        {
            var hits = Physics.RaycastAll(_camera.ScreenPointToRay(position));

            if (hits == null || EventSystem.current.IsPointerOverGameObject()) return;

            _currentSelected?.Highlight(false);

            _currentSelected = hits
                .Select(hit => hit.collider.GetComponentInParent<ISelectable>())
                .Where(c => c != null)
                .FirstOrDefault();

            _currentSelected?.Highlight(true);

            OnSelection?.Invoke(_currentSelected);
        }

        private void OnRightClick(Vector3 position)
        {
            Debug.Log($"Right mouse button clicked at {position}");
        }
    }
}
