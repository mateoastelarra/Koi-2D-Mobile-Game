using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] PezDeBarraDeProgreso progressBar;

    private Camera mainCamera;
    private float scrollingSpeed;
    private float[] scrollingSpeedForEachPhase;

    private Vector3 target;
    private PlayerInput playerInput;
    private Vector2 input;
    private Vector2 lastInput;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        
        transform.position = Vector3.zero;

        target = transform.position;

        input = transform.position;
        
        mainCamera = Camera.main;

        scrollingSpeedForEachPhase = mainCamera.GetComponent<CameraScroller>().scrollingSpeedForEachPhase;

        scrollingSpeed = scrollingSpeedForEachPhase[0];
        
    }
    
    private void OnEnable()
    {
        progressBar.OnChangePhase += ChangeScrollingSpeed;
    }

    private void OnDisable()
    {
        progressBar.OnChangePhase -= ChangeScrollingSpeed;
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

        target.y += scrollingSpeed * Time.deltaTime; 
    }

    private void UpScrolling()
    {
        transform.Translate(0, scrollingSpeed * Time.deltaTime, 0); 
    }

    private void ChangeScrollingSpeed(int phase)
    {
        if (phase < scrollingSpeedForEachPhase.Length)
            scrollingSpeed = scrollingSpeedForEachPhase[phase];
    }
}
