using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatarBossTrocaCena : MonoBehaviour
{
    public string cenaDestino = "NomeDaCena";
    public TextMeshProUGUI mensagemBloqueio;
    public GameObject Boss;

    private void Start()
    {
        if (mensagemBloqueio != null)
            mensagemBloqueio.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entrou no trigger: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detectado na saída!");

            TentarTrocarCena();
        }
        else
        {
            Debug.Log("Não é o player.");
        }
    }

    void TentarTrocarCena()
    {
        Debug.Log("Tentando trocar de cena...");
        Vidas vidaBoss = GetComponent<Vidas>();

        if (vidaBoss.vidaAtual <= 0)
        {
            Debug.Log("Pode sair!");
            SceneManager.LoadScene(cenaDestino);
        }
        else
        {
            Debug.Log("Bloqueado: Mate o Boss");

            if (mensagemBloqueio != null)
            {
                mensagemBloqueio.gameObject.SetActive(true);
                mensagemBloqueio.text = "Você precisa matar o boss primeiro!";
            }
        }
    }
}
