using UnityEngine;

public class Interacao : MonoBehaviour
{
    public string Mensagem = "Aperte E para interagir";

    void OnDrawGizmos()
    {
        // Gizmo opcional para ver o alcance da interação
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 2f);
    }
}
