using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.SceneManagement;
public class DesactivarMovimientoSignal : MonoBehaviour
{
    private ThirdPersonController thirdPersonController;
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        thirdPersonController = player.GetComponent<ThirdPersonController>();
        Cursor.visible = false;

    }
    
    public void Desactivate()
    {
        thirdPersonController.SetInAnimations(true);
    }

    public void Activate()
    {
        thirdPersonController.SetInAnimations(false);
    }
    public void RegresarAlIntro()
    {
        SceneManager.LoadScene(0);
    }
}
