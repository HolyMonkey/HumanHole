using UnityEngine;

namespace HumanHole.Scripts.ActiveRagdoll
{
    [RequireComponent(typeof(ConfigurableJoint))]
    public class Spine : BodyPart
    {
        public ConfigurableJoint ConfigurableJoint => _configurableJoint;
        
        private ConfigurableJoint _configurableJoint;

        protected override void Awake()
        {
           base.Awake();
           _configurableJoint = GetComponent<ConfigurableJoint>();
        }
    }
}
