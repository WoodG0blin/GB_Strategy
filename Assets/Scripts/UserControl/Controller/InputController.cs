using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using WizardsPlatformer;

namespace Strategy
{
    internal class InputController : IDisposable
    {
        private IInputView _inputView;
        private Camera _camera;
        private ISelectable _currentSelected;
        private Highlighter _highlighter;

        public event Action<ISelectable> OnSelection;
        public event Action<Vector3> OnLeftClick;
        public event Action<Vector3> OnRightClick;
        public InputController(IInputView input)
        {
            _inputView = input;
            _inputView.OnLeftClick += LeftClick;
            _inputView.OnRightClick += RightClick;

            _camera = Camera.main;

            _highlighter = new Highlighter();
        }

        public void Dispose()
        {
            _inputView.OnLeftClick -= LeftClick;
            _inputView.OnRightClick -= RightClick;
            _inputView = null;
            _camera = null;
        }

        private void LeftClick(Vector3 position)
        {
            var hits = Physics.RaycastAll(_camera.ScreenPointToRay(position));
            if (hits == null) return;

            _highlighter.HighLight(_currentSelected, false);

            _currentSelected = hits
                .Select(hit => hit.collider.GetComponentInParent<ISelectable>())
                .Where(c => c != null)
                .FirstOrDefault();

            _highlighter.HighLight(_currentSelected, true);

            OnSelection?.Invoke(_currentSelected);

            OnLeftClick?.Invoke(GetGroundsCoordinates(position));
        }

        private void RightClick(Vector3 position)
        {
            OnRightClick?.Invoke(GetGroundsCoordinates(position));
        }

        private Vector3 GetGroundsCoordinates(Vector3 position)
        {
            RaycastHit hit;
            if(Physics.Raycast(_camera.ScreenPointToRay(position), out hit, 1000, LayerMask.GetMask("Grounds")))
                return hit.point;
            return Vector3.zero;
        }
    }
}
