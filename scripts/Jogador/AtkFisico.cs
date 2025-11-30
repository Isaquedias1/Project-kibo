using System.Collections;
using UnityEngine;

public class AtkFisico : MonoBehaviour
{
    public float attackRange = 2f;
    public int atqDano = 10;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float Cooldown = 1f;
    private Animator anim;
    public GameObject Lira;


    private float proximoAtaque = 0f;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Time.time >= proximoAtaque)
        {
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetBool("Ataque", true);
                StartCoroutine(AtivarLiraAposTempo(0.5f));
                Debug.Log("Bateu");
                Attack();
                proximoAtaque = Time.time + Cooldown;
            }
            if (Input.GetMouseButtonUp(0))
            {
                anim.SetBool("Ataque", false);
                Lira.SetActive(false);
            }
        }
    }

    void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider inimigo in hitEnemies)
        {
           
            Estatua est = inimigo.GetComponent<Estatua>();
            if (est != null)
            {
                est.LevarDano();
                Debug.Log("Acertou uma ESTÁTUA.");
                continue;
            }

            
            ErebosManager boss = inimigo.GetComponent<ErebosManager>();
            if (boss != null)
            {
                boss.FoiAtingidoPeloJogador(atqDano);
                Debug.Log("Acertou o BOSS.");
                continue;
            }

            

            Vidas vidaInimigo = inimigo.GetComponent<Vidas>();
            if (vidaInimigo != null)
            {
                vidaInimigo.TomarDano(atqDano);
                Debug.Log("Bateu no inimigo");
            }
        }
    }

    IEnumerator AtivarLiraAposTempo(float tempo)
    {
        yield return new WaitForSeconds(tempo);
        Lira.SetActive(true);
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}