using MoreMountains.Feedbacks;
using UnityEngine;

public class Cookie : MonoBehaviour
{
    public float health;
    public float max_health;
    public Renderer renderer;
    private MaterialPropertyBlock property_block;
    public MMSpringFloat hurt_component;

    private void Start()
    {
        property_block = new MaterialPropertyBlock();
    }
    public void Damage(float damage)
    {
        health -= damage;
        renderer.GetPropertyBlock(property_block);
        property_block.SetFloat("_Destruction", 1 - health / max_health);
        renderer.SetPropertyBlock(property_block);
        if(health <= 0)
        {
            CookieDestructionHandler.instance.on_destruction(transform.position, transform.lossyScale);
            Destroy(gameObject);
        }
    }
}
