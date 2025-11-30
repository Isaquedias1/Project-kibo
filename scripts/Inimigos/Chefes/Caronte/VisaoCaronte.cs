using UnityEngine;

public class VisaoCaronte : MonoBehaviour
{
    [Header("Referências")]
    public Transform player;
    public LayerMask obstacles;   

    [Header("Configurações de Visão")]
    public float viewDistance = 10f;
    public float viewAngle = 45f;   
    public bool playerDetected;

    void Update()
    {
        DetectPlayer();
    }

    void DetectPlayer()
    {
        playerDetected = false;

        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        if (Vector3.Angle(transform.forward, directionToPlayer) < viewAngle)
        {
            if (Vector3.Distance(transform.position, player.position) < viewDistance)
            {
                if (!Physics.Raycast(transform.position, directionToPlayer,
                    Vector3.Distance(transform.position, player.position), obstacles))
                {
                    playerDetected = true;
                }
            }
        }

        if (playerDetected)
        {
            Debug.Log("Jogador foi visto! GAME OVER");
            Vidas viado = player.GetComponent<Vidas>();
            viado.TomarDano(50);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 leftLimit = Quaternion.Euler(0, -viewAngle, 0) * transform.forward;
        Vector3 rightLimit = Quaternion.Euler(0, viewAngle, 0) * transform.forward;

        Gizmos.DrawRay(transform.position, leftLimit * viewDistance);
        Gizmos.DrawRay(transform.position, rightLimit * viewDistance);
    }
}
