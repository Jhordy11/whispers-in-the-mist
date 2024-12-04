using UnityEngine;
using UnityEngine.Playables;

public class JumpCinematic : MonoBehaviour
{
    public PlayableDirector timeline; // Timeline para la cinemática
    private bool isCinematicActive = false; // Controla si la cinemática está en curso

    void Update()
    {
        if (timeline != null)
        {
            // Detecta si la Timeline está reproduciendo
            if (timeline.state == PlayState.Playing)
            {
                isCinematicActive = true;
            }
            else
            {
                isCinematicActive = false;
            }
        }
    }

    public void SkipToEnd()
    {
        if (timeline != null && isCinematicActive)
        {
            timeline.time = timeline.duration; // Salta al final
            timeline.Evaluate();
            isCinematicActive = false; // Marca como no activa
        }
    }

    public bool IsCinematicActive()
    {
        return isCinematicActive; // Devuelve si una cinemática está activa
    }
}
