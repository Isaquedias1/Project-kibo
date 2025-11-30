using UnityEngine;

public class AtaqueCalda : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Animator animator; // opcional caso faça animações
    public Collider hitboxLeft;
    public Collider hitboxRight;

    [Header("Settings")]
    public float attackWarningTime = 1f;
    public float attackCooldown = 2f;
    public int damage = 20;

    private bool attacking = false;
    private float nextAttackTime = 0f;

    void Update()
    {
        if (Time.time >= nextAttackTime && !attacking)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    System.Collections.IEnumerator AttackRoutine()
    {
        attacking = true;

        // decide aleatoriamente
        bool fromLeft = Random.value > 0.5f;

        // ativa animação de aviso
        if (animator != null)
            animator.SetTrigger(fromLeft ? "WarningLeft" : "WarningRight");

        // tempo de aviso antes do golpe real
        yield return new WaitForSeconds(attackWarningTime);

        // ativa hitbox correta
        Collider chosenHitbox = fromLeft ? hitboxLeft : hitboxRight;
        chosenHitbox.enabled = true;

        // animação do ataque
        if (animator != null)
            animator.SetTrigger(fromLeft ? "AttackLeft" : "AttackRight");

        // espera o frame do acerto (0.2s exemplo)
        yield return new WaitForSeconds(0.2f);

        chosenHitbox.enabled = false;

        // cooldown
        nextAttackTime = Time.time + attackCooldown;
        attacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // checa qual hitbox colidiu
        if (other.CompareTag("Player"))
        {
            Movimento dodge = other.GetComponent<Movimento>();

            if (dodge == null) return;

            // Se o jogador NÃO desviou → toma dano
            if (!dodge.isJumping && !dodge.isCrouching)
            {
                Debug.Log("Jogador tomou dano!");
                // aqui você coloca a função de dano do jogador
                // playerHealth.TakeDamage(damage);
            }
            else
            {
                Debug.Log("Jogador desviou!");
            }
        }
    }
}
