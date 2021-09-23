using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCows : MonoBehaviour
{
    public Rigidbody2D meuh;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")){
            Instantiate(meuh, transform.position, transform.rotation);
        }
    }
}
