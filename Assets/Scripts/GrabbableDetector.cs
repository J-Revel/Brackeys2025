using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabbableDetector : MonoBehaviour
{
    private Grabbable grabbable_in_range;
    private SpringJoint2D grab_joint;
    private Rigidbody2D rb;
    public PlayerMovement player_movement;
    public LayerMask raycast_layer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D arb = collision.attachedRigidbody;
        if (arb != null)
        {
            Grabbable grabbable = arb.GetComponent<Grabbable>();
            if(grabbable != null)
            {
                grabbable_in_range = grabbable;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(grabbable_in_range != null && grabbable_in_range == collision.attachedRigidbody.GetComponent<Grabbable>())
        {
            grabbable_in_range = null;
        }
    }

    public void Detach()
    {
        grabbable_in_range = null;
    }

    public void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    public void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && grabbable_in_range != null)
        {
            grab_joint = rb.gameObject.AddComponent<SpringJoint2D>();
            grab_joint.connectedBody = grabbable_in_range.GetComponent<Rigidbody2D>();
            grab_joint.autoConfigureConnectedAnchor = false;
            grab_joint.frequency = 5;
            grab_joint.dampingRatio = 1;
            Vector3 raycast_direction = (grabbable_in_range.transform.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, raycast_direction, 10, raycast_layer);
            Debug.DrawRay(transform.position, raycast_direction * 10, Color.red, 10);
            if(hit)
            {
                Vector2 local_point = grabbable_in_range.transform.InverseTransformPoint(hit.point);
                grab_joint.connectedAnchor = local_point;
                player_movement.target_position = hit.point;
            }
        }
        if (grab_joint != null && grab_joint.connectedBody != null)
        {
            float3 target_pos = grab_joint.connectedBody.transform.TransformPoint(grab_joint.connectedAnchor);
            player_movement.target_position = target_pos.xy;
            player_movement.has_target = true;
        }
        else player_movement.has_target = false;

        if (Mouse.current.leftButton.wasReleasedThisFrame && grab_joint != null)
        {
            player_movement.has_target = false;
            Destroy(grab_joint);
            grab_joint = null;
        }
    }
}
