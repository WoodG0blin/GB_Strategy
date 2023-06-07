namespace Strategy
{
    internal sealed class AttackCommandCreator : CancellableCommandCreator<IAttackCommand, IDamagable>
    {
        protected override IAttackCommand NewCommand(IDamagable argument) =>
            new Attack(argument.ToString());
    }
}
