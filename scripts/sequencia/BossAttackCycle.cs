using UnityEngine;

public class BossAttackCycle : MonoBehaviour
{
    public bool finishedCombo = false;        
    public MusicAttackManager music;          // Referência ao minigame musical

    private bool waitingForMusic = false;

    void Update()
    {
        
        if (finishedCombo && !waitingForMusic)
        {
            waitingForMusic = true;
            music.EnableMusicChance();
        }

       
        if (waitingForMusic && music.MinigameFinished)
        {
            waitingForMusic = false;
            finishedCombo = false;
        }
    }
}
