using UnityEngine;
using UnityEngine.Playables;
public class CinematicManager : MonoBehaviour
{
    public PlayableDirector[] cinematics; // Array de cinemáticas
    public GameObject player; // Referencia al jugador para desactivar controles

    private int currentCinematicIndex = 0;

    // Método para iniciar una cinemática específica
    public void PlayCinematic(int index)
    {
        if (index < 0 || index >= cinematics.Length) return; // Validación de índice
        
        // Desactivar control del jugador
        //TogglePlayerControl(false);

        // Asignar evento para restaurar el control después de la cinemática
        //cinematics[index].stopped += OnCinematicEnd;
        if (cinematics[index] != null)
        {
            cinematics[index].Play();
            currentCinematicIndex = index;
        }
    }

    private void OnCinematicEnd(PlayableDirector director)
    {
        // Restaurar control del jugador cuando la cinemática termine
        TogglePlayerControl(true);
        director.stopped -= OnCinematicEnd;
    }

    private void TogglePlayerControl(bool isActive)
    {
        // Activar o desactivar los scripts de control del jugador (ajusta según tus scripts)
        player.GetComponent<PlayerMovement>().enabled = isActive;
    }
}
