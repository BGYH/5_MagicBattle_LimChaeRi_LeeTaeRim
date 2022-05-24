using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        if (other.gameObject.tag == "magic") //�̱׳����Ϳ� ���� ���
        {
            DamagePlayer(4); //HP�� 5 ���δ�
            Monster.GetComponent<Play_Animation>().damaged();
            Monster.GetComponent<Play_Animation>().damaged_fin(); //���Ͱ� �´� animation ����

            if (HPBar.GetComponent<HealthBar>().healthBar.value == 20) //HP�� 20�� �Ǹ�
            {
                MA.magic1.SetActive(false);
                MA.magic2.SetActive(false);
                MA.magic3.SetActive(false);
                MA.Igniter.SetActive(false); //������ �������� ���� �����̰� ���峪 ��� ������ ignitor ��Ȱ��ȭ

                Monster.GetComponent<Play_Animation>().attack_magic();
                Monster.GetComponent<Play_Animation>().attack_magic_fin();
                StartCoroutine(Fs.GetComponent<FadeScreen>().FadeOut());
                StartCoroutine(Fs.GetComponent<FadeScreen>().FadeIn());

                StartCoroutine(chat.finalDragon()); //�ñر⸦ ����غ���� ����â ����

                Debug.Log("�ñر� �߻�");

            }
        
        }

    }
}
