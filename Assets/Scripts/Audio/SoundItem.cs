
using UnityEngine;

[System.Serializable]
public class SoundItem
{
    public SoundName soundName;
    public AudioClip soundClip;
    public string soundDescription;
    [Range(0.1f, 1.5f)] public float soundPitchRandomVariationMin = 0.8f;
    [Range(0.1f, 1.5f)] public float soundPitchRandomVariationMax = 1.2f;
    [Range(0f, 1.0f)] public float soundVolume = 1.0f;
}


public enum SoundName
{
    Effect_EnemyHit,
    Effect_EnemySpawn,
    Effect_Explosion1,
    Effect_Explosion2,
    Effect_Explosion3,
    Effect_Explosion4,
    Effect_TowerDeploy,
    Msuic_Intro,
    Msuic_Game,
    Msuic_GameOver,
    Music_Ambient,
    None
}

