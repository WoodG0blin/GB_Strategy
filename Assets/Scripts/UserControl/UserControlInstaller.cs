using Strategy;
using System;
using UniRx;
using UnityEngine;
using Zenject;

public class UserControlInstaller : MonoInstaller
{
    [SerializeField] private ControlsUIView _controlsUI;
    public override void InstallBindings()
    {
        UserControlsModel model = new UserControlsModel();

        Container.Bind<IUserControlsModel>().To<UserControlsModel>().FromInstance(model).Lazy();

        Container.Bind<LeftClickPosition>().FromInstance(model.leftClickPosition);
        Container.Bind<RightClickPosition>().FromInstance(model.rightClickPosition);
        Container.Bind<TargetSelected>().FromInstance(model.targetSelected);
        Container.Bind<ObjectSelected>().FromInstance(model.currentSelected);
        Container.Bind<IObservable<ISelectable>>().To<ObjectSelected>().FromInstance(model.currentSelected);

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