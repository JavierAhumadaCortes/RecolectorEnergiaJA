using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Target")]
    public Transform target; // El objeto que la cámara debe seguir

    [Header("Settings")]
    public float distance = 5.0f;
    public float sensitivityX = 0.2f;
    public float fixedYAngle = 20f;
    public float smoothSpeed = 10f;

    private float currentX = 0.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (!target) return;

        if (Mouse.current != null)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();
            currentX += mouseDelta.x * sensitivityX;
        }

        Quaternion rotation = Quaternion.Euler(fixedYAngle, currentX, 0);
        Vector3 direction = new Vector3(0, 0, -distance);
        Vector3 targetPosition = target.position + rotation * direction;

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        transform.LookAt(target.position);

        // NUEVO: Hace que el jugador gire horizontalmente junto con la cámara
        target.rotation = Quaternion.Euler(0, currentX, 0);
    }
}