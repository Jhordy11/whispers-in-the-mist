using System;
using UnityEngine;
using UnityEngine.UI;

public class PrometeoControllerCar : MonoBehaviour
{
    [Range(20, 190)]
    public int maxSpeed = 90;
    [Range(1, 10)]
    public int accelerationMultiplier = 2;
    [Range(10, 45)]
    public int maxSteeringAngle = 27;
    [Range(0.1f, 1f)]
    public float steeringSpeed = 0.5f;
    [Range(100, 600)]
    public int brakeForce = 350;
    [Space(10)]
    public Vector3 bodyMassCenter;

    public GameObject frontLeftMesh;
    public WheelCollider frontLeftCollider;
    public GameObject frontRightMesh;
    public WheelCollider frontRightCollider;
    public GameObject rearLeftMesh;
    public WheelCollider rearLeftCollider;
    public GameObject rearRightMesh;
    public WheelCollider rearRightCollider;

    [HideInInspector]
    public float carSpeed;

    // Nueva variable para definir la velocidad inicial
    public float initialSpeed = 60f; // Ajusta este valor a la velocidad inicial deseada en km/h

    private Rigidbody carRigidbody;
    private float steeringAxis;
    private float throttleAxis;

    void Start()
    {
        carRigidbody = gameObject.GetComponent<Rigidbody>();
        carRigidbody.centerOfMass = bodyMassCenter;

        // Convertir la velocidad inicial de km/h a m/s y aplicarla al Rigidbody
        float initialVelocity = initialSpeed / 3.6f; // km/h a m/s
        carRigidbody.velocity = transform.forward * initialVelocity;

        // Iniciar con un throttle alto para una velocidad inicial alta
        throttleAxis = 1f; // Valor máximo desde el inicio para simular una velocidad alta.

        // Aplicar torque inicial a las ruedas para comenzar con alta velocidad
        ApplyInitialTorque();
    }

    void Update()
    {
        // Aumenta automáticamente la velocidad con el tiempo hasta el valor máximo de `maxSpeed`.
        AutoAccelerate();

        // Control de la dirección (izquierda y derecha)
        if (Input.GetKey(KeyCode.A))
        {
            TurnLeft();
        }
        if (Input.GetKey(KeyCode.D))
        {
            TurnRight();
        }
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && steeringAxis != 0f)
        {
            ResetSteeringAngle();
        }

        // Animación de los mesh de las ruedas
        AnimateWheelMeshes();
    }

    // Función que acelera automáticamente el coche hasta alcanzar la velocidad máxima
    void AutoAccelerate()
    {
        throttleAxis += Time.deltaTime * (accelerationMultiplier / 2f);
        if (throttleAxis > 1f) throttleAxis = 1f;

        // Si no ha alcanzado la velocidad máxima, aplicar torque en las ruedas
        if (Mathf.RoundToInt(carSpeed) < maxSpeed)
        {
            ApplyTorque();
        }
        else
        {
            // Detener el torque cuando se alcanza la velocidad máxima
            StopTorque();
        }

        // Actualizar la velocidad del coche
        carSpeed = carRigidbody.velocity.magnitude * 3.6f; // Convertir a km/h
    }

    // Aplicar torque inicial en las ruedas para empezar con velocidad alta
    void ApplyInitialTorque()
    {
        frontLeftCollider.motorTorque = (accelerationMultiplier * 50f) * throttleAxis;
        frontRightCollider.motorTorque = (accelerationMultiplier * 50f) * throttleAxis;
        rearLeftCollider.motorTorque = (accelerationMultiplier * 50f) * throttleAxis;
        rearRightCollider.motorTorque = (accelerationMultiplier * 50f) * throttleAxis;
    }

    // Aplicar torque en las ruedas para continuar acelerando
    void ApplyTorque()
    {
        frontLeftCollider.motorTorque = (accelerationMultiplier * 50f) * throttleAxis;
        frontRightCollider.motorTorque = (accelerationMultiplier * 50f) * throttleAxis;
        rearLeftCollider.motorTorque = (accelerationMultiplier * 50f) * throttleAxis;
        rearRightCollider.motorTorque = (accelerationMultiplier * 50f) * throttleAxis;
    }

    // Detener el torque cuando se alcanza la velocidad máxima
    void StopTorque()
    {
        frontLeftCollider.motorTorque = 0;
        frontRightCollider.motorTorque = 0;
        rearLeftCollider.motorTorque = 0;
        rearRightCollider.motorTorque = 0;
    }

    public void TurnLeft()
    {
        steeringAxis = Mathf.Clamp(steeringAxis - (Time.deltaTime * 10f * steeringSpeed), -1f, 1f);
        ApplySteering();
    }

    public void TurnRight()
    {
        steeringAxis = Mathf.Clamp(steeringAxis + (Time.deltaTime * 10f * steeringSpeed), -1f, 1f);
        ApplySteering();
    }

    public void ResetSteeringAngle()
    {
        if (steeringAxis < 0f)
        {
            steeringAxis += Time.deltaTime * 10f * steeringSpeed;
        }
        else if (steeringAxis > 0f)
        {
            steeringAxis -= Time.deltaTime * 10f * steeringSpeed;
        }
        if (Mathf.Abs(steeringAxis) < 0.1f)
        {
            steeringAxis = 0f;
        }
        ApplySteering();
    }

    void ApplySteering()
    {
        var steeringAngle = steeringAxis * maxSteeringAngle;
        frontLeftCollider.steerAngle = Mathf.Lerp(frontLeftCollider.steerAngle, steeringAngle, steeringSpeed);
        frontRightCollider.steerAngle = Mathf.Lerp(frontRightCollider.steerAngle, steeringAngle, steeringSpeed);
    }

    void AnimateWheelMeshes()
    {
        Quaternion FLWRotation;
        Vector3 FLWPosition;
        frontLeftCollider.GetWorldPose(out FLWPosition, out FLWRotation);
        frontLeftMesh.transform.position = FLWPosition;
        frontLeftMesh.transform.rotation = FLWRotation;

        Quaternion FRWRotation;
        Vector3 FRWPosition;
        frontRightCollider.GetWorldPose(out FRWPosition, out FRWRotation);
        frontRightMesh.transform.position = FRWPosition;
        frontRightMesh.transform.rotation = FRWRotation;

        Quaternion RLWRotation;
        Vector3 RLWPosition;
        rearLeftCollider.GetWorldPose(out RLWPosition, out RLWRotation);
        rearLeftMesh.transform.position = RLWPosition;
        rearLeftMesh.transform.rotation = RLWRotation;

        Quaternion RRWRotation;
        Vector3 RRWPosition;
        rearRightCollider.GetWorldPose(out RRWPosition, out RRWRotation);
        rearRightMesh.transform.position = RRWPosition;
        rearRightMesh.transform.rotation = RRWRotation;
    }
}
