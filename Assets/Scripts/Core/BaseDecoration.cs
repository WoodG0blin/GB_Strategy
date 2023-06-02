using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy
{
    internal class BaseDecoration : MonoBehaviour, ISelectable
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public int Health { get; set; }
        [field: SerializeField] public int MaxHealth { get; private set; }
    }
}
