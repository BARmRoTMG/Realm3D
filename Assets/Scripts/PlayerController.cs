using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public GameObject player;
    public Camera _camera;

    public float speed = 12f;
    public float walk = 12f;
    public float sprint = 20f;
    public float gravity = -19.62f;
    public float jumpHeight = 3f;

    private float target_height = 1.8f;
    private float previous_y = 0;
    private bool is_crouching = false;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    bool isGrounded;

    private void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprint;
        } else
        {
            speed = walk;
        }

        /* previous_y = controller.transform.position.y - controller.height / 2 - controller.skinWidth;

         if (Input.GetKeyDown("c"))
         {
             if (is_crouching == false)
             {
                 is_crouching = true;
                 target_height = 0.9f;
             }
             else
             {
                 is_crouching = false;
                 target_height = 1.8f;
             }
         }
         controller.height = Mathf.Lerp(controller.height, target_height, 5f * Time.deltaTime);

         _camera.transform.position = Vector3.Lerp(_camera.transform.position, new Vector3(_camera.transform.position.x, controller.transform.position.y + target_height / 2 - 0.1f, _camera.transform.position.z), 5f * Time.deltaTime);

         controller.transform.position = Vector3.Lerp(controller.transform.position, new Vector3(controller.transform.position.x, previous_y + target_height / 2 + controller.skinWidth, controller.transform.position.z), 5f * Time.deltaTime);
         */
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
