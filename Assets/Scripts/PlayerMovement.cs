using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float scrollingSpeed = 1;
    [SerializeField] private float forceMagnitude = 0.5f;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float dontMoveifTouchclose = 0.5f;
    [SerializeField] private float speed = 10;

    private Camera mainCamera;
    private Rigidbody2D RB;
    private float cameraScrollingSpeed;

    private Vector2 movementDirection;
    private Vector2 lastTouch;

    private Vector2 input;
    private Vector3 target;
    private PlayerInput playerInput;
    private Vector3 lastTarget;
    private Vector2 lastInput;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        RB = GetComponent<Rigidbody2D>();
        
        transform.position = Vector3.zero;

        target = transform.position;

        input = transform.position;

        lastTarget = transform.position;
        
        mainCamera = Camera.main;

        cameraScrollingSpeed = mainCamera.GetComponent<CameraScroller>().scrollingSpeed;
    }

    private void Update()
    {
        GetInputAndDecideTarget();
    }

    void FixedUpdate()
    {
        UpScrolling();
        UpdateDestinyAndGoTo();
    }

    private void GetInputAndDecideTarget()
    {
        input = playerInput.actions["Move"].ReadValue<Vector2>();
        
        if (!input.Equals(lastInput))
        {
            Vector2 worldPos = mainCamera.ScreenToWorldPoint(input);

            target = new Vector3(worldPos.x, worldPos.y, 0);
            
            lastInput = input;    
        }
        
    }

    private void UpdateDestinyAndGoTo()
    {   
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        target.y += cameraScrollingSpeed * Time.deltaTime;

        lastTarget = target; 
    }

    private void UpScrolling()
    {
        transform.Translate(0, scrollingSpeed * Time.deltaTime, 0); 
    }


    private void ApplyForce()
    {
        if (movementDirection == Vector2.zero)
        {
            return;
        }
        else
        {
            RB.AddForce(movementDirection * forceMagnitude / 50, ForceMode2D.Force);

            RB.velocity = Vector2.ClampMagnitude(RB.velocity, maxVelocity);
        }
    }

    private void DecideWhereToGo()
    {
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPos = Touchscreen.current.primaryTouch.position.ReadValue();

            Vector2 worldPos = mainCamera.ScreenToWorldPoint(touchPos);

            if (touchPos == lastTouch)
            {
                movementDirection = worldPos - new Vector2(transform.position.x, transform.position.y);
                if (movementDirection.sqrMagnitude < dontMoveifTouchclose)
                {
                    movementDirection = Vector2.zero;
                    RB.velocity = Vector2.zero;
                }
                return;
            }

            lastTouch = touchPos;

            movementDirection = worldPos - new Vector2(transform.position.x, transform.position.y);
            movementDirection.Normalize();
        }
        else
        {
            movementDirection = Vector3.zero;
            RB.velocity = Vector2.zero;
        }

        if (!Touchscreen.current.primaryTouch.press.isPressed)
        {
            lastTouch = Vector2.zero;
        }
    }

}
