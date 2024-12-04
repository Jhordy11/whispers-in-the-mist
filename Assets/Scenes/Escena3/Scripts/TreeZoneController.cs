using System.Collections.Generic;
using UnityEngine;

public class TreeZoneController : MonoBehaviour
{
    public GameObject[] treeZones; // Array de zonas de árboles

    private void Start()
    {
        // Opcional: Desactivar todas las zonas al inicio o configurar la primera zona activa si es necesario
        foreach (GameObject zone in treeZones)
        {
            zone.SetActive(false);
        }
    }

    // Método para activar múltiples zonas a la vez
    public void ActivateZones(List<int> zoneIndices)
    {
        foreach (int index in zoneIndices)
        {
            if (index >= 0 && index < treeZones.Length)
            {
                treeZones[index].SetActive(true);
            }
        }
    }

    // Método para desactivar múltiples zonas a la vez
    public void DeactivateZones(List<int> zoneIndices)
    {
        foreach (int index in zoneIndices)
        {
            if (index >= 0 && index < treeZones.Length)
            {
                treeZones[index].SetActive(false);
            }
        }
    }
}
