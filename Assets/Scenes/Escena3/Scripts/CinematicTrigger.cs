using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicTrigger : MonoBehaviour
{
    public CinematicManager cinematicManager;
    public int cinematicIndex;

    private void OnTriggerEnter(Collider other)
    {
        cinematicManager.PlayCinematic(cinematicIndex);
        gameObject.SetActive(false);
    }
}
