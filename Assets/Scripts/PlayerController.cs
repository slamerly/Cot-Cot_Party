using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    public float MoveSpeed = 4.0f;
    public float SprintSpeed = 6.0f;
    public float RotationSpeed = 2.0f;
    public float SpeedMovementRate = 10f;
    public float JumpHeight = 1.2f;
    public float PlayerGravity = -15f;
    public float JumpStatusTimeout = 0.1f;
    public float FallStatusTimeout = 0.15f;
    public bool doubleJump = false;

    [Header("Player Grounded")]
    public bool Grounded = true;
    public float GroundedOffset = -0.14f;
    public float GroundedRadius = 0.5f;
    public LayerMask GroundLayers;

    [Header("Player Camera")]
    public GameObject PlayerCameraTarget;
    public float TopClamp = 90f;
    public float BottomClamp = -90f;

    //Player Camera
    private float playerCameraTargetPitch;

    // player
    private float speed;
    private float rotationVelocity;
    private float verticalVelocity;
    private float terminalVelocity = 53f;

    //timeout deltatime
    private float jumpStatusTimeoutDelta;
    private float fallStatusTimeoutDelta;

    private CharacterController characterController;
    private GameObject mainCamera;

    private void Awake()
    {
        // get a reference to our main camera
        if (mainCamera == null)
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        jumpStatusTimeoutDelta = JumpStatusTimeout;
        fallStatusTimeoutDelta = FallStatusTimeout;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        JumpAndGravity();
        GroundedCheck();
        Move();

    }
    private void LateUpdate()
    {
        CameraRotation();
    }

    private void GroundedCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
    }

    private void CameraRotation()
    {
        float deltaTimeMultiplier = 1.0f;
        playerCameraTargetPitch -= Input.GetAxis("Mouse Y") * RotationSpeed * deltaTimeMultiplier;
        rotationVelocity = Input.GetAxis("Mouse X") * RotationSpeed * deltaTimeMultiplier;

        playerCameraTargetPitch = ClampAngle(playerCameraTargetPitch, BottomClamp, TopClamp);

        PlayerCameraTarget.transform.localRotation = Quaternion.Euler(playerCameraTargetPitch, 0f, 0f);

        transform.Rotate(Vector3.up * rotationVelocity);
    }

    private void Move()
    {
        float targetSpeed = 0f;
        float currentHorizontalSpeed = new Vector3(characterController.velocity.x, 0f, characterController.velocity.z).magnitude;
        float speedOffset = 0.1f;
        Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;

        if (Input.GetKey(KeyCode.LeftShift))
            targetSpeed = SprintSpeed;
        else
            targetSpeed = MoveSpeed;
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            targetSpeed = 0f;

        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * SpeedMovementRate);
            speed = Mathf.Round(speed * 1000f) / 1000f;
        }
        else
        {
            speed = targetSpeed;
        }

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            inputDirection = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");

        characterController.Move(inputDirection.normalized * (speed * Time.deltaTime) + new Vector3(0f, verticalVelocity, 0f) * Time.deltaTime);
    }

    private void JumpAndGravity()
    {
        if (Grounded)
        {
            fallStatusTimeoutDelta = FallStatusTimeout;
            if (verticalVelocity < 0f)
                verticalVelocity = -2f;
            doubleJump = true;
            if (Input.GetButtonDown("Jump") && jumpStatusTimeoutDelta <= 0f)
            {
                verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * PlayerGravity);
            }
            if (jumpStatusTimeoutDelta >= 0f)
                jumpStatusTimeoutDelta -= Time.deltaTime;
        }
        else
        {
            if (Input.GetButtonDown("Jump") && doubleJump && jumpStatusTimeoutDelta > 0f)
            {
                verticalVelocity += Mathf.Sqrt(JumpHeight * -2f * PlayerGravity);
                doubleJump = false;
            }
            
            jumpStatusTimeoutDelta = JumpStatusTimeout;
            if (fallStatusTimeoutDelta >= 0f)
                fallStatusTimeoutDelta -= Time.deltaTime;
        }

        if (verticalVelocity < terminalVelocity)
            verticalVelocity += PlayerGravity * Time.deltaTime;
    }

    private static float ClampAngle(float CamAngle, float angleMin, float angleMax)
    {
        if (CamAngle < -360f) CamAngle += 360f;
        if (CamAngle > 360f) CamAngle -= 360f;
        return Mathf.Clamp(CamAngle, angleMin, angleMax);
    }

    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (Grounded) 
            Gizmos.color = transparentGreen;
        else 
            Gizmos.color = transparentRed;

        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
    }
}
