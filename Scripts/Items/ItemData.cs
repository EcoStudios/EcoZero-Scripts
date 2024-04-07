using JetBrains.Annotations;
using UnityEngine;

public class ItemData
{

    public static readonly ItemData DEFAULT = new ItemData(0.5f, false);

    public float AttackDamage { get; set; }
    public Quaternion? HandRotation { get; set; }
    public Vector3? HandVector3 { get; set; }
    public Vector3? HandScale { get; set; }

    public bool HasCustomAttackAnimation;
    public string AnimationBoolName;
    

    public ItemData(float attackDamage, bool hasCustomAttackAnimation, [CanBeNull] string animationBoolName = null, Quaternion? handRotation = null, Vector3? handVector3 = null, Vector3? handScale = null)
    {
        AttackDamage = attackDamage;
        HandRotation = handRotation;
        HandVector3 = handVector3;
        HandScale = handScale;
        HasCustomAttackAnimation = hasCustomAttackAnimation;
        AnimationBoolName = animationBoolName;
    }
}
