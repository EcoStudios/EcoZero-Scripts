using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static float _rotation;
    private static Vector3 _velocity;                  

    void Start()
    {
        // Cursor 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }




    void Update()
    {

        // Setting Is Grounded
        PlayerManager.IsGrounded = Physics.CheckSphere(PlayerManager.GroundCheck.position, PlayerManager.GroundDistance,
            PlayerManager.GroundMask);

        if (PlayerManager.IsGrounded)
        {
            _velocity.y = -2f;
        }

        if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
        {
            Vector3 vector3;
            // Running
            if (Input.GetKey(KeyCode.LeftShift))
            {
                float h = Input.GetAxis("Horizontal");
                float v = Input.GetAxis("Vertical");
                vector3 = PlayerManager.Player.right * h + PlayerManager.Player.forward * v;
                vector3 *= PlayerManager.RunningSpeed;
                PlayerManager.PlayerAnimator.SetBool("isRunning", true);
                PlayerManager.PlayerAnimator.SetBool("isWalking", false);
                PlayerManager.PlayerCamera.fieldOfView = PlayerManager.PlayerFOV + 1.5f;
            }
            // Walking  
            else
            {
                float h = Input.GetAxis("Horizontal");
                float v = Input.GetAxis("Vertical");
                vector3 = PlayerManager.Player.right * h + PlayerManager.Player.forward * v;
                vector3 *= PlayerManager.WalkingSpeed;
                PlayerManager.PlayerAnimator.SetBool("isWalking", true);
                PlayerManager.PlayerAnimator.SetBool("isRunning", false);
                PlayerManager.PlayerCamera.fieldOfView = PlayerManager.PlayerFOV;
            }

            PlayerManager.PlayerController.Move(vector3 * Time.deltaTime);

        }
        // Idling
        else
        {
            PlayerManager.PlayerAnimator.SetBool("isWalking", false);
            PlayerManager.PlayerAnimator.SetBool("isRunning", false);
            PlayerManager.PlayerCamera.fieldOfView = PlayerManager.PlayerFOV;
        }
        
        
        // Jumping
        if (PlayerManager.IsGrounded && Input.GetKey(KeyCode.Space))
        {
            _velocity.y = Mathf.Sqrt(PlayerManager.JumpingHeight * -2f * PlayerManager.Gravity);
        }

        // Gravity
        _velocity.y += PlayerManager.Gravity * Time.deltaTime;
        PlayerManager.PlayerController.Move(_velocity * Time.deltaTime);



        // Camera Movement
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            float mouseY = Input.GetAxis("Mouse Y");
            float mouseX = Input.GetAxis("Mouse X");
            _rotation += -mouseY * PlayerManager.MouseSensitivity;
            _rotation = Mathf.Clamp(_rotation, -PlayerManager.MouseLimit, PlayerManager.MouseLimit);

            PlayerManager.PlayerCamera.transform.localRotation = Quaternion.Euler(_rotation, 0, 0);
            PlayerManager.Player.rotation *= Quaternion.Euler(0, mouseX * PlayerManager.MouseSensitivity, 0);
        }

    }
}