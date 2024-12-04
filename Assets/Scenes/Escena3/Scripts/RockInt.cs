using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RockInt : MonoBehaviour
{
    public Transform player; // Transform del jugador.
    public float interactionRange = 2.0f; // Rango de interacción para interactuar.
    public LayerMask rockLayer; // Capa para identificar rocas.
    public LayerMask slotLayer; // Capa para identificar ranuras.

    private GameObject currentRock; // Roca que el jugador tiene actualmente.
    private GameObject nearbyRock; // Roca más cercana al jugador.
    public GameObject canvaColocar; // Roca más cercana al jugador.
    public GameObject canvaRecoger; // Roca más cercana al jugador.
    private Slot currentSlot; // Ranura en la que el jugador va a colocar la roca.
    public Animator playerAnimator;

    // void Start()
    // {
    //     InputSystem.EnableDevice(Keyboard.current);
    // }

    void Update()
    {
        // Busca rocas cercanas si el jugador no está cargando ninguna roca.
        Collider[] nearbyRocks = Physics.OverlapSphere(player.position, interactionRange, rockLayer);
        if (nearbyRocks.Length > 0)
        {
            nearbyRock = nearbyRocks[0].gameObject;
            canvaRecoger.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Vector3 directionToRock = (nearbyRock.transform.position - player.position).normalized;
                player.forward = new Vector3(directionToRock.x, 0, directionToRock.z);
                playerAnimator.SetTrigger("Recoger");
            }
        }
        else
        {
            canvaRecoger.SetActive(false);
            nearbyRock = null;
        }
        if (currentRock)
        {
            Collider[] nearbySlots = Physics.OverlapSphere(player.position, interactionRange, slotLayer);
            if (nearbySlots.Length > 0)
            {
                currentSlot = nearbySlots[0].GetComponent<Slot>();

                if (currentSlot != null)
                {
                    canvaColocar.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        playerAnimator.SetTrigger("Colocar");
                    }
                }else{
                    canvaColocar.SetActive(false);   
                }
            }
            else
            {
                canvaColocar.SetActive(false);
                currentSlot = null;
            }
        }else{
            canvaColocar.SetActive(false);
        }


    }

     public void PickupRock(GameObject rock)
    {
        // Si el jugador ya tiene una roca, la deja en la posición de la nueva roca.
        if (currentRock != null)
        {
            currentRock.transform.SetPositionAndRotation(rock.transform.position, rock.transform.rotation);
            currentRock.SetActive(true); // Hacer visible la roca anterior en la nueva posición.
        }

        // Recoge la nueva roca y ocúltala en la escena.
        currentRock = rock;
        currentRock.SetActive(false);
    }

    public void PlaceRockInSlot(Slot slot)
    {  
        
        if (slot != null &&slot.IsCorrectRock(currentRock))
        {
            slot.PlaceRock(currentRock);
            currentRock = null;
            ShowMessage("Roca colocada en la ranura");
        }
        else
        {
            ShowMessage("Roca incorrecta, busca otra");
        }
    }

    public void ShowMessage(string message)
    {
        Debug.Log(message + interactionRange);
        // Puedes implementar un sistema de UI aquí para mostrar el mensaje en pantalla.
    }
    
    public Slot GetSlot(){
        return currentSlot;
    }
    public GameObject GetNearbyRock(){
        return nearbyRock;
    }
}
