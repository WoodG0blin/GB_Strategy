using UnityEngine;
using Zenject;

namespace Strategy
{
    internal sealed class MoveCommandCreator : CancellableCommandCreator<IMoveCommand, RightClickPosition, Vector3>
    {
        protected override IMoveCommand NewCommand(Vector3 argument) =>
            new Move(argument);
    }
}
