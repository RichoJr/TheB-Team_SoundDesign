using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardToHand : MonoBehaviour
{
    public GameObject hand;
    public GameObject it;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hand = GameObject.Find("Hand");
        it.transform.SetParent(hand.transform);
        it.transform.localScale = Vector3.one;
        it.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        it.transform.eulerAngles = new Vector3(0, 0, 0);
    }
}
