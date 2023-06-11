using System;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Strategy
{
    internal class InputView : MonoBehaviour
    {
        private Camera _camera;
        private Highlighter _highlighter;

        [Inject(Id = "LeftClick")] private SubscribtableProperty<ISelectable> _selected;
        [Inject(Id = "RightClick")] private SubscribtableProperty<IDamagable> _target;
        [Inject(Id = "LeftClick")] private SubscribtableProperty<Vector3> _leftClickPosition;
        [Inject(Id = "RightClick")] private SubscribtableProperty<Vector3> _rightClickPosition;

        private void Start()
        {
            _camera = Camera.main;
            _highlighter = new Highlighter();

            var interactableStream = Observable.EveryUpdate()
                .Where(_ => !EventSystem.current.IsPointerOverGameObject());

            var leftClickStream = interactableStream
                .Where(_ => Input.GetMouseButtonUp(0))
                .Select(_ => _camera.ScreenPointToRay(Input.mousePosition))
                .Select(ray => (ray, Physics.RaycastAll(ray)))
                .Subscribe(cortege =>
                {
                    if (TrySelectHits<ISelectable>(cortege.Item2, out var selected))
                    {
                        _highlighter.HighLight(selected);
                        _selected.Value = selected;
                    }
                    _leftClickPosition.Value = GetGroundsCoordinates(cortege.Item1);
                });

            var rightClickStream = interactableStream
                .Where(_ => Input.GetMouseButtonUp(1))
                .Select(_ => _camera.ScreenPointToRay(Input.mousePosition))
                .Select(ray => (ray, Physics.RaycastAll(ray)))
                .Subscribe(cortege => 
                {
                    if (TrySelectHits<IDamagable>(cortege.Item2, out var selected))
                    {
                        _target.Value = selected;
                    }
                    _rightClickPosition.Value = GetGroundsCoordinates(cortege.Item1);
                });
        }

        private Vector3 GetGroundsCoordinates(Ray ray)
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("Grounds")))
                return hit.point;
            else return Vector3.zero;
        }

        private bool TrySelectHits<T>(RaycastHit[] hits, out T result) where T : class
        {
            result = default;
            if(hits.Length == 0) return false;

            result = hits
                .Select(hit => hit.collider.GetComponentInParent<T>())
                .Where(c => c != null)
                .FirstOrDefault();

            return result != default;
        }
    }
}
