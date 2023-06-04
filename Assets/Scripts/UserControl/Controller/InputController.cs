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
        private Highlighter _highlighter;

        public event Action<ISelectable> OnSelection;
        public event Action<Vector3> OnLeftClick;
        public event Action<Vector3> OnRightClick;
        public event Action<IDamagable> OnRightSelection;

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

        public void OnSelectionChanged(ISelectable selected) => _highlighter.HighLight(selected);

        private void LeftClick(Vector3 position)
        {
            var hits = Physics.RaycastAll(_camera.ScreenPointToRay(position));
            if (hits == null) return;

            var selected = hits
                .Select(hit => hit.collider.GetComponentInParent<ISelectable>())
                .Where(c => c != null)
                .FirstOrDefault();

            _highlighter.HighLight(selected);

            OnSelection?.Invoke(selected);

            SendGroundsCoordinates(position, OnLeftClick);
        }

        private void RightClick(Vector3 position)
        {
            var hits = Physics.RaycastAll(_camera.ScreenPointToRay(position));
            if (hits == null) return;

            var target = hits
                .Select(hit => hit.collider.GetComponentInParent<IDamagable>())
                .Where(c => c != null)
                .FirstOrDefault();

            OnRightSelection?.Invoke(target);

            SendGroundsCoordinates(position, OnRightClick);
        }

        private void SendGroundsCoordinates(Vector3 position, Action<Vector3> call)
        {
            RaycastHit hit;
            if (Physics.Raycast(_camera.ScreenPointToRay(position), out hit, 1000, LayerMask.GetMask("Grounds")))
                call?.Invoke(hit.point);
            else call?.Invoke(Vector3.zero);
        }
    }
}
