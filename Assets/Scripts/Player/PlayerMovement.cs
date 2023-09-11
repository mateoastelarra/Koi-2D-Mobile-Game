using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float scrollingSpeed = 1;
    [SerializeField] private float speed = 10;

    private Camera mainCamera;
    private float cameraScrollingSpeed;

    private Vector2 input;
    private Vector3 target;
    private PlayerInput playerInput;
    private Vector2 lastInput;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        
        transform.position = Vector3.zero;

        target = transform.position;

        input = transform.position;
        
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
    }

    private void UpScrolling()
    {
        transform.Translate(0, scrollingSpeed * Time.deltaTime, 0); 
    }
}
