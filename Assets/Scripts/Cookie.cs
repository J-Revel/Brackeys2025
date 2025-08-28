using MoreMountains.Feedbacks;
using UnityEngine;

public class Cookie : MonoBehaviour
{
    public float health;
    public float max_health;
    public Renderer renderer;
    private MaterialPropertyBlock property_block;
    public MMSpringFloat hurt_component;
    public ParticleSystem low_health_vfx;
    public float low_health_threshold = 0.4f;

    private void Start()
    {
        property_block = new MaterialPropertyBlock();
    }
    public void Damage(float damage)
    {
        float health_ratio = health / (float)max_health;
        float new_health_ratio = (health - damage) / (float)max_health;
        if(health_ratio > low_health_threshold && new_health_ratio <= low_health_threshold)
        {
            low_health_vfx.Play();
        }
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
