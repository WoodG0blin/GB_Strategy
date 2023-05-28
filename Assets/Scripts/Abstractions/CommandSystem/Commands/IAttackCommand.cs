using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy
{
    public interface IAttackCommand : ICommand
    {
        string Target { get; }
    }
}
