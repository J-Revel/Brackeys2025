using UnityEngine;

public class FixedRotation : MonoBehaviour
{
    public float angle = 0;
    void Start()
    {
        
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, angle);   
    }
}
