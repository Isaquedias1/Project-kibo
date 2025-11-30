using UnityEngine;

public class TrocaCamera : MonoBehaviour
{
    public Transform Cam;
    public Transform Cam2;
    public Movimento MoveScript;
    //public Movimento_teste MoveScript2;

    void Update()
    {
        Troca();
    }

    void Troca()
    {
        if (Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Keypad1))
        {
            Cam.gameObject.SetActive(false);
            Cam2.gameObject.SetActive(true);
            // Desativar MoveScript
            MoveScript.enabled = false;

            // Ativar MoveScript2
            //MoveScript2.enabled = true;
        }
        if (Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Keypad2))
        {
            Cam.gameObject.SetActive(true);
            Cam2.gameObject.SetActive(false);
            // Ativar MoveScript
            MoveScript.enabled = true;

            // Desativar MoveScript2
            //MoveScript2.enabled = false;
        }
    }
}