using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayAudio : MonoBehaviour
{
     private PlayableDirector timeline;

    private void Start()
    {
        // Obtiene el componente PlayableDirector en el mismo objeto
        timeline = GetComponent<PlayableDirector>();

        if (timeline == null)
        {
            Debug.LogWarning("No se encontró PlayableDirector en este objeto. Asegúrate de que el PlayableDirector está en el mismo objeto que este script.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && timeline != null)
        {
            if (timeline.state == PlayState.Playing)
            {
                // Pausa si ya está reproduciendo
                timeline.Pause();
            }
            else
            {
                // Reproduce si está detenido o en pausa
                timeline.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && timeline != null)
        {
            // Reinicia la Timeline al salir del Trigger
            timeline.Stop();
        }
    }
}
