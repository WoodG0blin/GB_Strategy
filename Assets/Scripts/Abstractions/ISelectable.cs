using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy
{
    public interface ISelectable
    {
        public Sprite Icon { get; }
        public int Health { get; }
        public int MaxHealth { get; }
    }
}
