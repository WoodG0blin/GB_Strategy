using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Strategy
{
    internal class LevelObjectUIView : MonoBehaviour
    {
        [SerializeField] private GameObject _activeSelection;
        [SerializeField] private GameObject _noSelection;

        [SerializeField] private Image _icon;
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private TextMeshProUGUI _healthText;

        public void DisplaySelected(ISelectable selected)
        {
            if(selected != null)
            {
                SetActive(true);
                _icon.sprite = selected.Icon;
                
                _healthSlider.value = selected.Health;
                _healthSlider.maxValue= selected.MaxHealth;

                _healthText.text = $"{selected.Health} / {selected.MaxHealth}";
                if (selected.MaxHealth == 0) _healthText.text = "-";
            }
            else SetActive(false);
        }

        private void SetActive(bool active)
        {
            _activeSelection.SetActive(active);
            _noSelection.SetActive(!active);
        }
    }
}
