using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemForceOffset : MonoBehaviour
{
    [SerializeField] private float YOffset = -1f;
    private string TargetTag1 = "TwoClickItem";
    private string TargetTag2 = "SlowTimerItem";
    private string TargetTag3 = "RottenItem";

    void Awake()
    {
        if (CompareTag(TargetTag1) || CompareTag(TargetTag2) || CompareTag(TargetTag3))
        {
            transform.position += new Vector3(0f, YOffset, 0f);
        }
        else
        {
            Debug.Log($"{name} is not a special item, no offset applied", this);
        }
    }
   
}
