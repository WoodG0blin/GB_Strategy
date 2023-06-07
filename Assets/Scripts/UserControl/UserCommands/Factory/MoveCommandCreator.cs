using UnityEngine;

namespace Strategy
{
    internal sealed class MoveCommandCreator : CancellableCommandCreator<IMoveCommand, Vector3>
    {
        protected override IMoveCommand NewCommand(Vector3 argument) =>
            new Move(argument);
    }
}
