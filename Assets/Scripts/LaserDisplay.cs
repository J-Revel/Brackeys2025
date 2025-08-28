using Unity.Mathematics;
using UnityEngine;

public enum LaserMode
{
    Grow,
    Damage,
}

public class LaserDisplay : MonoBehaviour
{
    public LayerMask raycast_layer;
    public float raycast_distance = 50;
    public Transform impact_vfx;
    public LineRenderer line_renderer;
    public LaserMode mode;

    void Start()
    {
        line_renderer = GetComponent<LineRenderer>();
        line_renderer.positionCount = 2;
    }

    void Update()
    {
        line_renderer.SetPosition(0, transform.position);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, raycast_distance, raycast_layer);
        if(hit)
        {
            line_renderer.SetPosition(1, hit.point);
            impact_vfx.transform.position = hit.point;
            switch(mode)
            {
                case LaserMode.Grow:
                    Growable growable = hit.collider.GetComponent<Growable>();
                    if (growable != null)
                        growable.Grow(Time.deltaTime);
                    break;
                case LaserMode.Damage:
                    break;
            }
        }
        else
        {
            float3 impact_point = transform.position + transform.up * raycast_distance;
            line_renderer.SetPosition(1, impact_point);
            impact_vfx.transform.position = impact_point;
        }
    }
}
