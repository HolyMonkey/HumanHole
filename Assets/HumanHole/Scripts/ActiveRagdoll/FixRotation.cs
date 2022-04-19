using UnityEngine;

namespace HumanHole.Scripts.ActiveRagdoll
{
    [RequireComponent(typeof(ConfigurableJoint))]
    public class FixRotation : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ConfigurableJoint joint = GetComponent<ConfigurableJoint>();
                joint.targetRotation = Quaternion.Inverse(transform.rotation);
            }
        }
    }
}
