using UnityEngine;

public class MaoAtaque : MonoBehaviour
{
    private BadRock hand;

    void Awake()
    {
        hand = GetComponent<BadRock>();
    }

    public void Attack()
    {
        hand.StartAttack();
    }

    public bool IsIdle()
    {
        return hand.IsIdle();
    }
}
