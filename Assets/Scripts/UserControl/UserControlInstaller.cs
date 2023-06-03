using Strategy;
using UnityEngine;
using WizardsPlatformer;
using Zenject;

public class UserControlInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        UserControlsModel model = new UserControlsModel();

        Container.Bind<IUserControlsModel>().To<UserControlsModel>().FromInstance(model).Lazy();

        Container.Bind<SubscribtableProperty<Vector3>>().FromInstance(model.RightClickPosition).AsSingle();

        Container.Bind<AttackCommandCreator>().FromNew().AsSingle();
        Container.Bind<ProduceUnitCommandCreator>().FromNew().AsSingle();
        Container.Bind<MoveCommandCreator>().FromNew().AsSingle();
        Container.Bind<PatrolCommandCreator>().FromNew().AsSingle();
        Container.Bind<HoldCommandCreator>().FromNew().AsSingle();

        Container.Bind<CommandFactory>().FromNew().AsSingle();

        Container.Bind<ICommandsModel>().To<CommandsModel>().FromNew().AsSingle();
    }
}