using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPosition : MonoBehaviour
{
    public Transform Endpot;

    public IEnumerator MonsterAppear(GameObject a)
    {
        float count = 0;
        Vector3 wasPos = a.transform.position;
        Vector3 toPos = Endpot.transform.position;
        Debug.Log("몬스터 등장");

        while (true)
        {
            count += Time.deltaTime;
            a.transform.position = Vector3.Lerp(wasPos, toPos, count);

            if (count >= 1)
            {
                a.transform.position = toPos;
                break;
            }
            yield return null;
        }
    }
}
