using Strategy;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProduceUnitInstaller : MonoInstaller
{
    [SerializeField] UnitSettings[] _units;
    public override void InstallBindings()
    {
        for(int i = 0; i < _units.Length; i++)
            Container.Bind<IUnitSettings>().WithId(_units[i].Name).To<UnitSettings>().FromInstance(_units[i]);
    }
}