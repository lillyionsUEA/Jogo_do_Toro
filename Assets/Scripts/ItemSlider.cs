using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SocialPlatforms.Impl;

public class ItemSlider : MonoBehaviour
{
    public enum MoveDirection { Left, Right }
    private MoveDirection moveDirection = MoveDirection.Left;

    [Header("Movement Settings")]
    public float slideSpeed = 2f;
    public float leftBoundaryX = -10f;
    public float rightBoundaryX = 10f;
    private AudioSource slidingAudio;

    [Header("Click Count")]
    private int clickcount = 0;
    public int requiredClicks = 2;



    private bool collected = false;

    void Start()
    {
        slidingAudio = GetComponent<AudioSource>();
        if (slidingAudio != null)
        {
            slidingAudio.Play();
        }
    }

    public void SetDirection(MoveDirection dir)
    {
        moveDirection = dir;
    }

    void Update()
    {
        if (collected) return;

        Vector3 dirVector = (moveDirection == MoveDirection.Left) ? Vector3.left : Vector3.right;
        transform.Translate(dirVector * slideSpeed * Time.deltaTime);

        if (moveDirection == MoveDirection.Left && transform.position.x <= leftBoundaryX)
        {
            Destroy(gameObject);
        }
        else if (moveDirection == MoveDirection.Right && transform.position.x >= rightBoundaryX)
        {
            Destroy(gameObject);
        }
    }

    public void Collect()
    {
        if (collected) return;

        clickcount++;

        if (clickcount >= requiredClicks)
        {
            collected = true;
            if (slidingAudio != null && slidingAudio.isPlaying)
            {
                slidingAudio.Stop();
            }
            int pointsToAdd = (requiredClicks == 2) ? 2 : 1;
            ScoreManager.Instance.AddPoints(pointsToAdd);

            if (CompareTag("SlowTimerItem"))
            {
                TimerController timerController = FindObjectOfType<TimerController>();
                if (timerController != null)
                {
                    timerController.ActivateSlowMotion();  // Ativa o efeito de desaceleração de tempo
                }
            }

            if (CompareTag("RottenItem"))
            {
                ScoreManager.Instance.AddPoints(-2); // Penaliza o jogador
            }

            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Clique necessário: " + (requiredClicks - clickcount));
        }
    }


    private void OnDestroy()
    {
        if (slidingAudio != null && slidingAudio.isPlaying)
        {
            slidingAudio.Stop();
        }
    }
}
