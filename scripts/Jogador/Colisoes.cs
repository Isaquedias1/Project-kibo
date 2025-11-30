using UnityEngine;
using UnityEngine.SceneManagement;

public class Colisoes : MonoBehaviour
{
    [SerializeField] string cenas;

    private void OnCollisionEnter(Collision collision)
    {
        Vidas playerVidas = GetComponent<Vidas>();

        switch (collision.gameObject.tag)
        {
            case "agua":
                playerVidas.TomarDano(50);
                break;

            case "barco":
                SceneManager.LoadScene(cenas);
                break;
        }

    }

}
