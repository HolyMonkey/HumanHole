using HumanHole.Scripts.Data;

namespace HumanHole.Scripts.Infrastructure.Services.PersistentProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public Progress Progress { get; set; }
    }
}