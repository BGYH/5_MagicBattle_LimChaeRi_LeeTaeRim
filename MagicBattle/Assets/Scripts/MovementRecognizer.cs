using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using PDollarGestureRecognizer;
using System.IO;
using UnityEngine.Events;

public class MovementRecognizer : MonoBehaviour
{
    public XRNode inputSource;
    public InputHelpers.Button inputButton;
    public float inputThreshold = 0.005f;
    public Transform movementSource;

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

        //movement ����
        if(!isMoving && isPressed)
        {
            StartMovement();
        }
        else if (isMoving && !isPressed) //movement ��
        {
            EndMovement();
        }
        else if (isMoving && isPressed) //movement�� ������Ʈ
        {
            UpdateMovement();
        }
    }

    void StartMovement()
    {
        Debug.Log("Start movement");
        isMoving = true;
        positionsList.Clear();
        positionsList.Add(movementSource.position); //�������ͷ��Ͱ� �����̴� position���� ����Ʈ�� ����

        if(HelpmeDragon)
        {
            //Instantiate�Լ��� ��ƼŬ ������Ʈ�� �������ͷ����� ��ġ�� ����, 7�� �� ����
            Destroy(Instantiate(HelpmeDragon, movementSource.position, Quaternion.identity), 5);
        }
    }

    void EndMovement()
    {
        Debug.Log("End movement");
        isMoving = false;

        //posiont list�κ��� ����ó�� �����.
        Point[] pointArray = new Point[positionsList.Count];

        for (int i = 0; i < positionsList.Count; i++)
        {
            Vector2 ScreenPoint = Camera.main.WorldToScreenPoint(positionsList[i]);
            pointArray[i] = new Point(ScreenPoint.x, ScreenPoint.y, 0);
        }

        Gesture newGesture = new Gesture(pointArray);

        //trainingSet�� ���ο� ����ó �����ϱ�
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
        //�������ͷ��� �����ǰ��� �����ߴ� ����Ʈ�� �� ������ ���� lastPosition ���Ͱ��� ����

        //(�������ͷ����� ���� ������)~(����Ʈ ���� ������ ������)���� �Ÿ��� 0.005���� �� ���
        if (Vector3.Distance(movementSource.position, lastPosition) > newPositionThresholdDistance) 
        {
            positionsList.Add(movementSource.position); //���� ������ ����

            if (HelpmeDragon)
            {
                Destroy(Instantiate(HelpmeDragon, movementSource.position, Quaternion.identity), 3);
            }
        }
        
    }
}
