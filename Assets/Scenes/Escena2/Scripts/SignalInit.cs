using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class SignalInit : MonoBehaviour
{
    public GameObject afterCinema;
    public PlayableDirector timeline;
    public KeyCode skipKey = KeyCode.Escape; 
    public void ActivateNewCanvas()
    {
        afterCinema.SetActive(true);
    }
    
    private void Update()
    {
        // Detecta si se presionó la tecla para omitir
        if (timeline != null && Input.GetKeyDown(skipKey))
        {
            SkipToEnd();
        }
    }

    private void SkipToEnd()
    {
        if (timeline != null)
        {
            // Salta al final de la Timeline
            timeline.time = timeline.duration;
            timeline.Evaluate();
            // timeline.Stop();
        }
        ActivateNewCanvas();
    }
}
