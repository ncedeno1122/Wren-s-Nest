using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public const float WALKSPEED = 3f;
    public const float RUNSPEED = 6f;

    [SerializeField] private bool m_IsGrounded;
    [SerializeField] private Vector3 m_LookInput = Vector3.zero;
    [SerializeField] private Vector3 m_MovementInput = Vector3.zero;
    [SerializeField] private Vector3 m_Velocity = Vector3.zero; 

    private CharacterController m_CC;
    private PlayerInput m_PlayerInput;
    private InputAction m_MoveIA, m_LookIA, m_SelectIA;

    public ScriptableCameraController m_PlayerCamera;

    private void Awake()
    {
        m_CC = GetComponent<CharacterController>();
        m_PlayerInput = GetComponent<PlayerInput>();
        m_MoveIA = m_PlayerInput.actions["Move"];
        m_LookIA = m_PlayerInput.actions["Look"];
        m_SelectIA = m_PlayerInput.actions["Select"];
    }

    private void Start()
    {
        // TODO: Set to Player Camera Initially
        m_PlayerCamera.RegisterPlayerController(this);
        m_PlayerCamera.ChangeState(new CameraPlayerState(m_PlayerCamera));
    }

    private void Update()
    {
        // Grounded Check
        m_IsGrounded = m_CC.isGrounded;
        
        // Walking or Running
        // TODO: Awful Temp fix - change this...
        var moveSpeed = (Input.GetKey(KeyCode.LeftShift) ? RUNSPEED : WALKSPEED);

        // Move & Send Camera information.
        if (m_PlayerCamera.IsPlayerControllable)
        {
            // Move Camera
            m_PlayerCamera.HandleLookInput(m_LookInput);

            // Move CC
            if (m_MovementInput != Vector3.zero)
            {
                Vector3 transformedMovementInput = m_PlayerCamera.transform.TransformDirection(m_MovementInput);
                transformedMovementInput[1] = 0f; // Negate vertical movement WITHOUT gravity :)
                m_CC.Move(transformedMovementInput * (moveSpeed * Time.deltaTime));
            }
        }

        // Apply Gravity if not Grounded
        if (!m_IsGrounded)
        {
            m_Velocity.y -= 9.81f * Time.deltaTime;
        }
        else
        {
            m_Velocity.y = 0f;
        }

        // Move CharacterController with Velocity
        m_CC.Move(m_Velocity * Time.deltaTime);
    }

    // + + + + | InputActions | + + + + 

    public void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 rawInput = ctx.ReadValue<Vector2>();
        m_MovementInput.Set(rawInput.x, 0f, rawInput.y);
    }

    public void OnLook(InputAction.CallbackContext ctx)
    {
        Vector2 rawInput = ctx.ReadValue<Vector2>();
        m_LookInput.Set(rawInput.y * -1f, rawInput.x, 0f);
    }

    public void OnSelect(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            m_PlayerCamera.HandleSelectInput();
        }
    }
}