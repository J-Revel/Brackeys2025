using UnityEngine;
using System.Linq;

public class RollingPin : MonoBehaviour
{
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string[] undestroyables = { "Cookie Detector" };
        if (!undestroyables.Contains(collision.gameObject.name)) {
            Destroy(collision.gameObject);
        }
    }

    void Update()
    {
        GetComponent<SpriteRenderer>().transform.Rotate(new Vector3(0, 0, 90 * Time.deltaTime));
    }
}
