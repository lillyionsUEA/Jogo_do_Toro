using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPoint : MonoBehaviour
{
    public enum MoveDirection { Left, Right }
    public MoveDirection moveDirection = MoveDirection.Left;

    public List<GameObject> itemPrefabs;
    public float minSpawnInterval = 2f;
    public float maxSpawnInterval = 5f;

    public float minSpeed = 1f;
    public float maxSpeed = 3f;

    private float spawnInterval;
    private float timer = 0f;
    private TimerController timerController;

    void Start()
    {
        timerController = FindObjectOfType<TimerController>();
        spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        if (timerController == null)
        {
            return;
        }
        //Debug.Log("TimerController found: " + timerController.name);
    }

    void Update()
    {
        if (timerController == null || !timerController.TimerRunning)
            return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnItem();
            timer = 0f;
            spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }

    void SpawnItem()
    {
        int index = Random.Range(0, itemPrefabs.Count);
        GameObject item = Instantiate(itemPrefabs[index], transform.position, Quaternion.identity);

        if (item.CompareTag("TwoClickItem"))
        {
            item.GetComponent<ItemSlider>().requiredClicks = 2;
        }
        else
        {
            item.GetComponent<ItemSlider>().requiredClicks = 1;
        }

        ItemSlider itemSlider = item.GetComponent<ItemSlider>();
        if (itemSlider != null)
        {
            itemSlider.SetDirection((ItemSlider.MoveDirection)moveDirection);
            itemSlider.slideSpeed = Random.Range(minSpeed, maxSpeed);
        }
        else
        {
            Debug.LogWarning("ItemSlider component not found on the spawned item: " + item.name);
        }
    }

}
