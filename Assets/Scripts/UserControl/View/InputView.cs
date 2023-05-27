using System;
using UnityEngine;

namespace Strategy
{
    internal interface IInputView
    {
        event Action<Vector3> OnLeftClick;
        event Action<Vector3> OnRightClick;
    }

    internal class InputView : MonoBehaviour, IInputView
    {
        public event Action<Vector3> OnLeftClick;
        public event Action<Vector3> OnRightClick;

        void Update()
        {
            if (Input.GetMouseButtonUp(0)) OnLeftClick?.Invoke(Input.mousePosition);
            if (Input.GetMouseButtonUp(1)) OnRightClick?.Invoke(Input.mousePosition);
        }
    }
}
