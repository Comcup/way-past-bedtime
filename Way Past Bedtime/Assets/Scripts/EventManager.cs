using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    public delegate void flashLight(GameObject collision);
    public static event flashLight collide;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void flashlightHitSomething(GameObject collision)
    {
        collide(collision);
    }
}
