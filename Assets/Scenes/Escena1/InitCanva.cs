using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InitCanva : MonoBehaviour
{
    public Image fadePanel;
    public float fadeDuration = 1f;
    public int nextSceneIndex; 
    public AudioSource audioSource;
    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void StartGame()
    {
        StartCoroutine(FadeAndLoadScene());
    }

    private IEnumerator FadeAndLoadScene()
    {
        // Asegúrate de que el panel esté visible
        fadePanel.gameObject.SetActive(true);

        // Gradualmente aumenta el alfa del color para oscurecer la pantalla
        Color color = fadePanel.color;
        float fadeAmount = 0f;
        float initialVolume = audioSource.volume; // Obtiene el volumen inicial

        while (fadeAmount < 1f)
        {
            fadeAmount += Time.deltaTime / fadeDuration;
            color.a = fadeAmount;
            fadePanel.color = color;

            // Reduce el volumen del audioSource gradualmente
            audioSource.volume = Mathf.Lerp(initialVolume, 0f, fadeAmount);

            yield return null;
        }

        // Cargar la siguiente escena cuando el panel esté completamente oscuro
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void Exit() {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif

    }
}
