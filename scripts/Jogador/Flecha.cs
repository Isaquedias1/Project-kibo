using UnityEngine;

public class Flecha : MonoBehaviour
{
    public int dano = 5;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Umbra"))
        {
            Vidas inimigoVida = collision.gameObject.GetComponent<Vidas>();
            if (inimigoVida != null)
            {
                inimigoVida.TomarDano(dano);
                //Debug.Log("Inimigo atingido por flecha!");
            }

            Destroy(gameObject);
        }
    }
}
