using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameData>().AsSingle();
        Container.Bind<CursorService>().AsSingle();
    }
}