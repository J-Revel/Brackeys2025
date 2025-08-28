using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public class CookieDestructionHandler : MonoBehaviour
{
    public static CookieDestructionHandler instance;
    public System.Action<float3, float3> on_destruction;
    public GameObject prefab;
    public float lifetime = 2;

    private class InstanceState
    {
        public GameObject element;
        public float lifetime;
    }
    private List<InstanceState> instances = new List<InstanceState>();

    void Awake()
    {
        instance = this;
        on_destruction += (float3 position, float3 scale) =>
        {
            GameObject prefab_instance = Instantiate(prefab, position, prefab.transform.rotation);
            prefab_instance.transform.localScale = scale;
            instances.Add(new InstanceState
            {
                element = prefab_instance,
                lifetime = lifetime,
            });
        };
    }

    void Update()
    {
       for(int i=instances.Count-1; i>=0; i--)
       {
            instances[i].lifetime -= Time.deltaTime;
            if (instances[i].lifetime <= 0)
            {
                Destroy(instances[i].element);
                instances.RemoveAt(i);
            }
       }
    }
}
