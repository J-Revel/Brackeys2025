using Unity.Mathematics;
using UnityEngine;

[System.Serializable]
public struct PatrollingEnemyConfig
{
    public float angle;
    public float2 altitude_range;
}

public class EnemySpawner : MonoBehaviour
{
    public Transform arena_min_pos, arena_max_pos;
    public PatrollingEnemy laser_enemy_prefab;
    public float laser_enemy_spawn_interval = 10;
    private float laser_enemy_spawn_time;
    public float laser_enemy_probability = 3;
    public PatrollingEnemyConfig[] laser_enemy_configs;
    public PatrollingEnemy melee_enemy_prefab;
    public float melee_enemy_probability = 2;
    public float2 melee_altitude_range;
    
    void Start()
    {
        
    }

    void Update()
    {
        laser_enemy_spawn_time += Time.deltaTime;
        if(laser_enemy_spawn_time > laser_enemy_spawn_interval)
        {
            if (UnityEngine.Random.Range(0, 1.0f) < laser_enemy_probability / (laser_enemy_probability + melee_enemy_probability))
            {
                int config_index = UnityEngine.Random.Range(0, laser_enemy_configs.Length);
                float2 altitude_range = laser_enemy_configs[config_index].altitude_range;
                float3 spawn_pos = new float3(
                    arena_min_pos.position.x + UnityEngine.Random.Range(0, 1.0f) * (arena_max_pos.position.x - arena_min_pos.position.x),
                    UnityEngine.Random.Range(altitude_range.x, altitude_range.y),
                    0
                );
                PatrollingEnemy patrolling_enemy = Instantiate(laser_enemy_prefab, spawn_pos, laser_enemy_prefab.transform.rotation * Quaternion.Euler(0, 0, laser_enemy_configs[config_index].angle));
                patrolling_enemy.target_angle_offset = laser_enemy_configs[config_index].angle;
                patrolling_enemy.altitude_range = laser_enemy_configs[config_index].altitude_range;
            }
            else
            {
                float3 spawn_pos = new float3(
                    arena_min_pos.position.x + UnityEngine.Random.Range(0, 1.0f) * (arena_max_pos.position.x - arena_min_pos.position.x),
                    UnityEngine.Random.Range(melee_altitude_range.x, melee_altitude_range.y),
                    0
                );
                Instantiate(melee_enemy_prefab, spawn_pos, laser_enemy_prefab.transform.rotation);
            }
            laser_enemy_spawn_time = 0;
        }
    }
}
