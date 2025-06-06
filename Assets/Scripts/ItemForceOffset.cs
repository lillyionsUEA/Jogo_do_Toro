using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemForceOffset : MonoBehaviour
{
    [SerializeField] private float YOffset = -1f;
    [SerializeField] private string TargetTag1 = "TwoClickItem";
    [SerializeField] private string TargetTag2 = "SlowTimerItem";
    [SerializeField] private string TargetTag3 = "RottenItem";

    void Awake()
    {
        if (CompareTag(TargetTag1) || CompareTag(TargetTag2) || CompareTag(TargetTag3))
        {
            transform.position += new Vector3(0f, YOffset, 0f);
            Debug.Log($"{name} had meets a target item for Y offset applied: {YOffset}", this);
        }
        else
        {
            Debug.Log($"{name} is not a special item, no offset applied", this);
        }
    }
   
}
