using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using PDollarGestureRecognizer;
using System.IO;
using System.Text;
using UnityEngine.Events;


public class MovementRecognizer : MonoBehaviour
{
    public XRNode inputSource;
    public InputHelpers.Button inputButton;
    public float inputThreshold = 0.005f;
   // public Transform movementSource;
    public GameObject movementSource;
    public GameObject XR;
   
   

    public float newPositionThresholdDistance = 0.01f;
    public GameObject HelpmeDragon;
    public bool creationMode = true;
    public string newGestureName;

    public float recognitionThreshold = 0.9f;
    
    [System.Serializable]
    public class UnityStringEvent : UnityEvent<string> { }
    public UnityStringEvent OnRecognized;

    private List<Gesture> trainingSet = new List<Gesture>();
    private bool isMoving = false;
    private List<Vector3> positionsList = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        movementSource = find_pos(find_magic(4));
        string[] gestureFiles = Directory.GetFiles(Application.dataPath, "*.xml");
        foreach (var item in gestureFiles)
        {
            trainingSet.Add(GestureIO.ReadGestureFromFile(item));
        }
    }

    // Update is called once per frame
    void Update()
    {
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(inputSource), inputButton, out bool isPressed, inputThreshold);

        //movement 시작
        if(!isMoving && isPressed)
        {
            StartMovement();
        }
        else if (isMoving && !isPressed) //movement 끝
        {
            EndMovement();
        }
        else if (isMoving && isPressed) //movement의 업데이트
        {
            UpdateMovement();
        }
    }

    void StartMovement()
    {
        Debug.Log("Start movement");
        isMoving = true;
        positionsList.Clear();
        //positionsList.Add(movementSource.position); //레이인터랙터가 움직이는 position값을 리스트로 저장
        positionsList.Add(movementSource.transform.position);

        if (HelpmeDragon)
        {
            //Instantiate함수로 파티클 오브젝트를 레이인터랙터의 위치에 복사, 7초 후 삭제
            Destroy(Instantiate(HelpmeDragon, movementSource.transform.position, Quaternion.identity), 5);
        }
    }

    void EndMovement()
    {
        Debug.Log("End movement");
        isMoving = false;

        //posiont list로부터 제스처를 만든다.
        Point[] pointArray = new Point[positionsList.Count];

        for (int i = 0; i < positionsList.Count; i++)
        {
            Vector2 ScreenPoint = Camera.main.WorldToScreenPoint(positionsList[i]);
            pointArray[i] = new Point(ScreenPoint.x, ScreenPoint.y, 0);
        }

        Gesture newGesture = new Gesture(pointArray);

        //trainingSet에 새로운 제스처 저장하기
        if(creationMode)
        {
            newGesture.Name = newGestureName;
            trainingSet.Add(newGesture);

            string fileName = Application.dataPath + "/" + newGestureName + ".xml";
            GestureIO.WriteGesture(pointArray, newGestureName, fileName);
        }
        else
        {
            Result result = PointCloudRecognizer.Classify(newGesture, trainingSet.ToArray());
            Debug.Log(result.GestureClass + result.Score);
            if (result.Score > recognitionThreshold)
            {
                OnRecognized.Invoke(result.GestureClass);
            }
        }
    }

    void UpdateMovement()
    {
        Debug.Log("Update movement");
        Vector3 lastPosition = positionsList[positionsList.Count - 1];
        //레이인터랙터 포지션값을 저장했던 리스트의 젤 마지막 값을 lastPosition 벡터값에 저장

        //(레이인터랙터의 현재 포지션)~(리스트 제일 마지막 포지션)벡터 거리가 0.005보다 길 경우
        if (Vector3.Distance(movementSource.transform.position, lastPosition) > newPositionThresholdDistance) 
        {
            positionsList.Add(movementSource.transform.position); //현재 포지션 저장

            if (HelpmeDragon)
            {
                Destroy(Instantiate(HelpmeDragon, movementSource.transform.position, Quaternion.identity), 3);
            }
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
        GameObject a;
        switch (wand)
        {
            case "earth":
                return XR.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject;

            case "fire":
                return XR.transform.GetChild(4).gameObject.transform.GetChild(0).gameObject;

            case "water":
                return XR.transform.GetChild(5).gameObject.transform.GetChild(0).gameObject;

            case "wind":
                return XR.transform.GetChild(6).gameObject.transform.GetChild(0).gameObject;

            case "electric":
                return XR.transform.GetChild(7).gameObject.transform.GetChild(0).gameObject;

            case "dark":
                return XR.transform.GetChild(8).gameObject.transform.GetChild(0).gameObject;

            case "light":
                return XR.transform.GetChild(9).gameObject.transform.GetChild(0).gameObject;

            case "illusion":
                return XR.transform.GetChild(10).gameObject.transform.GetChild(0).gameObject;

            default:
                return null;

        }
    }

}
