using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class World : MonoBehaviour
{
    public static Dictionary<String, Mob> SpawnedMobs = new Dictionary<string, Mob>();


    public static GameObject FindMobGameObject(Mob mob)
    {
        return GameObject.Find(mob.UID);
    }

    public static Mob FindMobFromGameObject(GameObject gameObject)
    {
        return SpawnedMobs[gameObject.name];
    }


    public static void Spawn(Mob mob, Vector3 location)
    {
        if (!mob.IsAlive)
        {
            MobType type = mob.Type;
            GameObject gameObject = Instantiate(type.Prefab);
            float random = Random.Range(1000000, 2000000);
            string id = Convert.ToString(random);
            if (SpawnedMobs.ContainsKey(id))
            {
                while (SpawnedMobs.ContainsKey(id))
                {
                    random = Random.Range(1000000, 2000000);
                    id = Convert.ToString(random);
                }
            }
            
            gameObject.transform.position = location;
            gameObject.name = id;
            mob.UID = id;
            SpawnedMobs.Add(mob.UID, mob);
            mob.IsAlive = true;
            
            // Hostile mob's pathfinding
            if (mob.Type.PathFinds && mob.Type.IsHostile)
            {
                gameObject.AddComponent<NavMeshAgent>();
                gameObject.AddComponent<HostilePathfinding>();
                mob.MeshAgent = gameObject.GetComponent<NavMeshAgent>();
            }
            // ADD: Non-hostile pathfinding
        }
    }
}
