using UnityEngine;

public class FuncElevador : MonoBehaviour
{
    public bool canMove;

    [SerializeField] float speed;
    [SerializeField] int startPoint;
    [SerializeField] Transform[] points;

    int i;
    bool reverse;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = points[startPoint].position;
        i = startPoint;
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(transform.position, points[i].position) < 0.01f)
        {
            canMove = false;

            if (i == points.Length - 1)
            {
                reverse = true;
                i--;
                return;
            }
            else if (i == 0)
            {
                reverse = true;
                i++;
                return;
            }

            if (reverse)
            {
                i--;
            }
            else
            {
                i++;
            }
        }

        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.SetParent(transform);
    }
    private void OnCollisionExit(Collision collision)
    {
        collision.transform.SetParent(null);
    }
    private void OnTriggerEnter(Collider other)
    {
        canMove = true;
    }
}
