using Strategy;
using UnityEngine;
using Zenject;

public class CoreInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<TimeModel>().AsSingle();
    }
}