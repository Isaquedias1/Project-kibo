using UnityEngine;

public class rotateSkybox : MonoBehaviour
{
    //gaybi bota esse script na camera do personagem
    public float RotateSpeed = 1.2f;
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * RotateSpeed);
    }
}
