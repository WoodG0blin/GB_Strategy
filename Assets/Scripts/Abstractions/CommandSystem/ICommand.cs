using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy
{
    public enum CommandType
    {
        None = 0,
        Attack = 1,
        ProduceUnit = 2,
        Move = 3,
        Patrol = 4,
        Hold = 5
    }

    public interface ICommand
    {
    }
}
