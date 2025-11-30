using UnityEngine;
using UnityEngine.AI;

public class MovCaronte : MonoBehaviour
{
    public Transform[] pontosDePatrulha;
    private int indiceAtual = 0;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = pontosDePatrulha[indiceAtual].position;
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            indiceAtual = (indiceAtual + 1) % pontosDePatrulha.Length;
            agent.destination = pontosDePatrulha[indiceAtual].position;
        }
    }
}
