using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSpawner : MonoBehaviour
{
    //public GameObject itemPrefab;
    private TimerController timerController;
    private float timer = 0f;
    public List<GameObject> itemPrefabs;
    public Transform leftSpawnPoint;
    public Transform rightSpawnPoint;
    public float spawnInterval = 1.5f;

    public enum MoveDirection { Left, Right }
    [SerializeField]
    public MoveDirection moveDirection = MoveDirection.Left;

    void Update()
    {
        if (timerController == null || !timerController.TimerRunning)
            return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnItem();
            timer = 0f;
        }
    }

    void Start()
    {
        timerController = FindObjectOfType<TimerController>();
        if (timerController == null)
        {
            Debug.LogWarning("TimerController not found.");
        }
    }

    void SpawnItem()
    {
        int index = Random.Range(0, itemPrefabs.Count);

        Vector3 spawnPostion = (moveDirection == MoveDirection.Left) ? rightSpawnPoint.position : leftSpawnPoint.position;
        GameObject item = Instantiate(itemPrefabs[index], spawnPostion, Quaternion.identity);

        ItemSlider slider = item.GetComponent<ItemSlider>();
        if (slider != null)
        {
            //slider.moveDirection = (ItemSlider.MoveDirection)((moveDirection == MoveDirection.Left) ? MoveDirection.Left : MoveDirection.Right);
        }
        else
        {
            Debug.LogError("Item prefab doesn't has ItemSlider component: " + itemPrefabs[index].name);
        }
    }
}
