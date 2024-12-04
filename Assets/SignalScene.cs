using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
public class SignalScene : MonoBehaviour
{
    public PlayableDirector timeline;
    public KeyCode skipKey = KeyCode.Escape; 
    public void ChangeEscene()
    {
        SceneManager.LoadScene(2);
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
        ChangeEscene();
    }
}
