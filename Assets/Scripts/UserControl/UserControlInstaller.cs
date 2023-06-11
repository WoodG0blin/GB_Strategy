using Strategy;
using UnityEngine;
using Zenject;

public class UserControlInstaller : MonoInstaller
{
    [SerializeField] private ControlsUIView _controlsUI;
    public override void InstallBindings()
    {
        UserControlsModel model = new UserControlsModel();

        Container.Bind<IUserControlsModel>().To<UserControlsModel>().FromInstance(model).Lazy();

        Container.Bind<SubscribtableProperty<Vector3>>().WithId("LeftClick").FromInstance(model.LeftClickPosition);
        Container.Bind<SubscribtableProperty<Vector3>>().WithId("RightClick").FromInstance(model.RightClickPosition);
        Container.Bind<SubscribtableProperty<IDamagable>>().WithId("RightClick").FromInstance(model.TargetSelected).AsSingle();
        Container.Bind<SubscribtableProperty<ISelectable>>().WithId("LeftClick").FromInstance(model.CurrentSelected).AsSingle();

        Container.Bind<AttackCommandCreator>().FromNew().AsSingle();
        Container.Bind<ProduceUnitCommandCreator>().FromNew().AsSingle();
        Container.Bind<MoveCommandCreator>().FromNew().AsSingle();
        Container.Bind<PatrolCommandCreator>().FromNew().AsSingle();
        Container.Bind<HoldCommandCreator>().FromNew().AsSingle();

        Container.Bind<CommandFactory>().FromNew().AsSingle();
        Container.Bind<IControlsUIView>().To<ControlsUIView>().FromInstance(_controlsUI);

        Container.Bind<ICommandsModel>().To<CommandsModel>().FromNew().AsSingle();
    }
}