using UnityEngine;
using System.Linq;

public class RollingPin : MonoBehaviour
{
    public float rotation_speed = 0;
    public float damage = 1;
    public float pushback = 100;
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Cookie cookie = collision.collider.GetComponent<Cookie>();
        if (cookie != null)
        {
            cookie.Damage(damage);
        }
    }

    void Update()
    {
        //GetComponent<SpriteRenderer>().transform.Rotate(new Vector3(0, 0, rotation_speed * Time.deltaTime));
    }
}
