using UnityEngine;
using System.Linq;

public class RollingPin : MonoBehaviour
{
    public float rotation_speed = 0;
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string[] undestroyables = { "Cookie Detector" };
        if (!undestroyables.Contains(collision.gameObject.name))
        {
            Destroy(collision.gameObject);
        }
    }

    void Update()
    {
        GetComponent<SpriteRenderer>().transform.Rotate(new Vector3(0, 0, rotation_speed * Time.deltaTime));
    }
}
