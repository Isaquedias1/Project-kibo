using UnityEngine;

public class AtqInim : MonoBehaviour
{
    public float attackRange = 1.5f;
    public int atqDano = 5;
    public float Cooldown = 2f;
    public LayerMask playerLayer;


    private float ProxAtaque = 0f;

    void Update()
    {
        if (Time.time >= ProxAtaque)
        {
            Collider[] hitPlayers = Physics.OverlapSphere(transform.position, attackRange, playerLayer);

            foreach (Collider player in hitPlayers)
            {
                Vidas playerHealth = player.GetComponent<Vidas>();
                if (playerHealth != null)
                {
                    if (playerHealth.vidaAtual <= 0) return;
                    playerHealth.TomarDano(atqDano);
                    ProxAtaque = Time.time + Cooldown;
                    Debug.Log("Inimigo Bateu");
                }
                
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}