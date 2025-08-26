using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float force;

    public Transform left_hand_target;
    public Transform right_hand_target;
    public float max_target_speed = 3;

    public float2 target_hand_offset = new float2(0.5f, 1);
    
    public float hand_max_reach = 3;
    public bool has_target = false;
    public float2 target_position;

    private float2 left_target_default_offset;
    private float2 right_target_default_offset;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody2D>();
        left_target_default_offset = ((float3)left_hand_target.transform.localPosition).xy;
        right_target_default_offset = ((float3)right_hand_target.transform.localPosition).xy;
    }

    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        float2 left_target = left_target_default_offset;
        float2 right_target = right_target_default_offset;
        if(has_target)
        {
            float2 target_offset = target_position - (float2)(Vector2)transform.position;
            float2 tangent = math.normalize(target_offset);
            float2 normal = math.normalize(math.cross(new float3(target_offset, 0), new float3(0, 0, 1)).xy);
            right_target = target_offset + tangent * target_hand_offset.y + normal * target_hand_offset.x;
            left_target = target_offset + tangent * target_hand_offset.y - normal * target_hand_offset.x;
        }
        left_hand_target.localPosition = new float3(left_target, 0);
        right_hand_target.localPosition = new float3(right_target, 0);
        rb.AddForce(Mouse.current.delta.ReadValue() * force);
    }
}
