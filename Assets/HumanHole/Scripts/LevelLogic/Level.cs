using HumanHole.Scripts.ActiveRagdoll;
using HumanHole.Scripts.Infrastructure.Services.Factory;
using HumanHole.Scripts.Level;
using HumanHole.Scripts.WaterLogic;
using UnityEngine;

namespace HumanHole.Scripts.LevelLogic
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Platform _platform;
        [SerializeField] private BodyTarget _bodyTarget;
        [SerializeField] private WaterCollider _waterCollider;
        [SerializeField] private Water _water;

        public WaterCollider WaterCollider => _waterCollider;
        
        public void Initial(Person person, LevelStaticData levelStaticData, IFactoryService factoryService)
        {
            _platform.Initial(levelStaticData.PlatformColor);
            _bodyTarget.Initial(person);
            _waterCollider.Initial(factoryService);
            _water.Initial(levelStaticData.WaterMaterial);
        }
    }
}
