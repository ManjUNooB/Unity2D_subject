using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] GameObject playerObj;
    SpriteRenderer playerRenderer;
    

    // Start is called before the first frame update
    void Start()
    {
        playerRenderer = playerObj.GetComponent<SpriteRenderer>(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
