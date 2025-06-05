using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTimerItem : MonoBehaviour
{
    public float slowDownFactor = 0.5f;    // Quanto mais baixo, mais devagar o tempo
    public float duration = 2f;            // Duração do power-up em segundos

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SlowTimerItem")) // Verifica se o objeto colidido é o power-up de slowdown
        {
            // Inicia o efeito de slowdown
            StartCoroutine(SlowDownTime());
            
            // Destrói o power-up após a coleta
            Destroy(gameObject);
        }

    }

    private IEnumerator SlowDownTime()
    {
        // Diminui o tempo
        Time.timeScale = slowDownFactor;

        // Mantém o tempo desacelerado por um determinado tempo
        yield return new WaitForSecondsRealtime(duration);  // Usa WaitForSecondsRealtime para não afetar o tempo do jogo enquanto o jogo está em slow motion.

        // Restaura o tempo
        Time.timeScale = 1f;
    }
}
