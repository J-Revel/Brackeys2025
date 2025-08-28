using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PatrollingEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    public float2 altitude_range = new float2(10, 20);
    public float altitude_force;
    public float patrolling_step_duration = 5;
    public float patrolling_step_force = 10;
    private float patrolling_step_time = 0;
    private float patrolling_step_direction = 1;
    public bool follow_target = false;
    public float target_angle_offset = 0;
    public float balance_torque = 10;
    public float2 balance_angle_smoothstep = new float2(0, math.PI / 5);
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float current_angle = transform.rotation.eulerAngles.z;
        float target_angle = target_angle_offset;
        if(follow_target)
        {
            target_angle += (patrolling_step_direction > 0 ? 0 : 180);
        }
        float angle_offset = (target_angle - current_angle) * math.PI / 180;
        while (angle_offset < -math.PI)
            angle_offset += math.PI * 2;
        while (angle_offset > math.PI)
            angle_offset -= math.PI * 2;
        float balance_force = math.smoothstep(balance_angle_smoothstep.x, balance_angle_smoothstep.y, math.abs(angle_offset));
        rb.AddTorque(balance_force * math.sign(angle_offset) * balance_torque);
        
        if (transform.position.y > altitude_range.y)
            rb.AddForce(new float2(0, -altitude_force));
        if (transform.position.y < altitude_range.x)
            rb.AddForce(new float2(0, altitude_force));
        patrolling_step_time += Time.deltaTime;
        if(patrolling_step_time > patrolling_step_duration)
        {
            patrolling_step_time = 0;
            patrolling_step_direction *= -1;
        }
        rb.AddForce(new float2(patrolling_step_direction * patrolling_step_force, 0));
    }
}
