using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finder : MonoBehaviour
{
    public CardViewPriority priority;
    // Start is called before the first frame update
    void Start()
    {
        priority = FindObjectOfType<CardViewPriority>();
    }

    // Update is called once per frame
    void Update()
    {
        priority = FindObjectOfType<CardViewPriority>();
    }
}
