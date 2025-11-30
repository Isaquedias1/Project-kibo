using UnityEngine;

public class FalsoManager : MonoBehaviour
{
    public GameObject tailPrefab;
    public Transform spawnLeft;
    public Transform spawnRight;

    public float attackCooldown = 2f;
    public int ataquesParaVulneravel = 2;
    public float tempoVulneravel = 3f;  

    private int ataquesRealizados = 0;

    private bool vulneravel = false;
    private bool podeAtacar = true;
    public BossAttackCycle bossCycle;

    private void Update()
    {
        if (!vulneravel && podeAtacar)
            StartCoroutine(FazerAtaque());
    }

    private System.Collections.IEnumerator FazerAtaque()
    {
        podeAtacar = false;

        int lado = Random.Range(0, 2);

        if (lado == 0)
            AtacarDaEsquerda();
        else
            AtacarDaDireita();

        ataquesRealizados++;

        if (ataquesRealizados >= ataquesParaVulneravel)
        {
            vulneravel = true;
            Debug.Log("BOSS VULNERÁVEL");

            if (bossCycle != null)
            {
                bossCycle.finishedCombo = true;   
            }


            
            yield return new WaitForSeconds(tempoVulneravel);

            
            ataquesRealizados = 0;
            vulneravel = false;
            Debug.Log("BOSS RECUPERADO!");

            podeAtacar = true;
            yield break;
        }

        yield return new WaitForSeconds(attackCooldown);
        podeAtacar = true;
    }

    void AtacarDaEsquerda()
    {
        GameObject rabo = Instantiate(tailPrefab);
        Calda atk = rabo.GetComponent<Calda>();
        atk.StartAttack(spawnLeft.position, spawnRight.position);
    }

    void AtacarDaDireita()
    {
        GameObject rabo = Instantiate(tailPrefab);
        Calda atk = rabo.GetComponent<Calda>();
        atk.StartAttack(spawnRight.position, spawnLeft.position);
    }

    public void TomarDano(int dmg)
    {
        if (!vulneravel)
        {
            Debug.Log("Boss não pode tomar dano");
            return;
        }

        Debug.Log("Boss tomou dano!");

        
        ataquesRealizados = 0;
        vulneravel = false;
        podeAtacar = true;
    }
}
