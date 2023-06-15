using Strategy;
using UnityEngine;
using Zenject;

public class ProgressUIInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ProgressUIView>().FromComponentInHierarchy().AsSingle();
    }
}