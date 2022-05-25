using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.XR;
//using UnityEngine.XR.Interaction.Toolkit;

public class Health : MonoBehaviour
{
    //public XRController controller = null;
    public int curHealth = 0;
    public int maxHealth = 100;
    public HealthBar HPBar;

    public ChatController chat;
    public magicAttack MA;
    public GameObject FadeScreen;

    //public GameObject Monster;
    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DamagePlayer(int damage)
    {
        curHealth -= damage;

        HPBar.SetHealth(curHealth);
    }
    /*public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "magic")
        {
            DamagePlayer(3);
        }
    }*/
    public void OnTriggerEnter(Collider other)
    {
        Play_Animation Monster = GameObject.Find("wizard_weapon_legacy DEMO (1)").GetComponent<Play_Animation>();
        FadeScreen Fs = GameObject.Find("Fader Screen").GetComponent<FadeScreen>();
        if (other.gameObject.tag == "magic") //일반 마법에 맞으면
        {
            DamagePlayer(4); //HP가 5 깎인다
            Monster.GetComponent<Play_Animation>().damaged();
            Monster.GetComponent<Play_Animation>().damaged_fin(); //몬스터가 맞는 animation 실행

            if (HPBar.GetComponent<HealthBar>().healthBar.value == 20) //HP가 20이 되면
            {
                MA.magic1.SetActive(false);
                MA.magic2.SetActive(false);
                MA.magic3.SetActive(false);
                MA.Damage.SetActive(false); //몬스터의 공격으로 인해 지팡이가 고장나 모든 마법과 ignitor 비활성화

                Monster.GetComponent<Play_Animation>().attack_magic();
                Monster.GetComponent<Play_Animation>().attack_magic_fin();
                StartCoroutine(Fs.GetComponent<FadeScreen>().FadeOut());
                StartCoroutine(Fs.GetComponent<FadeScreen>().FadeIn());

                StartCoroutine(chat.finalDragon()); //궁극기를 사용해보라는 설명창 등장
            }
        }
        else if (other.gameObject.tag == "dragonBall") //드래곤볼에 맞을경우
        {
            Debug.Log("드래곤볼 맞음");
            DamagePlayer(100);
            Monster.GetComponent<Play_Animation>().dead();
            Monster.GetComponent<Play_Animation>().dead_fin(); //몬스터가 죽는 animation 실행

            StartCoroutine(changeScene());
        }
    }

    public IEnumerator changeScene()
    {
        yield return new WaitForSeconds(10.0f);
        MA.magic1.SetActive(true);
        MA.magic2.SetActive(true);
        MA.magic3.SetActive(true);
        MA.Damage.SetActive(true); //다시 활성화 시켜줘야 또 할때 멀쩡함

        StartCoroutine(FadeScreen.GetComponent<StartEndFade>().FadeOut());
        SceneManager.LoadScene("testScene");

    }
}
