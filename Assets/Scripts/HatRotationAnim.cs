using UnityEngine;

public class HatRotationAnim : MonoBehaviour
{
    public float angle_range = 30;
    private float angle = 0;
    public float force_multiplier = 1;
    public float damping = 0.3f;
    private float previous_x;
    void Start()
    {
        previous_x = transform.position.x;
    }

    void Update()
    {
        float offset = (transform.position.x - previous_x);
        angle += offset * force_multiplier;
        angle = Mathf.Clamp(angle, -angle_range, angle_range);
        angle *= Mathf.Pow(damping, Time.deltaTime);
        transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        previous_x = transform.position.x;
    }
}
