using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProduceUnitInstaller : MonoInstaller
{
    [SerializeField] GameObject[] _units;
    public override void InstallBindings()
    {
        for(int i = 0; i < _units.Length; i++)
            Container.Bind<GameObject>().WithId(_units[i].name).FromInstance(_units[i]).AsTransient().Lazy();
    }
}