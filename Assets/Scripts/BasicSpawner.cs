using UnityEngine;

public class BasicSpawner : MonoBehaviour
{
    public Transform to_spawn;
    public void Spawn()
    {
        Instantiate(to_spawn, transform.position, to_spawn.rotation);
    }
}
