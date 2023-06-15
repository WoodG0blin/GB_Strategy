using UnityEngine;
using TMPro;
using Zenject;
using System;
using UniRx;
using UnityEngine.UI;

namespace Strategy
{
    internal class GameStateView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timeValue;
        [SerializeField] private Button _menuButton;
        [SerializeField] private GameObject _menu;

        [Inject]
        private void Init(ITimeModel timeModel)
        {
            timeModel.GameTime.Subscribe(seconds =>
            {
                var t = TimeSpan.FromSeconds(seconds);
                _timeValue.text = $"{t.Minutes:D2} : {t.Seconds:D2}";
            }
            );

            _menuButton.OnClickAsObservable().Subscribe(_ => _menu.SetActive(true));
        }
    }
}
