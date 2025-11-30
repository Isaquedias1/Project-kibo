using UnityEngine;
using UnityEngine.AI;

public class MovInim : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float movementSpeed = 5f;
    public float speedRun = 9f;
    public float startWaitTime = 4;
    float m_WaitTime;
    public Animator animInimigo;

    //Patroling
    public Vector3 walkPoint;
    public Transform[] wayPoints;
    int m_CurrentWaypointIndex;

    //attack
    /* public float timeBetweenAttacks;
     public bool alreadyattacked
     */

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = movementSpeed;
        agent.SetDestination(wayPoints[m_CurrentWaypointIndex].position);
        m_WaitTime = startWaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) Chase();
        if (playerInSightRange && playerInAttackRange) Attack();
    }

    private void Patroling()
    {

        agent.SetDestination(wayPoints[m_CurrentWaypointIndex].position);
        agent.speed = movementSpeed;
        animInimigo.SetBool("Ataque", false);
        if (m_WaitTime <= 0)
        {
            NextPoint();
            m_WaitTime = startWaitTime;
        }
        else
        {
            m_WaitTime -= Time.deltaTime;
        }
    }

    public void NextPoint()
    {
        m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % wayPoints.Length;
        agent.SetDestination(wayPoints[m_CurrentWaypointIndex].position);
    }

    private void Chase()
    {
        agent.SetDestination(player.position);
        agent.speed = speedRun;
        animInimigo.SetBool("Ataque", false);
    }
    
    private void Attack()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        animInimigo.SetBool("Ataque", true);
        //Debug.Log("Atacar");
    }
}
