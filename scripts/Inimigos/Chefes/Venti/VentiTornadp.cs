using UnityEngine;
using UnityEngine.AI;

public class VentiTornadp : MonoBehaviour
{
    [Header("Referências")]
    public Transform player;
    private NavMeshAgent agent;
    Animator anim;

    [Header("Controle do Boss")]
    public BossAttackCycle boss;

    [Header("Configurações do Tornado")]
    public float chaseSpeed = 8f;
    public float chaseDuration = 5f;
    public int tornadoDamage = 10;

    [Header("Cooldown")]
    public float cooldownDuration = 4f;
    public float cooldownSpeed = 0f;

    private float stateTimer;
    private bool isChasing = true;

    [Header("Detecção de Dano")]
    public float hitRange = 2f;
    public LayerMask playerMask;

    public GameObject windEffects;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        stateTimer = chaseDuration;
        agent.speed = chaseSpeed;
        anim = GetComponentInChildren<Animator>();
        this.enabled = false;
    }

    private void Update()
    {
        if (isChasing)
            ChasePlayer();
        else
            CooldownState();
    }

    void ChasePlayer()
    {
        stateTimer -= Time.deltaTime;
        anim.SetBool("Bravao", true);

        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);
        SetWind(true);

        Collider[] hit = Physics.OverlapSphere(transform.position, hitRange, playerMask);
        foreach (Collider c in hit)
        {
            Vidas Vida = c.GetComponent<Vidas>();
            if (Vida != null)
                Vida.TomarDano(tornadoDamage);
        }

        if (stateTimer <= 0)
        {
            isChasing = false;
            stateTimer = cooldownDuration;
            agent.speed = cooldownSpeed;

            if (boss != null)
                boss.finishedCombo = true;
        }
    }

    void CooldownState()
    {
        stateTimer -= Time.deltaTime;
        anim.SetBool("Bravao", false);
        SetWind(false);
        agent.SetDestination(transform.position);

        if (stateTimer <= 0)
        {
            isChasing = true;
            stateTimer = chaseDuration;
            agent.speed = chaseSpeed;
        }
    }

    void SetWind(bool active)
    {
        windEffects.SetActive(active);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hitRange);
    }
}
