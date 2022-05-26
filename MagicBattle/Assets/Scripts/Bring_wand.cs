using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class Bring_wand : MonoBehaviour
{
    public XRController controller = null;
    public int button_num = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        change_inter();
    }
    void change_inter()
    {
        
        magicAttack ma = GameObject.Find("XR Origin").GetComponent<magicAttack>();
        

        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 position))
        {


            if (position.y > 0)
            {
                ma.go = 1;
                transform.Find("RightHand Controller").gameObject.SetActive(false);
                set_active(true);
                transform.Find("Camera Offset").Find("Ray Interactor").gameObject.SetActive(false);
            }
            else if (position.y < 0)
            {
                ma.go = 0;
                transform.Find("RightHand Controller").gameObject.SetActive(true);
                set_active(false);
                transform.Find("Camera Offset").Find("Ray Interactor").gameObject.SetActive(true);
            }
        }

    }
    void set_active(bool a)
    {
        string fileName = "TestJson";
        string path = Application.dataPath + "/" + fileName + ".Json";

        FileStream filestream = new FileStream(path, FileMode.Open);
        byte[] data = new byte[filestream.Length];
        filestream.Read(data, 0, data.Length);
        filestream.Close();
        string json = Encoding.UTF8.GetString(data);

        PlayerState myplayerState = JsonUtility.FromJson<PlayerState>(json);
        switch (myplayerState.final_wand)
        {
            case "earth":
                transform.Find("earthwand_controller").gameObject.SetActive(a);
                break;
            case "fire":
                transform.Find("firewand_controller").gameObject.SetActive(a);
                break;
            case "water":
                transform.Find("waterwand_controller").gameObject.SetActive(a);
                break;
            case "wind":
                transform.Find("windwand_controller").gameObject.SetActive(a);
                break;
            case "electric":
                transform.Find("electricwand_controller").gameObject.SetActive(a);
                break;
            case "dark":
                transform.Find("darkwand_controller").gameObject.SetActive(a);
                break;
            case "light":
                transform.Find("lightwand_controller").gameObject.SetActive(a);
                break;
            case "illusion":
                transform.Find("illusionwand_controller").gameObject.SetActive(a);
                break;
        }
    }
}
