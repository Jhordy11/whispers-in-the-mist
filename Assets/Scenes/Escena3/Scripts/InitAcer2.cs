using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitAcer2 : MonoBehaviour
{
    public GameObject canvasToActivate; // Asigna aquí tu Canvas desde el Inspector

     private void OnTriggerEnter(Collider other)
    {
        ActivateCanvas();
        gameObject.SetActive(false);
    }

    void ActivateCanvas()
    {
        if (canvasToActivate)
        {
            canvasToActivate.SetActive(true); // Activa el Canvas
        }
    }
}
