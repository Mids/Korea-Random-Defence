using KRD;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IRTSManager>().FromComponentInHierarchy().AsCached();
    }
}