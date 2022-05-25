using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonSpawn : MonoBehaviour
{
    //public List<GameObject> dragons;
    public GameObject Red;
    public GameObject Black;
    public GameObject Red_small;
    public GameObject Green;
    public GameObject Green_small;
    public GameObject Yellow;

    // Start is called before the first frame update
    public void Spawn(string objectName)
    {
        StartCoroutine(Yellow.GetComponent<DragonPosition>().DragonAppear(Yellow));
        /*foreach (var item in dragons)
        {
            item.SetActive(objectName == item.name);
        }*/
    }
}
