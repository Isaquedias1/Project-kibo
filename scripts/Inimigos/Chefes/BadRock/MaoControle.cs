using UnityEngine;

public class MaoControle : MonoBehaviour
{
    public MaoAtaque leftHand;
    public MaoAtaque rightHand;

    public float timeBetweenAttacks = 1.5f;
    public float vulnerableTime = 3f;

    public BossAttackCycle cycle;

    private bool cycleRunning = false;

    void Start()
    {
        StartCoroutine(BossCycle());
    }

    System.Collections.IEnumerator BossCycle()
    {
        cycleRunning = true;

        while (true)
        {
            leftHand.Attack();
            yield return new WaitUntil(() => leftHand.IsIdle());
            yield return new WaitForSeconds(timeBetweenAttacks);

            rightHand.Attack();
            yield return new WaitUntil(() => rightHand.IsIdle());
            yield return new WaitForSeconds(timeBetweenAttacks);

            Debug.Log("BOSS VULNERÁVEL — ATAQUE AGORA!");

            cycle.finishedCombo = true;

            yield return new WaitForSeconds(vulnerableTime);

        }
    }
}
