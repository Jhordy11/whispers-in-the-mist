using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public Transform target; // El objeto que queremos seguir con la mirada
    public float rotationSpeed = 5f; // Velocidad de rotación

    void Update()
    {
        if (target != null)
        {
            // Calcula la dirección hacia el objetivo
            Vector3 direction = target.position - transform.position;
            // Calcula la rotación necesaria para mirar hacia el objetivo
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            // Suaviza la rotación hacia el objetivo
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
