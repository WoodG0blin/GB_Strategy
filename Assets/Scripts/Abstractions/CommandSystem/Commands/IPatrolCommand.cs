using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy
{
    public interface IPatrolCommand : ICommand
    {
        Vector3 PatrolTarget { get; }
    }
}
