using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [Header("Camera Positioning")]
    [SerializeField] private Transform player; // Reference to the player
    [SerializeField] private float followSpeed = 5f; // Speed of smooth following
    [SerializeField] private float rotationSpeed = 10f; // Speed of camera rotation
    [SerializeField] private float scrollSensitivity = 2f; // Sensitivity for zooming
    
    [Header("Zoom Settings")]
    [SerializeField] private float minZoom = 5f; // Minimum distance for zooming
    [SerializeField] private float maxZoom = 20f; // Maximum distance for zooming

    private Vector3 offset; // Initial offset from the player
    private bool isDragging = false; // To check if left mouse button is pressed
    private Camera cam; // Reference to the camera component
    private float currentZoom; // Current zoom level

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player reference is not set on FollowCamera script.");
            return;
        }

        cam = Camera.main;
        offset = transform.position - player.position;
        currentZoom = offset.magnitude;
    }

    void Update()
    {
        HandleMouseInput();
    }

    void LateUpdate()
    {
        if (player == null) return;

        if (!isDragging)
        {
            SmoothFollowPlayer();
        }
    }

    void HandleMouseInput()
    {
        // Rotate camera when left mouse button is held
        /*
        if (Input.GetMouseButton(0))
        {
            isDragging = true;
            float horizontal = Input.GetAxis("Mouse X") * rotationSpeed;
            float vertical = -Input.GetAxis("Mouse Y") * rotationSpeed;

            transform.RotateAround(player.position, Vector3.up, horizontal);
            transform.RotateAround(player.position, transform.right, vertical);
        }
        else
        {
            isDragging = false;
        }
        */
        // Zoom in and out with the mouse wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity;
        currentZoom = Mathf.Clamp(currentZoom - scroll, minZoom, maxZoom);
        offset = offset.normalized * currentZoom;
    }

    void SmoothFollowPlayer()
    {
        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        transform.LookAt(player);
    }
}
