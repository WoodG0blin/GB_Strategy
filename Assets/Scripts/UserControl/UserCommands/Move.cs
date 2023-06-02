using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy
{
    internal class Move : IMoveCommand
    {
        Action<ICommand> _callback;

        public Move(Vector3 direction) => Direction = direction;
        public Vector3 Direction { get; private set; }
    }
}
