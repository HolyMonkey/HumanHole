using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
