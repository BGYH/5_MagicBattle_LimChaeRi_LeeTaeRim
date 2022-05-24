using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Magic : MonoBehaviour
{
    public GameObject spear;
    public Transform pos;
    public float x;
    public float y;
    public float time = 0f;
    public Vector3 pp;
    
    // Start is called before the first frame update
    void Start()
    {
        pp.x = pos.position.x;
        pp.y = pos.position.y;
        pp.z = pos.position.z;

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        
        if (time > 0.1f)
        {
            shooting();
            time = 0f;
        }
    }
    void shooting()
    {


        x = Random.Range(-2f, 2f);
        y = Random.Range(-2f, 2f);
        pos.transform.position = new Vector3(pp.x + x, pp.y+y, pp.z);
        Instantiate(spear, pos.position, spear.transform.rotation);
        
        
    }
}
