using Player.Inventory;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerManager : MonoBehaviour
{
    // Non-static vars so can switch values in Unity 
    public Camera playerCamera;
    public Animator playerAnimator;
    public CharacterController playerController;
    public float mouseSensitivity = 5;
    public float mouseLimit = 45;
    public float walkingSpeed = 5;
    public float runningSpeed = 8;
    public float fov = 60;
    public float pickupDistance = 5;
    public float jumpingHeight = 3;
    public float gravity = 10.0f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    // Static vars so can use the vars above in other files (I don't know another way of doing it.)
    public static Transform Player;
    public static Camera PlayerCamera;
    public static Animator PlayerAnimator;
    public static CharacterController PlayerController;
    public static float MouseSensitivity;
    public static float MouseLimit;
    public static float RunningSpeed;
    public static float WalkingSpeed;
    public static float PlayerFOV;
    public static float PickupDistance;
    public static float JumpingHeight;
    public static float Gravity;
    public static bool IsGrounded;
    public static Transform GroundCheck;
    public static float GroundDistance;
    public static LayerMask GroundMask;
    private static Mob _testMob;

    

    private void Start()
    {
        Player = transform;
        PlayerCamera = playerCamera;
        PlayerAnimator = playerAnimator;
        MouseSensitivity = mouseSensitivity;
        MouseLimit = mouseLimit;
        RunningSpeed = runningSpeed;
        WalkingSpeed = walkingSpeed;
        PlayerFOV = fov;
        PickupDistance = pickupDistance;
        JumpingHeight = jumpingHeight;
        PlayerController = playerController;
        Gravity = gravity;
        GroundCheck = groundCheck;
        GroundDistance = groundDistance;
        GroundMask = groundMask;
    }


    private void LateUpdate()
    {

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            InventoryManager.SetHandQuick(0);
        } else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            InventoryManager.SetHandQuick(1);
        } else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            InventoryManager.SetHandQuick(2);
        } else if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            InventoryManager.SetHandQuick(3);
        } else if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            InventoryManager.SetHandQuick(4);
        }

        if (Input.GetMouseButtonUp(1))
        {
            ItemManager.SpawnItem(new ItemStack(ItemType.AXE, 1), Player.localPosition + Player.forward + Player.up );
            
        }
    
        if (Input.GetKeyUp(KeyCode.M))
        {
            _testMob = new Mob(MobType.TEST);
            World.Spawn(_testMob, Player.position + Player.forward + Player.up);
        }
        
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (InventoryManager.GetInventorySlot(InventoryManager.CurrentHeldSlot).Type != ItemType.AIR) 
            {
                InventoryManager.DropItem(InventoryManager.GetInventorySlot(InventoryManager.CurrentHeldSlot), Player.localPosition + Player.forward + Player.up);
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (InventoryManager.GetInventory().activeSelf)
            {
                InventoryManager.CloseInventory();
            }
            else
            {
                InventoryManager.OpenInventory();
            }
        }
        
        
        
    }
}
