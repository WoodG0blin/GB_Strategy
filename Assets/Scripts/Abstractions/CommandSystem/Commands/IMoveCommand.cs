using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy
{
    public interface IMoveCommand : ICommand
    {
        Vector3 Direction { get; }
    }
}
