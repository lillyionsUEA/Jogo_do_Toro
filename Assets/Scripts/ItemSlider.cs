using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ItemSlider : MonoBehaviour
{
    public enum MoveDirection { Left, Right }
    private MoveDirection moveDirection = MoveDirection.Left;

    public float slideSpeed = 2f;
    public float leftBoundaryX = -10f;
    public float rightBoundaryX = 10f;
    private bool collected = false;
    private AudioSource slidingAudio;

    private int clickcount = 0;
    public int requiredClicks = 2; // Cliques para o item edredom

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

        clickcount++; // Incrementa o contador de cliques

        if (clickcount >= requiredClicks)
        {
            collected = true;
            if (slidingAudio != null && slidingAudio.isPlaying)
            {
                slidingAudio.Stop();
            }
            int pointsToAdd = (requiredClicks == 2) ? 2 : 1;
            ScoreManager.Instance.AddPoints(pointsToAdd);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Clique necessário: " + (requiredClicks - clickcount));
        }

        /*collected = true;
        if (slidingAudio != null && slidingAudio.isPlaying)
        {
            slidingAudio.Stop();
        }

        ScoreManager.Instance.AddPoints(1);

        Destroy(gameObject);*/
    }

    private void OnDestroy()
    {
        if (slidingAudio != null && slidingAudio.isPlaying)
        {
            slidingAudio.Stop();
        }
    }
}
