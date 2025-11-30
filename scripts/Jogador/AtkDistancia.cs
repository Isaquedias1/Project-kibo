using System.Collections;
using UnityEngine;

public class AtkDistancia : MonoBehaviour
{
    public GameObject Flecha;
    //public GameObject Inimigo;
    public Transform pontoDisparo;
    public float Forca = 10f;  // Defina um valor padrão de força
    public float Carregamento = 1f;  // Defina um valor padrão de tempo de carregamento
    public float TempoDestruicaoFlecha = 5f; // Tempo para destruir a flecha após disparo
    bool carregando = false;
    public float ArremessoUpwardForca;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && !carregando)
        {
            StartCoroutine(CarregarFlecha());
        }
    }

    IEnumerator CarregarFlecha()
    {
        carregando = true;
        Debug.Log("Carregando");
        yield return new WaitForSeconds(Carregamento);

        DispararFlecha();
        carregando = false;
    }

    void DispararFlecha()
    {
        if (Flecha != null && pontoDisparo != null)
        {
            // Instancia a flecha na posição do ponto de disparo
            GameObject Disparo = Instantiate(Flecha, pontoDisparo.position, pontoDisparo.rotation);

            // Obtém o Rigidbody da flecha instanciada
            Rigidbody rb = Disparo.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Aplica força na flecha para ela ser disparada
                Vector3 forceToAdd = pontoDisparo.transform.forward * Forca + transform.up * ArremessoUpwardForca;
                rb.AddForce(forceToAdd, ForceMode.Impulse);
                Debug.Log("Flecha Disparada");

                // Chama a função para destruir a flecha após um tempo
                Destroy(Disparo, TempoDestruicaoFlecha);
            }
            else
            {
                Debug.LogWarning("A flecha não possui Rigidbody.");
            }
        }
        else
        {
            Debug.LogWarning("Flecha ou ponto de disparo não definidos.");
        }
    }
}
