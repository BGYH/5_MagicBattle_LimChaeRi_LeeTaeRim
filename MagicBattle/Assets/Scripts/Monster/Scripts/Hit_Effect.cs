using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit_Effect : MonoBehaviour
{

    public float time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        
        time += Time.deltaTime;
        Play_Animation ani = GameObject.Find("wizard_weapon_legacy DEMO (1)").GetComponent<Play_Animation>();
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(false);
        transform.GetChild(5).gameObject.SetActive(false);
        transform.GetChild(6).gameObject.SetActive(false);
        transform.GetChild(7).gameObject.SetActive(false);


        if (other.gameObject.tag == "wind")
        {
            ani.damaged();
            ani.damaged_fin();
            transform.GetChild(0).gameObject.SetActive(true);
            

        }
        else if (other.gameObject.tag == "light")
        {
            ani.damaged();
            ani.damaged_fin();
            transform.GetChild(1).gameObject.SetActive(true);
            
        }
        else if (other.gameObject.tag == "dark")
        {
            ani.damaged();
            ani.damaged_fin();
            transform.GetChild(2).gameObject.SetActive(true);
            
        }
        else if (other.gameObject.tag == "earth")
        {
            ani.damaged();
            ani.damaged_fin();
            transform.GetChild(3).gameObject.SetActive(true);

        }
        else if (other.gameObject.tag == "fire")
        {
            ani.damaged();
            ani.damaged_fin();
            transform.GetChild(4).gameObject.SetActive(true);

        }
        else if (other.gameObject.tag == "water")
        {
            ani.damaged();
            ani.damaged_fin();
            transform.GetChild(5).gameObject.SetActive(true);

        }
        else if (other.gameObject.tag == "electric")
        {
            ani.damaged();
            ani.damaged_fin();
            transform.GetChild(6).gameObject.SetActive(true);

        }
        else if (other.gameObject.tag == "illusion")
        {
            ani.damaged();
            ani.damaged_fin();
            transform.GetChild(7).gameObject.SetActive(true);

        }



    }
}
