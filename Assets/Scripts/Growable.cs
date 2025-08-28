using Unity.Mathematics;
using UnityEngine;

public class Growable : MonoBehaviour
{
    public Rigidbody2D rb;
    private float grow_ratio = 0;
    public float grow_duration = 5;
    public float2 grow_scale_range = new float2(1, 3);
    public float2 mass_range = new float2(1, 9);

    public void Grow(float deltaTime)
    {
        if (grow_ratio < 1)
        {
            grow_ratio += deltaTime / grow_duration;
        }
        else grow_ratio = 1;
        transform.localScale = Vector3.one * math.lerp(grow_scale_range.x, grow_scale_range.y, grow_ratio);
        rb.mass = math.lerp(mass_range.x, mass_range.y, grow_ratio);
    }
}
