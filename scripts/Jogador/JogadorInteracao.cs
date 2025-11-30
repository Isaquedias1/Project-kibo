using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JogadorInteracao : MonoBehaviour
{public float distanciaInteracao = 2f;
    public TextMeshProUGUI mensagemUI;
    public GameObject imagemUI;
    public GameObject imagemUI2;

    public bool interagiuComAEstatua = false;

    private Interacao objetoAtual;

    void Start()
    {
        mensagemUI.gameObject.SetActive(false);
        imagemUI.SetActive(false);
        imagemUI2.SetActive(false);
    }

    void Update()
    {
        DetectarObjeto();
        VerificarInteracao();
        FecharImagem();
    }

    void DetectarObjeto()
    {
        if (imagemUI.activeSelf || imagemUI2.activeSelf)
        {
            mensagemUI.gameObject.SetActive(false);
            return;
        }

        objetoAtual = null;

        Collider[] colls = Physics.OverlapSphere(transform.position, distanciaInteracao);

        foreach (Collider c in colls)
        {
            Interacao inter = c.GetComponent<Interacao>();
            if (inter != null)
            {
                objetoAtual = inter;
                mensagemUI.text = inter.Mensagem;
                mensagemUI.gameObject.SetActive(true);
                return;
            }
        }

        mensagemUI.gameObject.SetActive(false);
    }

    void VerificarInteracao()
    {
        if (objetoAtual != null && Input.GetKeyDown(KeyCode.E))
        {
           
            imagemUI.SetActive(true);
            Debug.Log("Imagem apareceu");

            if (objetoAtual.CompareTag("EstatuaNessa"))
            {
                interagiuComAEstatua = true;
                Debug.Log("Interagiu com a estátua!");
                imagemUI2.SetActive(true);
            }

        }
    }

    void FecharImagem()
    {
        if (imagemUI.activeSelf && Input.anyKeyDown || imagemUI2.activeSelf && Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.E)) return;

            imagemUI.SetActive(false);
            imagemUI2.SetActive(false);
            Debug.Log("Imagem sumiu");
        }
    }
}
