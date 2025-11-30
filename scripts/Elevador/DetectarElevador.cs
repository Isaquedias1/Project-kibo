using Unity.VisualScripting;
using UnityEngine;

public class DetectarElevador : MonoBehaviour
{
    FuncElevador elevador;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        elevador = GetComponent<FuncElevador>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.SetParent(transform);
    }
    private void OnCollisionExit(Collision collision)
    {
        collision.transform.SetParent(null);
    }
    private void OnTriggerEnter(Collider other)
    {
        elevador.canMove = true;
    }
}
