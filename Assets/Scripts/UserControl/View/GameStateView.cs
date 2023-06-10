using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;
using System;
using UniRx;

namespace Strategy
{
    internal class GameStateView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _timeValue;

        [Inject]
        private void Init(ITimeModel timeModel)
        {
            timeModel.GameTime.Subscribe(seconds =>
            {
                var t = TimeSpan.FromSeconds(seconds);
                _timeValue.text = $"{t.Minutes:D2} : {t.Seconds:D2}";
            }
            );
        }
    }
}
