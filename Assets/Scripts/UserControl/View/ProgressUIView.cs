using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Strategy
{
    internal class ProgressUIView : MonoBehaviour
    {
        public IObservable<int> CancelButtonClick => _cancelButtonClick;

        [SerializeField] private Slider _progressSlider;
        [SerializeField] private TextMeshProUGUI _currentName;

        [SerializeField] private Image[] _icons;
        [SerializeField] private GameObject[] _imageHolders;
        [SerializeField] private Button[] _cancellButtons;

        private Subject<int> _cancelButtonClick = new Subject<int>();
        private IDisposable _productionCancellationToken;

        private void Start()
        {
            for(int i = 0; i < _cancellButtons.Length; i++) _cancellButtons[i].onClick.AddListener(() => _cancelButtonClick.OnNext(i));
        }

        private void SetAll(IUnitProductionTask task)
        {
            bool clear = task == null;

            if(clear) ClearIcons();

            _progressSlider.gameObject.SetActive(!clear);
            _currentName.text = clear ? string.Empty : task.UnitName;
            _currentName.enabled= !clear;
            if (clear) _productionCancellationToken.Dispose();
            else _productionCancellationToken = Observable.EveryUpdate()
                    .Subscribe(_ =>
                    {
                        _progressSlider.value = task.TimeLeft / task.ProductionTime;
                    });
        }

        private void ClearIcons()
        {
            for (int i = 0; i < _icons.Length; i++)
            {
                _icons[i].sprite = null;
                _imageHolders[i].SetActive(false);
            }
        }


        public void SetTask(IUnitProductionTask task, int index)
        {
            if (task == null)
            {
                if (index == 0) SetAll(null);
                else ClearIcons();
            }
            else
            {
                _imageHolders[index].SetActive(true);
                _icons[index].sprite = task.Icon;

                if(index == 0) SetAll(task);
            }
        }
    }
}
