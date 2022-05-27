using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.VFX;
using Magio;
using System.IO;
using System.Text;
using UnityEngine.UI;

public class magicAttack : MonoBehaviour
{
    public XRController controller;
    public XRController Lcontroller;
    public GameObject Fire;
    public GameObject Ice;
    public GameObject Electric;
    public GameObject Light;
    public GameObject Darkness;
    public GameObject Illusion;
    public GameObject Earth;
    public GameObject Wind;
    public GameObject MagicPos; //원래 Transform이었음
    public float time = 0.0f;

    public GameObject magic1;
    public GameObject magic2;
    public GameObject magic3;
    public GameObject Damage;

    public XRController earthcon;
    public XRController firecon;
    public XRController watercon;
    public XRController windcon;
    public XRController electriccon;
    public XRController darkcon;
    public XRController lightcon;
    public XRController illusioncon;

    public int go = 0;

    //public GameObject Monster;

    // Start is called before the first frame update
    void Start()
    {
        magic1 = Get_magic(find_magic(1));
        magic2 = Get_magic(find_magic(2));
        magic3 = Get_magic(find_magic(3));
        MagicPos = find_pos(find_magic(4));
        controller = find_con(find_magic(4));
    }

    // Update is called once per frame
    void Update()
    {
        if (go == 1)
        {
            controller.inputDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
            Lcontroller.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 position);
            time += Time.deltaTime;

            if (position.x > 0 && position.y < 0.7) //왼손 조이스틱 오른쪽으로 당기면
            {
                if (triggerValue > 0.1f) //오른손 트리거 눌렀을 때
                {
                    GameObject bullet3 = (GameObject)Instantiate(magic3, MagicPos.transform.position, MagicPos.transform.rotation);
                    if (time > 0.7f)
                    {
                        Debug.Log("Trigger - magic3");
                        Instantiate(Damage, MagicPos.transform.position, MagicPos.transform.rotation);
                        time = 0.0f;
                    }
                    Destroy(bullet3, 2.0f); //2초 후 발사한 마법공격 삭제
                }
            }
            else if (position.x < 0 && position.y < 0.7) //왼쪽으로 당기면
            {
                if (triggerValue > 0.1f)
                {
                    GameObject bullet1 = (GameObject)Instantiate(magic1, MagicPos.transform.position, MagicPos.transform.rotation);
                    if (time > 0.7f)
                    {
                        Debug.Log("Trigger - magic1");
                        Instantiate(Damage, MagicPos.transform.position, MagicPos.transform.rotation);
                        time = 0.0f;
                    }
                    Destroy(bullet1, 2.0f);
                }
            }
            if (position.y >= 0.7) //위쪽으로 당기면
            {
                if (triggerValue > 0.1f)
                {
                    GameObject bullet2 = (GameObject)Instantiate(magic2, MagicPos.transform.position, MagicPos.transform.rotation);
                    if (time > 0.7f)
                    {
                        Debug.Log("Trigger - magic2");
                        Instantiate(Damage, MagicPos.transform.position, MagicPos.transform.rotation);
                        time = 0.0f;
                    }
                    Destroy(bullet2, 2.0f);
                }
            }
        }
        
    }
    GameObject Get_magic(string title)
    {
        string fileName = "TestJson";
        string path = Application.dataPath + "/" + fileName + ".Json";

        FileStream filestream = new FileStream(path, FileMode.Open);
        byte[] data = new byte[filestream.Length];
        filestream.Read(data, 0, data.Length);
        filestream.Close();
        string json = Encoding.UTF8.GetString(data);
        PlayerState myplayerState = JsonUtility.FromJson<PlayerState>(json);

        switch (title)
        {
            case "earth":
                return Earth;

            case "fire":
                return Fire;

            case "water":
                return Ice;

            case "wind":
                return Wind;

            case "electric":
                return Electric;

            case "dark":
                return Darkness;

            case "light":
                return Light;

            case "illusion":
                return Illusion;

            default:
                return null;

        }
    }
    string find_magic(int num) //원하는 번호의 속성 가져오기
    {

        string fileName = "TestJson";
        string path = Application.dataPath + "/" + fileName + ".Json";

        FileStream filestream = new FileStream(path, FileMode.Open);
        byte[] data = new byte[filestream.Length];
        filestream.Read(data, 0, data.Length);
        filestream.Close();
        string json = Encoding.UTF8.GetString(data);

        PlayerState myplayerState = JsonUtility.FromJson<PlayerState>(json);
        if (num == 1)
        {
            return myplayerState.wand1;
        }
        else if (num == 2)
        {
            return myplayerState.wand2;
        }
        else if (num == 3)
        {
            return myplayerState.wand3;
        }
        else if (num == 4)
        {
            return myplayerState.final_wand;
        }
        else
        {
            return "";
        }


    }
    GameObject find_pos(string wand)
    {
        
        switch (wand)
        {
            case "earth":
                return transform.GetChild(3).gameObject.transform.GetChild(0).gameObject;
                
            case "fire":
                return transform.GetChild(4).gameObject.transform.GetChild(0).gameObject; ;

            case "water":
                return transform.GetChild(5).gameObject.transform.GetChild(0).gameObject; ;

            case "wind":
                return transform.GetChild(6).gameObject.transform.GetChild(0).gameObject; ;

            case "electric":
                return transform.GetChild(7).gameObject.transform.GetChild(0).gameObject; ;

            case "dark":
                return transform.GetChild(8).gameObject.transform.GetChild(0).gameObject; ;

            case "light":
                return transform.GetChild(9).gameObject.transform.GetChild(0).gameObject; ;

            case "illusion":
                return transform.GetChild(10).gameObject.transform.GetChild(0).gameObject; ;

            default:
                return null;

        }
    }

    XRController find_con(string wand)
    {
        GameObject a;
        switch (wand)
        {
            case "earth":
                return earthcon;

            case "fire":
                return firecon;

            case "water":
                return watercon;

            case "wind":
                return windcon;

            case "electric":
                return electriccon;

            case "dark":
                return darkcon;

            case "light":
                return lightcon;

            case "illusion":
                return illusioncon;

            default:
                return null;

        }
    }

}
