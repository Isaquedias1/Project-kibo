using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vidas : MonoBehaviour
{
    public Animator animKibo;
    public Animator animInimigo;
    public int vidaTotal = 30;
    public int vidaAtual;

    [Header("Invencibilidade do Player")]
    public float invencivelDuracao = 1.2f;
    public float blinkSpeed = 0.1f;
    private bool invencivel = false;

    // Para piscar
    private SkinnedMeshRenderer[] meshes;

    void Start()
    {
        vidaAtual = vidaTotal;

        meshes = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    void Update()
    {
        if (invencivel)
        {
            invencivelDuracao -= Time.deltaTime;

            if (invencivelDuracao <= 0)
            {
                invencivel = false;
                invencivelDuracao = 1.2f; 
                StopAllCoroutines();
                SetAllMeshesVisible(true);
            }
        }
    }

    public void TomarDano(int amount)
    {
        if (gameObject.CompareTag("Player") && invencivel)
            return;

        vidaAtual -= amount;

        if (gameObject.CompareTag("Player"))
        {
            invencivel = true;
            StartCoroutine(PiscarInvencivel()); 
        }

        if (vidaAtual <= 0)
        {
            if (gameObject.CompareTag("Umbra"))
            {
                animInimigo.SetBool("Morte", true);
                GetComponent<AtqInim>().enabled = false;
                GetComponent<MovInim>().enabled = false;
                StartCoroutine(MorteInimigo(4.3f));
            }
            if (gameObject.CompareTag("Player"))
            {
                animKibo.SetBool("Morte", true);
                Debug.Log("Jogador Morreu");

                GetComponent<Movimento>().enabled = false;
                GetComponent<AtkFisico>().enabled = false;

                StopAllCoroutines();
                SetAllMeshesVisible(true);
            }
            if (gameObject.CompareTag("Venti"))
            {
                animInimigo.SetBool("Morte", true);
                GetComponent<VentiTornadp>().enabled = false;
                StartCoroutine(MorteInimigo(3.3f));
            }
            if (gameObject.CompareTag("erebos"))
            {
                GetComponent<ErebosManager>().enabled = false;
                StartCoroutine(MorteInimigo(3.3f));
                SceneManager.LoadScene("menu");
            }
            if (gameObject.CompareTag("FalsoIctio"))
            {
                GetComponent<FalsoManager>().enabled = false;
                StartCoroutine(MorteInimigo(3.3f));
            }
            if (gameObject.CompareTag("Gollem"))
            {
                Morrer();
                SceneManager.LoadScene("cena06golem");
                    
            }
        }
    }

    IEnumerator PiscarInvencivel()
    {
        while (invencivel)
        {
            SetAllMeshesVisible(false);
            yield return new WaitForSeconds(blinkSpeed);

            SetAllMeshesVisible(true);
            yield return new WaitForSeconds(blinkSpeed);
        }
    }

    void SetAllMeshesVisible(bool visible)
    {
        foreach (var m in meshes)
        {
            if (m != null)
                m.enabled = visible;
        }
    }

    private void Morrer()
    {
        Debug.Log(gameObject.name + " morreu.");
        Destroy(gameObject);
    }

    IEnumerator MorteInimigo(float tempo)
    {
        yield return new WaitForSeconds(tempo);
        Morrer();
    }
}
