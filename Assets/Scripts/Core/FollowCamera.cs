using UnityEngine;

namespace Core
{
    public class FollowCamera : MonoBehaviour
    {
        // The target the camera will follow (e.g., the Player)
        [SerializeField] private Transform target;

        // Adjustable parameters for distance and rotation
        [Header("Camera Positioning")]
        [SerializeField] private Vector3 offset = new Vector3(0.1f, 0.1f, -10f);
        [SerializeField] private float rotationSpeed = 5f;

        [Header("Follow Settings")]
        [SerializeField] private bool smoothFollow = true;
        [SerializeField] private float followSpeed = 10f;

        [Header("Zoom Settings")]
        [SerializeField] private float zoomSpeed = 5f;
        [SerializeField] private float minZoom = 2f;
        [SerializeField] private float maxZoom = 20f;
        [SerializeField] private float currentZoom;

        private void Start()
        {
            currentZoom = offset.magnitude;
        }

        private void LateUpdate()
        {
            if (target == null)
            {
                Debug.LogWarning("FollowCamera: No target assigned!");
                return;
            }

            // Handle zoom input
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            currentZoom -= scrollInput * zoomSpeed;
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

            // Adjust the offset based on the current zoom
            offset = offset.normalized * currentZoom;

            // Calculate the desired position
            Vector3 desiredPosition = target.position + offset;

            // Move the camera to the desired position
            if (smoothFollow)
            {
                transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = desiredPosition;
            }

            // Smoothly rotate to look at the target
            Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
