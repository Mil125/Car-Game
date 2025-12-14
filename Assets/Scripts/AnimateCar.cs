using UnityEngine;

<<<<<<< HEAD
public class AnimateCar : MonoBehaviour
{
    public float acceleration = 25f;         // Gas pedal strength
    public float braking = 40f;              // Brake strength (S key)
    public float maxSpeed = 10f;             // Max forward speed
    public float turnSpeed = 90f;            // Steering rotation speed
    public float downforce = 50f;            // Keeps car on the road at high speeds

    public float currentSpeed = 0f;
    private Rigidbody rb;
=======
public class SimpleCarPhysicsController : MonoBehaviour
{
    public float acceleration = 10f;         // Gas pedal strength
    public float braking = 15f;              // Brake strength (S key)
    public float maxSpeed = 100f;             // Max forward speed
    public float turnSpeed = 90f;            // Steering rotation speed
    public float downforce = 50f;            // Keeps car on the road at high speeds

    private Rigidbody rb;
    private float currentSpeed = 0f;
>>>>>>> c4ece5b6574bbd835838267759069054ec7eabeb

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;

        // Prevent tipping over
        rb.constraints = RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        float steer = Input.GetAxis("Horizontal");
        bool gas = Input.GetKey(KeyCode.W);
        bool brake = Input.GetKey(KeyCode.S);

        // ===============================
        //        ACCELERATION
        // ===============================

        if (gas)
        {
            // Increase speed
            currentSpeed += acceleration * Time.fixedDeltaTime;
        }
        else if (brake)
        {
            // Apply braking force
            currentSpeed -= braking * Time.fixedDeltaTime;
        }
        // else → NO INPUT → MAINTAIN SPEED (cruise control)
        // currentSpeed stays the same

        // Clamp speed
        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed * 0.4f, maxSpeed);

        // ===============================
        //           STEERING
        // ===============================

        float turnAmount = steer * turnSpeed * Time.fixedDeltaTime;
        transform.Rotate(0, turnAmount, 0);

        // ===============================
        //           MOVEMENT
        // ===============================

        Vector3 forwardMove = transform.forward * currentSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMove);
<<<<<<< HEAD
=======

        // ===============================
        //      ANTI-JUMP STABILIZATION
        // ===============================

        rb.AddForce(-transform.up * downforce, ForceMode.Acceleration);
>>>>>>> c4ece5b6574bbd835838267759069054ec7eabeb
    }
}
