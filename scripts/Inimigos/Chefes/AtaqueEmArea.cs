using UnityEngine;

public class AtaqueEmArea : MonoBehaviour
{
    public float tempo = 1;
    public float cooldown = 10f;
    public Vector3 alvo;
    private bool subir = false;
    private bool moverD = false;
    private bool moverE = false;
    private bool descer = false;
    private bool tempoC = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        alvo = new Vector3(0, 1, 0);
        transform.position = alvo;
        alvo.Set(0, 0, 0);
        subir = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("Peixe"))
        {
            if (subir)
            {
                cooldown = 10;
                alvo.y += 0.5f;
                transform.transform.position = alvo;
                if (alvo.y >= 5)
                {
                    subir = false;
                }
                moverD = true;
            }
            else if (moverD)
            {
                tempo += Time.deltaTime;
                if (tempo >= 3)
                {
                    alvo.x += 0.3f;
                    transform.transform.position = alvo;
                    if (alvo.x >= 10)
                    {
                        moverD = false;
                    }
                }
                descer = true;
            }
            else if (descer)
            {
                alvo.y -= 0.5f;
                transform.transform.position = alvo;
                if (alvo.y <= 1)
                {
                    descer = false;
                }
                moverE = true;
                tempo = 0;
            }
            else if (moverE)
            {
                tempo += Time.deltaTime;
                if (tempo >= 3)
                {
                    alvo.x -= 0.5f;
                    transform.transform.position = alvo;
                    if (alvo.x <= 0)
                    {
                        moverE = false;
                    }
                }
                tempoC = true;
            }
            else if (tempoC)
            {
                tempo = 0;
                cooldown -= Time.deltaTime;
                if (cooldown <= 0)
                {
                    subir = true;
                    tempoC = false;
                }
            }
        }
        if (gameObject.CompareTag("golem"))
        {
            if (subir)
            {
                cooldown = 10;
                alvo.y += 0.5f;
                transform.transform.position = alvo;
                if (alvo.y >= 5)
                {
                    subir = false;
                }
                moverD = true;
            }
            else if (moverD)
            {
                tempo += Time.deltaTime;
                if (tempo >= 3)
                {
                    alvo.x += 0.3f;
                    transform.transform.position = alvo;
                    if (alvo.x >= 10)
                    {
                        moverD = false;
                    }
                }
                descer = true;
            }
            else if (descer)
            {
                alvo.y -= 0.5f;
                transform.transform.position = alvo;
                if (alvo.y <= 1)
                {
                    descer = false;
                }
                moverE = true;
                tempo = 0;
            }
            else if (moverE)
            {
                tempo += Time.deltaTime;
                if (tempo >= 3)
                {
                    alvo.x -= 0.5f;
                    transform.transform.position = alvo;
                    if (alvo.x <= 0)
                    {
                        moverE = false;
                    }
                }
                tempoC = true;
            }
            else if (tempoC)
            {
                tempo = 0;
                cooldown -= Time.deltaTime;
                if (cooldown <= 0)
                {
                    subir = true;
                    tempoC = false;
                }
            }
        }
    }
}
