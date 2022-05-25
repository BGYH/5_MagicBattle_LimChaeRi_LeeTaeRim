using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play_Animation : MonoBehaviour
{
    public Animator animator;
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 2.0f)
        {
            animator.SetBool("idle_combat", true);
        }
        //if(time >= 10f)
        //{
        //    attack_magic();
        //    attack_magic_fin();
        //    time = 0f;
            
        //}
        
    }
    public void damaged()
    {
        animator.SetBool("damage_001", true);
      
    }
    public void damaged_stop()
    {
        animator.SetBool("damage_001", false);
    }
    public void damaged_fin()
    {
        Invoke("damaged_stop", 1f);
    }
    public IEnumerator attack()
    {
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Monster 선빵");
        animator.SetBool("attack_short_001", true);
    }
    public void attack_stop()
    {
        animator.SetBool("attack_short_001", false);
    }
    public IEnumerator attack_fin() 
    {
        yield return new WaitForSeconds(1.0f);
        Invoke("attack_stop", 1f); //공격모션 attack() 쓴다음에 바로 다음 줄에 attack_fin()을 써줘야함
    }
    public void attack_magic()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
    public void attack_magic_stop()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
    public void attack_magic_fin()
    {
        Invoke("attack_magic_stop", 6f); //마찬가지로 attack_magic()쓴다음attack_magic_fin을 써준다. 스탑은 쓰는거 아님
    }
    public void dead()
    {
        animator.SetBool("dead", true);

    }
    public void dead_stop()
    {
        animator.SetBool("dead", false);
    }
    public void dead_fin()
    {
        Invoke("dead", 1f);
    }
}
