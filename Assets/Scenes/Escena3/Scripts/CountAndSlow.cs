using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class CountdownWithTimeSlow : MonoBehaviour
{
    public TextMeshProUGUI countdownText; // Referencia al TextMeshPro
    public Image leftArrow; // Referencia a la flecha izquierda como Image
    public Image rightArrow; // Referencia a la flecha derecha como Image
    public float countdownDuration = 5f; // Duración del conteo regresivo
    public float slowMotionFactor = 0.5f; // Factor de ralentización del tiempo

    private bool isCountingDown = false;
    private bool isRight = false;
    private bool isGood = false;
    private bool endDecition = false;

    public CinematicManager cinematicManager;
    void Update()
    {
        // Ralentizar el tiempo si el Canvas está activo
        if (isCountingDown)
        {
            Time.timeScale = slowMotionFactor;
        }

        // Cambiar el color de las flechas según la rotación del canvas
        ChangeArrowColors();

        // Iniciar el conteo regresivo solo una vez
        if (!isCountingDown) // Cambia esto a tu método de activación
        {
            StartCoroutine(Countdown());
        }

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            countdownDuration = 0f;
        }

        if(endDecition){
            gameObject.SetActive(false);
        }
    }

    private IEnumerator Countdown()
    {
        isCountingDown = true;
        float timer = countdownDuration;

        while (timer > 0)
        {
            // Actualiza el texto con el tiempo restante
            countdownText.text = Mathf.Ceil(timer).ToString();
            timer -= Time.deltaTime; // Disminuir el tiempo

            // Si el tiempo ha sido forzado a cero, terminar el bucle inmediatamente
            if (countdownDuration <= 0f)
            {
                break;
            }

            yield return null; // Esperar el siguiente cuadro
        }

        endDecition = true; // Finalizar la cuenta regresiva
        countdownText.text = "0";
    }

    private void ChangeArrowColors()
    {
        // Obtener la rotación del canvas en el eje Y
        float rotationY = transform.rotation.eulerAngles.y;
        // Cambiar los colores de las flechas según la rotación
        if (rotationY >= 0f && rotationY <= 55f)
        {
            leftArrow.color = Color.red; // Cambia a rojo
            rightArrow.color = Color.white; // Restablece a blanco
            isRight = false;
            isGood = false;

        }
        else if (rotationY <= 330f && rotationY >= 265f)
        {
            rightArrow.color = Color.red; // Cambia a rojo
            leftArrow.color = Color.white; // Restablece a blanco
            isRight = true;
            isGood = false;

        }
        else
        {
            // Restablece ambos a blanco si no se cumplen las condiciones
            leftArrow.color = Color.white;
            rightArrow.color = Color.white;
            isGood = true;
        }
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
        if(isGood){
            cinematicManager.PlayCinematic(1);
        }
        else if(isRight){
            cinematicManager.PlayCinematic(3);
        }
        else{
            cinematicManager.PlayCinematic(4);
        }

    }


}
