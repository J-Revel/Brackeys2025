using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class LeverActivator : MonoBehaviour
{
    public float2 activation_angle_range;
    private bool activated = false;
    private HingeJoint2D joint;
    public UnityEvent activation_event;
    void Start()
    {
        joint = GetComponent<HingeJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bool in_activation_range = joint.jointAngle > activation_angle_range.x && joint.jointAngle < activation_angle_range.y;
        if(in_activation_range != activated)
        {
            activated = in_activation_range;
            activation_event.Invoke();
        }
    }
}
