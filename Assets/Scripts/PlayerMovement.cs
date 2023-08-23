using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float scrollingSpeed = 1;
    [SerializeField] private float forceMagnitude = 0.5f;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float dontMoveifTouchclose = 0.5f;

    private Camera mainCamera;
    private Rigidbody2D RB;

    private Vector2 movementDirection;
    private Vector2 lastTouch;

    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        
        transform.position = new Vector3(0, 0, 0);
        
        mainCamera = Camera.main;
    }

    private void Update()
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
            //movementDirection.z = 0;
            if (movementDirection.sqrMagnitude < dontMoveifTouchclose)
            {
                movementDirection = Vector2.zero;
            }
            else
            {
                movementDirection.Normalize();
            }
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

    void FixedUpdate()
    {
        transform.Translate(0, scrollingSpeed * Time.deltaTime, 0);
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
}
