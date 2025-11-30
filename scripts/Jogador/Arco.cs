using System.Collections;
using UnityEngine;

public class Arco : MonoBehaviour
{
    [Header("References")]
    public Transform pontodeDisparo;
    public GameObject flechaPrefab;
    public GameObject Lira;
    public LineRenderer lineRenderer;
    public Animator anim;

    [Header("Settings")]
    public float forcaMaxima = 30f;  
    public float Carregamento = 15f;
    public int PontoTrajeto = 30;
    public float TempoDestruicaoFlecha = 10f;

    private float ForcaAtual = 0f;
    private bool isCharging = false;


    private void Update()
    {
        // INICIA O CARREGAMENTO
        if (Input.GetMouseButtonDown(1))
        {
            isCharging = true;
            ForcaAtual = 0f;
            lineRenderer.enabled = true;

            anim.SetBool("Ataque", true);
            StartCoroutine(AtivarLiraAposTempo(1f));
            /*if (anim != null)
            {

            }*/
        }

        // SEGURANDO PARA CARREGAR
        if (isCharging && Input.GetMouseButton(1))
        {
            ForcaAtual += Carregamento * Time.deltaTime;
            ForcaAtual = Mathf.Clamp(ForcaAtual, 0, forcaMaxima);

            DesenharTrajeto();
            CorTrajeto();
        }

        // SOLTOU O BOTÃO → DISPARA A FLECHA
        if (Input.GetMouseButtonUp(1))
        {
            Atirar();
            lineRenderer.enabled = false;
            anim.SetBool("Ataque", false);
            Lira.SetActive(false);

        }
    }

    private void Atirar()
    {
        GameObject flecha = Instantiate(flechaPrefab, pontodeDisparo.position, pontodeDisparo.rotation);

        Rigidbody rb = flecha.GetComponent<Rigidbody>();
        rb.linearVelocity = pontodeDisparo.forward * ForcaAtual;

        isCharging = false;
        ForcaAtual = 0f;
        Destroy(flecha, TempoDestruicaoFlecha);
        
    }

    private void DesenharTrajeto()
    {
        Vector3[] pontos = new Vector3[PontoTrajeto];

        Vector3 startPosition = pontodeDisparo.position;
        Vector3 startVelocity = pontodeDisparo.forward * ForcaAtual;

        for (int i = 0; i < PontoTrajeto; i++)
        {
            float t = i * 0.1f; // Intervalo entre os pontos da trajetória

            // Fórmula: posição = posição inicial + velocidade inicial * t + 1/2 * gravidade * t²
            Vector3 ponto = startPosition + startVelocity * t + 0.5f * Physics.gravity * (t * t);

            pontos[i] = ponto;
        }

        lineRenderer.positionCount = PontoTrajeto;
        lineRenderer.SetPositions(pontos);
    }
    private void CorTrajeto()
    {
        if (!lineRenderer) return;

        // Normaliza a força (0 → 1)
        float t = ForcaAtual / forcaMaxima;

        // Define as cores
        Color fraco = Color.green;
        Color medio = Color.yellow;
        Color forte = Color.red;

        // Interpola: verde → amarelo → vermelho
        Color CorFinal;

        if (t < 0.5f)
            CorFinal = Color.Lerp(fraco, medio, t * 2f);        // 0% a 50%
        else
            CorFinal = Color.Lerp(medio, forte, (t - 0.5f) * 2f); // 50% a 100%

        // Aplica a cor no Line Renderer
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] {
            new GradientColorKey(CorFinal, 0f),
            new GradientColorKey(new Color(CorFinal.r, CorFinal.g, CorFinal.b, 0f), 1f)
            },
            new GradientAlphaKey[] {
            new GradientAlphaKey(1f, 0f),
            new GradientAlphaKey(0f, 1f)
            }
        );

        lineRenderer.colorGradient = gradient;
    }

    IEnumerator AtivarLiraAposTempo(float tempo)
    {
        yield return new WaitForSeconds(tempo);
        Lira.SetActive(true);
    }

}
