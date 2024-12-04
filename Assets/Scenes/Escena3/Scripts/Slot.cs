using UnityEngine;

public class Slot : MonoBehaviour
{
    public GameObject correctRockSol1; // La roca correcta que debe colocarse en esta ranura.
    public GameObject correctRockSol2; // La roca correcta que debe colocarse en esta ranura.
    public GameObject holeSol; // Elemento visual del hueco.
    public GameObject placedRockSol; // Elemento visual de la roca colocada.
    public GameObject correctRockLuna1; // La roca correcta que debe colocarse en esta ranura.
    public GameObject correctRockLuna2; // La roca correcta que debe colocarse en esta ranura.
    public GameObject holeLuna; // Elemento visual del hueco.
    public GameObject placedRockLuna; // Elemento visual de la roca colocada.
    public CinematicManager cinematicManager;
    public int cinematicIndex;

    private void Start()
    {
        // Asegúrate de que el hueco esté visible y la roca colocada esté oculta al inicio.
        holeSol.SetActive(true);
        holeLuna.SetActive(true);
        placedRockSol.SetActive(false);
        placedRockLuna.SetActive(false);
    }
    void Update(){
        if (placedRockLuna.activeSelf && placedRockSol.activeSelf)
        {
           gameObject.SetActive(false);
        }
    }
    public bool IsCorrectRock(GameObject rock)
    {
        // Compara si la roca que el jugador tiene es la correcta para esta ranura.

        return rock == correctRockSol1 || rock == correctRockLuna1 || rock == correctRockSol2 || rock == correctRockLuna2;
    }

    public void PlaceRock(GameObject rock)
    {
        if (rock == correctRockSol1 || rock == correctRockSol2)
        {
            // Oculta el hueco y muestra la roca colocada.
            holeSol.SetActive(false);
            placedRockSol.SetActive(true);
        }
        if (rock == correctRockLuna1 || rock ==  correctRockLuna2)
        {
            // Oculta el hueco y muestra la roca colocada.
            holeLuna.SetActive(false);
            placedRockLuna.SetActive(true);
        }
    }

    private void OnDisable()
    {
        Debug.Log("Inicia la animacion");
        cinematicManager.PlayCinematic(cinematicIndex);
    }
}
