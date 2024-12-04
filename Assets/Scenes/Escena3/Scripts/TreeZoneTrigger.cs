using System.Collections.Generic;
using UnityEngine;

public class TreeZoneTrigger : MonoBehaviour
{
    public TreeZoneController treeZoneController; // Referencia al TreeZoneController
    public List<int> zonesToActivate; // Lista de índices de zonas a activar
    public List<int> zonesToDeactivate; // Lista de índices de zonas a desactivar

    private void OnTriggerEnter(Collider other)
    {
        treeZoneController.ActivateZones(zonesToActivate);
        treeZoneController.DeactivateZones(zonesToDeactivate);
    }
}
