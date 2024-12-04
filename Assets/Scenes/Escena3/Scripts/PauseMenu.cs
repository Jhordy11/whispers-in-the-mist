using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Referencia al menú de pausa
    public KeyCode pauseKey = KeyCode.Escape; // Tecla de pausa
    private bool isPaused = false; // Estado del juego (pausado o no)
    public JumpCinematic cinematicManager; // Referencia al script de las cinemáticas

    void Update()
    {
        // Si se presiona la tecla de pausa
        if (Input.GetKeyDown(pauseKey))
        {
            // Verifica si una cinemática está activa
            if (cinematicManager != null && cinematicManager.IsCinematicActive())
            {
                cinematicManager.SkipToEnd(); // Omite la cinemática
            }
            else
            {
                if (isPaused)
                {
                    Resume(); // Reanuda el juego
                }
                else
                {
                    Pause(); // Pausa el juego
                }
            }
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true); // Activa el menú de pausa
        Time.timeScale = 0f; // Pausa el tiempo
        isPaused = true;
        Cursor.visible = true; // Muestra el cursor
        Cursor.lockState = CursorLockMode.None; // Desbloquea el cursor
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Desactiva el menú de pausa
        Time.timeScale = 1f; // Reanuda el tiempo
        isPaused = false;
        Cursor.visible = false; // Oculta el cursor
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor
    }

    public void RestartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit(); // Salir del juego
    }

    public void InitEscene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
