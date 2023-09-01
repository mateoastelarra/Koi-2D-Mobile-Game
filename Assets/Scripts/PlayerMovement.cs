using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float scrollingSpeed = 1;
    [SerializeField] private float forceMagnitud = 20f;
    [SerializeField] private float dontMoveifTouchclose = 0.3f;
    [SerializeField] private float speed;
    [SerializeField] GameObject stick;

    private Camera mainCamera;
    private Rigidbody2D RB;

    private PlayerInput playerInput;
    private Vector2 input;

    private Vector3 target;

    private bool touchControlsActivated = false;


    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();

        playerInput = GetComponent<PlayerInput>();

        transform.position = new Vector3(0, 0, 0);
        
        mainCamera = Camera.main;

        target = transform.position;
    }

    private void Update()
    {
        if (!touchControlsActivated)
            UpdateInput();
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            touchControlsActivated = !touchControlsActivated;
            Debug.Log("Touch controls activated/deactivated");
            stick.SetActive(!touchControlsActivated);
        }
    }

    void FixedUpdate()
    {
        ConstantFall();
        if (touchControlsActivated)
        {
            UpdateDestiny();
            return;
        }   
        MoveWithGamePad();
    }

    private void UpdateDestiny()
    {
        if ((target - transform.position).sqrMagnitude > dontMoveifTouchclose)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        target.y += scrollingSpeed * Time.deltaTime;
    }

    private void ConstantFall()
    {
        transform.Translate(0, scrollingSpeed * Time.deltaTime, 0);
    }

    public void UpdateInput()
    {
        input = playerInput.actions["MoveGamepad"].ReadValue<Vector2>();
    }

    public void MoveWithGamePad()
    {
        RB.AddRelativeForce(new Vector2(input.x, input.y) * forceMagnitud, ForceMode2D.Force);
        Debug.Log(new Vector2(input.x, input.y) * forceMagnitud);
    }

    public void Move2(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed && touchControlsActivated)
        {
            Vector2 touchPos = Touchscreen.current.primaryTouch.position.ReadValue();

            Vector2 worldPos = mainCamera.ScreenToWorldPoint(touchPos);

            target = new Vector3(worldPos.x, worldPos.y, 0);

            Debug.Log("touch");
        }
    }

}
