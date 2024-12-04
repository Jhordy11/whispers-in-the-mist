using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowCar : MonoBehaviour {

    public Transform carTransform;
    [Range(1, 10)]
    public float followSpeed = 2;
    [Range(1, 10)]
    public float lookSpeed = 5;
    public Vector3 offset = new Vector3(0, 5, -10);  // Offset para mantener la cámara detrás del coche

    void FixedUpdate()
    {
        // Look at car (mantén la cámara mirando hacia adelante desde la perspectiva del coche)
        Vector3 _lookDirection = carTransform.position - transform.position;
        Quaternion _rot = Quaternion.LookRotation(_lookDirection, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, _rot, lookSpeed * Time.deltaTime);

        // Move to car (ajusta la posición detrás del coche según su rotación)
        Vector3 _targetPos = carTransform.position + carTransform.TransformDirection(offset);
        transform.position = Vector3.Lerp(transform.position, _targetPos, followSpeed * Time.deltaTime);
    }

}
