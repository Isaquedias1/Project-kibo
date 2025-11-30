using UnityEngine;

public class Estatua : MonoBehaviour
{
    private ErebosManager boss;

    [Header("Configuração da Estátua")]
    public bool destruiQuandoAtingida = true;
    public GameObject Player;
    public BossAttackCycle bossCycle;

    void Start()
    {
        
        boss = FindAnyObjectByType<ErebosManager>();
    }

    public void LevarDano()
    {
        if (boss != null)
        {
            boss.FoiAtingidoPeloJogador(0);
            bossCycle.finishedCombo = false;
        }

        Debug.Log("Jogador atingiu uma ESTÁTUA! Ele deve levar dano ou sofrer penalidade.");

        // Aqui você coloca dano ao jogador, se quiser.
        Vidas PlayerVida = Player.GetComponent<Vidas>();
        PlayerVida.TomarDano(1);

        

        if (destruiQuandoAtingida)
            Destroy(gameObject);
    }
}
