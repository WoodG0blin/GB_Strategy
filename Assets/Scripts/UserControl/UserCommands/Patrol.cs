using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy
{
    internal class Patrol : IPatrolCommand
    {
        public Vector3 PatrolTarget { get; private set; }
        public Patrol(Vector3 to) => PatrolTarget = to;
    }
}
