using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private ConfigurableJoint _upperArm;
    [SerializeField] private ConfigurableJoint _lowerArm;

    public void PrepareForSwim()
    {
        JointDrive drive = new JointDrive();
        drive.positionSpring = 20;
        drive.positionDamper = 2;
        drive.maximumForce = 3.402823e+38f;

        _upperArm.slerpDrive = drive;

        _lowerArm.slerpDrive = drive;
    }

    public void Relax()
    {
        JointDrive drive = new JointDrive();
        drive.positionSpring = 0;
        drive.positionDamper = 0;
        drive.maximumForce = 3.402823e+38f;

        _upperArm.slerpDrive = drive;

        _lowerArm.slerpDrive = drive;
    }
}
