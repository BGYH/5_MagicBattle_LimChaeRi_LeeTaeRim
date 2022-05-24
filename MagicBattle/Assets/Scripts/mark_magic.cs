using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEngine.UI;

public class mark_magic : MonoBehaviour
{
    public Text magic1;
    public Text magic2;
    public Text magic3;
    
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        magic1.text = change_korean(find_magic(1));//첫번재 속성 배치
        magic2.text = change_korean(find_magic(2));//두번재 속성 배치
        magic3.text = change_korean(find_magic(3));//세번재 속성 배치

    }

    
    string change_korean(string magic_eng) //가져온 속성 화면에 표시하기 위해 한글로 바꾸기
    {

        switch (magic_eng)
        {
            case "earth":
                return "대지의 마법";
                
            case "fire":
                return "불의 마법";
                
            case "water":
                return "얼음의 마법";
                
            case "wind":
                return "바람의 마법";
               
            case "electric":
                return "전기의 마법";
               
            case "dark":
                return "암흑의 마법";
            
            case "light":
                return "빛의 마법";
          
            case "illusion":
                return "환각의 마법";
        
            default:
                return "";
        
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
        else
        {
            return "";
        }

        
    }
}

