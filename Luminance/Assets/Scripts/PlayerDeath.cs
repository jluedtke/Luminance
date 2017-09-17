using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour {

    public ParticleSystem[] wispParticles;
    public ParticleSystem[] enemyParticles;

    public Color almostDeadColor;

    public List<Color> originalColors;

    private bool PlayerDied;

    public void Start()
    {
        for (int i = 0; i < wispParticles.Length; i++)
        {
            var psw = wispParticles[i].main;
            originalColors.Add(psw.startColor.color);
        }
    }

    public void PlayerAt25()
    {
        if (PlayerDied)
            return; 

        for (int i = 0; i < wispParticles.Length; i++)
        {
            var psw = wispParticles[i].main;

            psw.startColor = almostDeadColor;

        }
    }

    public void PlayerAbove25()
    {
        for (int i = 0; i < wispParticles.Length; i++)
        {
            var psw = wispParticles[i].main;
            psw.startColor = originalColors[i];
        }
    }

    public void PlayerHasDied()
    {
        PlayerDied = true;

        for (int x = 0; x < enemyParticles.Length; x++)
        {
            Instantiate(enemyParticles[x], GameObject.Find("Player/PS_Wisp").transform);
        }
        for (int i = 0; i < wispParticles.Length; i++)
        {
            var psw = wispParticles[i].main;
            psw.loop = false;

        }

    }
}
