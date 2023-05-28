using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy
{
    internal class Attack : IAttackCommand
    {
        public Attack(string targetName) => Target = targetName;
        public string Target { get; private set; } 
    }
}
