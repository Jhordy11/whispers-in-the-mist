using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Choque : MonoBehaviour
{
    public Canvas CanvasFinish;
    public GameObject soundObject; 
    private void Start()
    {
        // Asegúrate de que el Canvas esté desactivado al inicio
        if (CanvasFinish)
        {
            CanvasFinish.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Activa el Canvas cuando se detecta la colisión
        if (CanvasFinish)
        {
            CanvasFinish.gameObject.SetActive(true);
        }
        soundObject.SetActive(false);
    }

}
