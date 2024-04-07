using UnityEngine;
using UnityEngine.AI;

public class Mob
{

    public MobType Type { get; }
    public float Health { get; set; }
    public bool IsAngry { get; set; }
    public string UID { get; set; }
    public bool IsAlive { get; set; }
    public NavMeshAgent MeshAgent { get; set; }


    public Mob(MobType mobType, float health = 100)
    {
        Type = mobType;
        Health = health;
        IsAlive = false;
        IsAngry = false;
    }
}
   
