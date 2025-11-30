using UnityEngine;
using static UnityEngine.UI.Image;
public class UIManager : MonoBehaviour
{

    public GameObject tapecaria;
    public GameObject estatua;
    public GameObject interagir;


    public float rayDistance = 2f;
    public Color rayColor = Color.red;

    void Update()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.TransformDirection(Vector3.back);

        if (Physics.Raycast(origin, direction, out RaycastHit hit, rayDistance))
        {
            Debug.Log("Raycast atingiu: " + hit.collider.gameObject.name);
            if (hit.collider.gameObject.CompareTag("tapecaria"))
            {
                mostrarInteragir();
                if (Input.GetKey(KeyCode.E))
                {
                    ocultarInteragir();
                    mostrarTapecaria();
                }
                if (Input.anyKeyDown)
                {
                    ocultarTapecaria();
                }
            }
            if (hit.collider.gameObject.CompareTag("estatua"))
            {
                mostrarInteragir();
                if (Input.GetKey(KeyCode.E))
                {
                    ocultarInteragir();
                    mostrarEstatua();
                }
                if (Input.anyKeyDown)
                {
                    ocultarEstatua();
                }
            }
            if (hit.collider.gameObject.CompareTag("fragmento"))
            {
                mostrarInteragir();
                if (Input.GetKey(KeyCode.E))
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
        Debug.DrawRay(origin, direction * rayDistance, rayColor);
    }

    // Método para mostrar o elemento de UI
    public void mostrarTapecaria()
    {
        if (tapecaria != null)
        {
            tapecaria.SetActive(true);
            Debug.Log("Elemento de UI mostrado.");
        }
    }


    public void ocultarTapecaria()
    {
        if (tapecaria != null)
        {
            tapecaria.SetActive(false);
            Debug.Log("Elemento de UI ocultado.");
        }
    }

    //--------------------------------------------------------------------------------------
    public void mostrarEstatua()
    {
        if (estatua != null)
        {
            estatua.SetActive(true);
            Debug.Log("Elemento de UI mostrado.");
        }
    }


    public void ocultarEstatua()
    {
        if (estatua != null)
        {
            estatua.SetActive(false);
            Debug.Log("Elemento de UI ocultado.");
        }
    }

    //--------------------------------------------------------------------------------------
    public void mostrarInteragir()
    {
        if (interagir != null)
        {
            interagir.SetActive(true);
            Debug.Log("Elemento de UI mostrado.");
        }
    }


    public void ocultarInteragir()
    {
        if (interagir != null)
        {
            interagir.SetActive(false);
            Debug.Log("Elemento de UI ocultado.");
        }
    }
}