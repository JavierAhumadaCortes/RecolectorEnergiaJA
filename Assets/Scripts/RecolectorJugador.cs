using TMPro; 
using UnityEngine; 
  
public class RecolectorJugador : MonoBehaviour 
{ 
    [SerializeField] private int energiaObjetivo = 5; 
    [SerializeField] private TMP_Text textoContador; 
    [SerializeField] private GameObject panelVictoria; 
  
    private int energiaActual = 0; 
  
    private void Start() 
    { 
        ActualizarInterfaz(); 
    } 
  
    private void OnTriggerEnter(Collider otro) 
    { 
        if (!otro.CompareTag("Energia")) return; 
  
        energiaActual++; 
        Destroy(otro.gameObject); 
        ActualizarInterfaz(); 
    } 
  
    private void ActualizarInterfaz() 
    { 
        textoContador.text = 
            $"Energía: {energiaActual} / {energiaObjetivo}"; 
  
        panelVictoria.SetActive( 
            energiaActual >= energiaObjetivo 
        ); 
    }
}