using UnityEngine;

public class Cookie : MonoBehaviour
{
    public float health;
    public float max_health;
    public Renderer renderer;
    private MaterialPropertyBlock property_block;

    private void Start()
    {
        property_block = new MaterialPropertyBlock();
    }
    public void Damage(float damage)
    {
        health -= damage;
        renderer.GetPropertyBlock(property_block);
        property_block.SetFloat("_Damage", 1 - health / max_health);
        if(health <= 0)
        {
            CookieDestructionHandler.instance.on_destruction(transform.position, transform.lossyScale);
            Destroy(gameObject);
        }
    }
}
