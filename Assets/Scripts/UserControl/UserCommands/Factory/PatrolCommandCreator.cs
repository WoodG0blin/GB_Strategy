using UnityEngine;

namespace Strategy
{
    internal sealed class PatrolCommandCreator : CancellableCommandCreator<IPatrolCommand, Vector3>
    {
        protected override IPatrolCommand NewCommand(Vector3 argument) =>
            new Patrol(argument);
    }
}
