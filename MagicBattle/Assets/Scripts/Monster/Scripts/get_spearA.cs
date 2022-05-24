using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class get_spearA : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "attack")
        {
            
            FadeScreen fadeScreen = GameObject.Find("Fader Screen").GetComponent<FadeScreen>();
            Destroy(other.gameObject);
            player_damaged();
            
            
        }
    }
    
    void player_damaged()
    {
      
        
        FadeScreen fadeScreen = GameObject.Find("Fader Screen").GetComponent<FadeScreen>();
        fadeScreen.FadeIn();
        


    }
}
