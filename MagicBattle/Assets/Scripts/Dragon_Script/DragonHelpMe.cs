using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class DragonHelpMe : MonoBehaviour
{
    public string myDragon;
    public GameObject Red;
    public GameObject Black;
    public GameObject Red_Small;
    public GameObject Green;
    public GameObject Green_Small;
    public GameObject Yellow;
    public GameObject MD;

    // Start is called before the first frame update
    public void Spawn()
    {
        string fileName = "TestJson";
        string path = Application.dataPath + "/" + fileName + ".Json";

        FileStream filestream = new FileStream(path, FileMode.Open);
        byte[] data = new byte[filestream.Length];
        filestream.Read(data, 0, data.Length);
        filestream.Close();
        string json = Encoding.UTF8.GetString(data);

        PlayerState myplayerState = JsonUtility.FromJson<PlayerState>(json);
        myDragon = myplayerState.dragon;

        MD = Match_Dragon(myDragon);
        StartCoroutine(MD.GetComponent<DragonPosition>().DragonAppear(MD));
    }

    GameObject Match_Dragon(string a)
    {
        switch (a)
        {
            case "Red":
                return Red;
            case "Black":
                return Black;
            case "Green":
                return Green;
            case "Yellow":
                return Yellow;
            case "Red_Small":
                return Red_Small;
            case "Green_Small":
                return Green_Small;
            default:
                return null;
        }
    }
}
