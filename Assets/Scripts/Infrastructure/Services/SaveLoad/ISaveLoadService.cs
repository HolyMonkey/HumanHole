public interface ISaveLoadService : IService
{
    void SaveProgress();
    Progress LoadProgress();
}