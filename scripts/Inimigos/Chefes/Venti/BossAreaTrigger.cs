using UnityEngine;

public class BossAreaTrigger : MonoBehaviour
{
    public VentiTornadp boss;
    public bool ativarSomenteUmaVez = true;

    private bool j·Ativou = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ativarSomenteUmaVez && j·Ativou) return;

            j·Ativou = true;

            boss.enabled = true;

            Debug.Log("Player entrou na ·rea do boss!");
        }
    }
}

