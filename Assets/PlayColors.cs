using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PlayColors : MonoBehaviour
{
    public Color[] coloresDisponibles; // 6 colores
    public GameObject[] plataformasPerro; // 3 plataformas para el perro
    public GameObject[] plataformasPlayer; // 3 plataformas para el player
    public GameObject perro; // El personaje perro
    public GameObject player; // El personaje player

    private Vector3 posicionInicialPerro; // Nueva: Posición inicial del perro
    private Vector3 posicionInicialPlayer; // Nueva: Posición inicial del player

    private List<int> secuenciaPerroIndices = new List<int>();
    private List<int> secuenciaPlayerIndices = new List<int>();
    private List<int> movimientosPerro = new List<int>();
    private List<int> movimientosPlayer = new List<int>();
    private int indiceSecuencia = 0;
    private int indiceDeMovimineto = 0;
    private ThirdPersonController thirdPersonController;
    private bool jugadorActivo = false;
    private bool mostrandoSecuencia = false;
    private bool turnoGastadoPerro = false;
    private bool turnoGastadoPlayer = false;
    private Animator animatorPerro;
    private Animator animatorPlayer;
    private bool finalizoAniPerro = false;
    private bool finalizoAniPlayer = false;
    private int perdidas=0;
    public CinematicManager cinematicManager;
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        thirdPersonController = player.GetComponent<ThirdPersonController>();
        thirdPersonController.SetInAnimations(true);

        // Guardar posiciones iniciales
        posicionInicialPerro = perro.transform.position;
        posicionInicialPlayer = player.transform.position;

        animatorPerro = perro.GetComponent<Animator>();
        animatorPlayer = player.GetComponent<Animator>();

        OcultarColores();
        GenerarNuevaSecuencia();
    }

    void Update()
    {
        if(indiceDeMovimineto == 5){
            cinematicManager.PlayCinematic(7);
        } else if(perdidas == 2){
            cinematicManager.PlayCinematic(6);
        }

        if (jugadorActivo && !mostrandoSecuencia)
        {
            if (!turnoGastadoPerro)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1)) MoverPersonaje(perro, plataformasPerro[0], 0, movimientosPerro);
                if (Input.GetKeyDown(KeyCode.Alpha2)) MoverPersonaje(perro, plataformasPerro[1], 1, movimientosPerro);
                if (Input.GetKeyDown(KeyCode.Alpha3)) MoverPersonaje(perro, plataformasPerro[2], 2, movimientosPerro);
            }

            if (!turnoGastadoPlayer)
            {
                if (Input.GetKeyDown(KeyCode.Q)) MoverPersonaje(player, plataformasPlayer[0], 0, movimientosPlayer);
                if (Input.GetKeyDown(KeyCode.W)) MoverPersonaje(player, plataformasPlayer[1], 1, movimientosPlayer);
                if (Input.GetKeyDown(KeyCode.E)) MoverPersonaje(player, plataformasPlayer[2], 2, movimientosPlayer);
            }

            if (turnoGastadoPerro && turnoGastadoPlayer && finalizoAniPerro && finalizoAniPlayer)
            {
                finalizoAniPerro = false;
                finalizoAniPlayer = false;
                indiceDeMovimineto++;
                StartCoroutine(VerificarSecuencia());
                turnoGastadoPerro = false;
                turnoGastadoPlayer = false;
            }
        }
    }

    void GenerarNuevaSecuencia()
    {
        secuenciaPerroIndices.Add(Random.Range(0, plataformasPerro.Length));
        secuenciaPlayerIndices.Add(Random.Range(0, plataformasPlayer.Length));

        MostrarSecuencia();
    }

    void MostrarSecuencia()
    {
        StartCoroutine(MostrarSecuenciaConRetraso());
    }

    IEnumerator MostrarSecuenciaConRetraso()
    {
        mostrandoSecuencia = true;

        for (int i = 0; i < secuenciaPerroIndices.Count; i++)
        {
            plataformasPerro[secuenciaPerroIndices[i]].GetComponent<Renderer>().material.color = coloresDisponibles[i];
            plataformasPlayer[secuenciaPlayerIndices[i]].GetComponent<Renderer>().material.color = coloresDisponibles[i];
            yield return new WaitForSeconds(1f);
            OcultarColores();
            yield return new WaitForSeconds(0.5f);
        }

        jugadorActivo = true;
        mostrandoSecuencia = false;
    }

    void OcultarColores()
    {
        foreach (var plataforma in plataformasPerro)
        {
            plataforma.GetComponent<Renderer>().material.color = Color.gray;
        }
        foreach (var plataforma in plataformasPlayer)
        {
            plataforma.GetComponent<Renderer>().material.color = Color.gray;
        }

        jugadorActivo = true;
        movimientosPerro.Clear();
        movimientosPlayer.Clear();
    }

    void MoverPersonaje(GameObject personaje, GameObject plataformaDestino, int indicePlataforma, List<int> movimientos)
    {
        if (personaje.CompareTag("Player"))
        {
            turnoGastadoPlayer = true;
        }
        else
        {
            turnoGastadoPerro = true;
        }
        StartCoroutine(MoverPersonajeSuave(personaje, plataformaDestino, indicePlataforma, movimientos));
    }

    IEnumerator MoverPersonajeSuave(GameObject personaje, GameObject plataformaDestino, int indicePlataforma, List<int> movimientos)
    {
        Vector3 destino = plataformaDestino.transform.position;

        Collider plataformaCollider = plataformaDestino.GetComponent<Collider>();
        if (plataformaCollider != null)
        {
            destino.y = plataformaCollider.bounds.max.y; // Parte superior de la plataforma
        }

        Vector3 direccion = (destino - personaje.transform.position).normalized;
        Quaternion rotacionFinal = Quaternion.LookRotation(direccion);

        Animator animator = personaje.CompareTag("Player") ? animatorPlayer : animatorPerro;

        // Rotar suavemente hacia la plataforma
        while (Quaternion.Angle(personaje.transform.rotation, rotacionFinal) > 0.1f)
        {
            personaje.transform.rotation = Quaternion.Slerp(personaje.transform.rotation, rotacionFinal, Time.deltaTime * 5);
            yield return null;
        }

        // Moverse suavemente hacia la plataforma
        while (Vector3.Distance(personaje.transform.position, destino) > 0.1f)
        {
            float velocidadMovimiento = personaje.CompareTag("Player") ? 3 : 6; // Perro siempre corre

            // Actualizar el parámetro Speed
            animator.SetFloat("Speed", personaje.CompareTag("Player") ? velocidadMovimiento : 6);

            Vector3 nuevaPosicion = Vector3.MoveTowards(personaje.transform.position, destino, Time.deltaTime * velocidadMovimiento);
            personaje.transform.position = nuevaPosicion;
            yield return null;
        }

        // Ajustar la posición final exactamente en la parte superior
        personaje.transform.position = destino;

        // Detener la animación al llegar al destino
        animator.SetFloat("Speed", 0);

        movimientos.Add(indicePlataforma);

        if (personaje.CompareTag("Player"))
        {
            finalizoAniPlayer = true;
        }
        else
        {
            finalizoAniPerro = true;
        }

    }



    IEnumerator VerificarSecuencia()
    {
        bool secuenciaPerroCorrecta = true;
        bool secuenciaPlayerCorrecta = true;

        for (int i = 0; i < indiceDeMovimineto; i++)
        {
            if (movimientosPerro[i] != secuenciaPerroIndices[i])
            {
                secuenciaPerroCorrecta = false;
                break;
            }
        }

        for (int i = 0; i < indiceDeMovimineto; i++)
        {
            if (movimientosPlayer[i] != secuenciaPlayerIndices[i])
            {
                secuenciaPlayerCorrecta = false;
                break;
            }
        }

        if (secuenciaPerroCorrecta && secuenciaPlayerCorrecta)
        {
            indiceSecuencia++;
            if (indiceDeMovimineto == secuenciaPerroIndices.Count)
            {
                indiceDeMovimineto = 0;
                yield return RegresarAlCentroSuave(perro);
                yield return RegresarAlCentroSuave(player);
                GenerarNuevaSecuencia();
            }
        }
        else
        {
            perdidas++;
            yield return RegresarAlCentroSuave(perro);
            yield return RegresarAlCentroSuave(player);
            ResetJuego();
        }
    }

    IEnumerator RegresarAlCentroSuave(GameObject personaje)
    {
        Vector3 posicionInicial = personaje.CompareTag("Player") ? posicionInicialPlayer : posicionInicialPerro;
        Vector3 direccionInicial = (posicionInicial - personaje.transform.position).normalized;
        Quaternion rotacionInicial = Quaternion.LookRotation(direccionInicial);

        Animator animator = personaje.CompareTag("Player") ? animatorPlayer : animatorPerro;

        // Rotar suavemente hacia el centro
        while (Quaternion.Angle(personaje.transform.rotation, rotacionInicial) > 0.1f)
        {
            personaje.transform.rotation = Quaternion.Slerp(personaje.transform.rotation, rotacionInicial, Time.deltaTime * 5);
            yield return null;
        }

        // Moverse suavemente hacia el centro
        while (Vector3.Distance(personaje.transform.position, posicionInicial) > 0.1f)
        {
            float velocidadMovimiento = personaje.CompareTag("Player") ? 3 : 6; // Perro siempre corre

            // Actualizar el parámetro Speed
            animator.SetFloat("Speed", personaje.CompareTag("Player") ? velocidadMovimiento : 6);

            Vector3 nuevaPosicion = Vector3.MoveTowards(personaje.transform.position, posicionInicial, Time.deltaTime * velocidadMovimiento);
            personaje.transform.position = nuevaPosicion;
            yield return null;
        }

        // Ajustar la posición final exactamente en el centro
        personaje.transform.position = posicionInicial;

        // Detener la animación al llegar al centro
        animator.SetFloat("Speed", 0);

        personaje.transform.rotation = rotacionInicial;
    }

    void ResetJuego()
    {
        indiceDeMovimineto = 0;
        indiceSecuencia = 0;
        secuenciaPerroIndices.Clear();
        secuenciaPlayerIndices.Clear();
        movimientosPerro.Clear();
        movimientosPlayer.Clear();
        jugadorActivo = false;
        turnoGastadoPerro = false;
        turnoGastadoPlayer = false;
        GenerarNuevaSecuencia();
    }
}