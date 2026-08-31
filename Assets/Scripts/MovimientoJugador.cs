using UnityEngine; 
using UnityEngine.InputSystem; 

[RequireComponent(typeof(CharacterController))] 
public class MovimientoJugador : MonoBehaviour 
{ 
    [Header("Movimiento")] 
    [SerializeField] private float velocidad = 5f; 
    
    [Header("Salto y gravedad")] 
    [SerializeField] private float alturaSalto = 1.5f; 
    [SerializeField] private float gravedad = -9.81f; 
    
    private Vector2 entrada; 
    private CharacterController controlador; 
    private float velocidadVertical; 
    private bool saltoSolicitado; 

    private void Awake() 
    { 
        controlador = GetComponent<CharacterController>(); 
    } 

    public void OnMove(InputValue valor) 
    { 
        entrada = valor.Get<Vector2>(); 
    } 

    public void OnJump(InputValue valor) 
    { 
        if (valor.isPressed) 
            saltoSolicitado = true; 
    } 

    private void Update() 
    { 
        bool enSuelo = controlador.isGrounded; 
        if (enSuelo && velocidadVertical < 0f) 
            velocidadVertical = -2f; 

        if (saltoSolicitado && enSuelo) 
            velocidadVertical = Mathf.Sqrt(alturaSalto * -2f * gravedad); 
        
        saltoSolicitado = false; 
        velocidadVertical += gravedad * Time.deltaTime; 

        // 1. Calculamos el movimiento en el plano local del jugador (X y Z)
        Vector3 movimientoXZ = new Vector3(entrada.x, 0f, entrada.y) * velocidad;

        // 2. Transformamos la dirección local a dirección global basada en hacia dónde mira el objeto (y la cámara)
        Vector3 movimientoGlobal = transform.TransformDirection(movimientoXZ);

        // 3. Añadimos la velocidad vertical (gravedad y salto)
        Vector3 movimientoFinal = new Vector3(movimientoGlobal.x, velocidadVertical, movimientoGlobal.z);

        // 4. Movemos el personaje
        controlador.Move(movimientoFinal * Time.deltaTime); 
    } 
}