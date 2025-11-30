using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErebosManager : MonoBehaviour
{
    [Header("Spawn Points")]
    public Transform[] spawnPoints;
    public GameObject statuePrefab;

    [Header("Boss Configurações")]
    public bool vulneravel = false;
    public float tempoVulneravel = 5f;

    private int bossSpawnIndex = -1;
    private List<GameObject> estatuasAtivas = new List<GameObject>();
    [Header("Referência ao ciclo de ataque")]
    public BossAttackCycle bossCycle;

    void Start()
    {
        EscolherNovoSpawnParaOBoss();
        SpawnarEstatuas();
    }

    void EscolherNovoSpawnParaOBoss()
    {
        int novoIndex;

        
        do
        {
            novoIndex = Random.Range(0, spawnPoints.Length);
        }
        while (novoIndex == bossSpawnIndex);

        bossSpawnIndex = novoIndex;

        transform.position = spawnPoints[bossSpawnIndex].position;
    }

    void SpawnarEstatuas()
    {
        RemoverEstatuasAntigas();

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (i == bossSpawnIndex)
                continue;

            GameObject nova = Instantiate(statuePrefab, spawnPoints[i].position, spawnPoints[i].rotation);
            estatuasAtivas.Add(nova);
        }
    }

    public void FoiAtingidoPeloJogador(int dano)
    {
        if (!vulneravel)
        {
            Debug.Log("Boss ficou VULNERÁVEL!");
            StartCoroutine(TornarVulneravel());
        }
    }

    IEnumerator TornarVulneravel()
    {
        vulneravel = true;

        RemoverEstatuasAntigas();

        if (bossCycle != null)
        {
            
            bossCycle.finishedCombo = true;  
        }

        yield return new WaitForSeconds(tempoVulneravel);

        vulneravel = false;

        EscolherNovoSpawnParaOBoss();
        SpawnarEstatuas();
    }

    public void RemoverEstatuasAntigas()
    {
        foreach (GameObject estatua in estatuasAtivas)
        {
            if (estatua != null)
                Destroy(estatua);
        }

        estatuasAtivas.Clear();
    }
}

