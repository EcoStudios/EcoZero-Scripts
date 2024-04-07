using UnityEngine;


public class MobType
{

    public static readonly MobType TEST = Register(Resources.Load<GameObject>("Mobs/Test"), 100);

    public float MaxHealth { get; }
    public bool PathFinds { get; }
    public bool IsHostile { get; }
    public GameObject Prefab { get; }
    
    

    private static MobType Register(GameObject prefab, float maxHealth, bool isHostile = true, bool pathFinds = true)
    {
        return new MobType(prefab, maxHealth, isHostile, pathFinds);
    }
    
    

    public MobType(GameObject prefab, float maxHealth, bool isHostile, bool pathFinds)
    {
        MaxHealth = maxHealth;
        Prefab = prefab;
        IsHostile = isHostile;
        PathFinds = pathFinds;
    }

}
