using UnityEngine;

public class Calda : MonoBehaviour
{
    public float speed = 10f;

    private Vector3 targetPos;
    private bool moving = false;

    public void StartAttack(Vector3 startPos, Vector3 endPos)
    {
        transform.position = startPos; 
        targetPos = endPos;
        moving = true;
    }

    void Update()
    {
        if (!moving) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            speed * Time.deltaTime
        );

        // chegou no fim
        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            moving = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vidas life = other.GetComponent<Vidas>();
            if (life != null)
                life.TomarDano(1);
        }
    }
}
