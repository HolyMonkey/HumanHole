
    using CodeBase.Infrastructure.Services;

    public interface IPersistentProgressService : IService
    {
        Progress Progress { get; set; }
    }