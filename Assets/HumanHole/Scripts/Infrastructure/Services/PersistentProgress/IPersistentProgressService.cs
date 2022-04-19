
    using HumanHole.Scripts.Data;

    namespace HumanHole.Scripts.Infrastructure.Services.PersistentProgress
     {
         public interface IPersistentProgressService : IService
         {
             Progress Progress { get; set; }
         }
     }