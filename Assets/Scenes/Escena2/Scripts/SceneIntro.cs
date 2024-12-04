using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneIntro : MonoBehaviour
{
    private bool gameStarted = false;
    public AudioSource carSound;
    private Canvas canvas;
    public Image fadePanel;
    public float fadeDuration = 1f;
    public Canvas canvasInit;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        carSound.enabled = false;
        if (canvasInit != null)
        {
            Destroy(canvasInit.gameObject);
        }
        Time.timeScale = 0f;
        StartCoroutine(FadeAndLoadScene());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameStarted)
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        gameStarted = true;
        canvas.gameObject.SetActive(false);
        carSound.enabled = true;
        Time.timeScale = 1f;
    }

    private IEnumerator FadeAndLoadScene()
    {
        fadePanel.gameObject.SetActive(true);

        Color color = fadePanel.color;
        float fadeAmount = 1f; // Comienza completamente opaco

        while (fadeAmount > 0f)
        {
            fadeAmount -= Time.unscaledDeltaTime / fadeDuration; // Reducimos en lugar de aumentar
            color.a = fadeAmount;
            fadePanel.color = color;
            yield return null;
        }

        // Asegurarse de que el panel esté completamente transparente al final
        color.a = 0f;
        fadePanel.color = color;

        // Desactivar el panel si es necesario
        fadePanel.gameObject.SetActive(false);
    }
}
