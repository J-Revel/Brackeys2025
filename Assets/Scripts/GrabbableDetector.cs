using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabbableDetector : MonoBehaviour
{
    private Grabbable grabbable_in_range;
    private SpringJoint2D grab_joint;
    private Rigidbody2D rb;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Grabbable grabbable = collision.attachedRigidbody.GetComponent<Grabbable>();
        if(grabbable != null)
        {
            grabbable_in_range = grabbable;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(grabbable_in_range != null && grabbable_in_range == collision.attachedRigidbody.GetComponent<Grabbable>())
        {
            grabbable_in_range = null;
        }
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
            grab_joint.autoConfigureDistance = false;
            grab_joint.distance = 0.05f;
            grab_joint.frequency = 5;
            grab_joint.dampingRatio = 1;
            Vector3 raycast_direction = (transform.position - grabbable_in_range.transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, raycast_direction, 10);
            if(hit)
            {
                Vector2 local_point = grabbable_in_range.transform.InverseTransformPoint(hit.point);
                grab_joint.connectedAnchor = local_point;
            }
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame && grab_joint != null)
        {
            Destroy(grab_joint);
            grab_joint = null;
        }
    }
}
