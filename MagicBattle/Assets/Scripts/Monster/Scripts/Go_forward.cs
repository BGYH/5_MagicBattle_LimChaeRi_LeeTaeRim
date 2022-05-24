using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Go_forward : MonoBehaviour
{
    public float rotate_speed = 200f;
    public float shoow_speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * shoow_speed * Time.deltaTime);
        transform.Rotate(Vector3.up * Time.deltaTime * rotate_speed);
    }

    
}
