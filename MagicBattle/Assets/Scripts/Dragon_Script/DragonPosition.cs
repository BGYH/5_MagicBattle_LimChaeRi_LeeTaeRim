using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Magio;

public class DragonPosition : MonoBehaviour
{
    public Transform Endpot;
    public Animator D_animator;
    public GameObject FirePos;
    public GameObject DragonBall;
    public GameObject Igniter;
    public AudioSource wings;
    public AudioSource purr;

    public void Start()
    {
        //D_animator.SetBool("Attack FireBall", false);
        wings.Play();
        purr.Play();
    }

    public IEnumerator DragonAppear(GameObject a)
    {
        this.gameObject.SetActive(true);
        float count = 0;
        Vector3 wasPos = a.transform.position;
        Vector3 toPos = Endpot.transform.position;
        Debug.Log("드래곤 등장");

        while (true)
        {
            count += Time.deltaTime*1/2;
            a.transform.position = Vector3.Lerp(wasPos, toPos, count);

            if (count >= 1)
            {
                a.transform.position = toPos;
                break;
            }
            yield return null;
        }
        wings.Stop();
        D_animator.SetBool("Land", true);
        StartCoroutine(ShootDragonBall());
    }

    public IEnumerator ShootDragonBall()
    {
        yield return new WaitForSeconds(1.0f);
        Instantiate(DragonBall, FirePos.transform.position, FirePos.transform.rotation);
        Igniter.GetComponent<SphereIgnite>().affectedClass = EffectClass.Flame; //이그나이터의 효과 Flame으로 변경
        Instantiate(Igniter, FirePos.transform.position, FirePos.transform.rotation);
        Debug.Log("드래곤볼 발사");
    }
}
