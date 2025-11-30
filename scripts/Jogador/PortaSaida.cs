using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortaSaida : MonoBehaviour
{
    public JogadorInteracao player;
    public string cenaDestino = "NomeDaCena";
    public TextMeshProUGUI mensagemBloqueio;

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

            if (player == null)
            {
                Debug.LogError("PlayerInteraction NÃO está referenciado!");
                return;
            }

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

        if (player.interagiuComAEstatua)
        {
            Debug.Log("Pode sair! Trocando cena...");
            SceneManager.LoadScene(cenaDestino);
        }
        else
        {
            Debug.Log("Bloqueado: falta interagir com a estátua.");

            if (mensagemBloqueio != null)
            {
                mensagemBloqueio.gameObject.SetActive(true);
                mensagemBloqueio.text = "Você precisa interagir com a estátua primeiro!";
            }
        }
    }
}
