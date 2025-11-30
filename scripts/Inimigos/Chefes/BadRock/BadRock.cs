using UnityEngine;

public class BadRock : MonoBehaviour
{
    public float speed = 10f;
    public Transform attackTarget;   
    public Transform restPosition;   
    public float damageRadius = 1f;  
    public int damage = 20;

    private bool isAttacking = false;
    private bool isReturning = false;
    public Transform player;
    void Update()
    {
        if (isAttacking)
        {
            MoveTowards(attackTarget.position);
            
            if (Vector3.Distance(transform.position, attackTarget.position) < 0.3f)
            {
                CheckDamage();
                isAttacking = false;
                isReturning = true;
            }
        }
        else if (isReturning)
        {
            MoveTowards(restPosition.position);
            
            if (Vector3.Distance(transform.position, restPosition.position) < 0.3f)
            {
                isReturning = false;
            }
        }
    }

    void MoveTowards(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );
    }

    void CheckDamage()
    {
        if (Vector3.Distance(transform.position, player.position) < damageRadius)
        {
            Debug.Log("PLAYER TOMOU DANO PELA MÃO");
            Vidas vida = player.GetComponent<Vidas>();
            vida.TomarDano(2);
        }
    }

    public void StartAttack()
    {
        if (!isAttacking && !isReturning)
            isAttacking = true;
        
    }

    public bool IsIdle()
    {
        return !isAttacking && !isReturning;
        
    }
}
