using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class MovementRecognizer : MonoBehaviour
{
    public XRNode inputSource;
    public InputHelpers.Button inputButton;
    public float inputThreshold = 0.1f;
    public Transform movementSource;

    public float newPositionThresholdDistance = 0.05f;
    public GameObject HelpmeDragon;

    private bool isMoving = false;
    private List<Vector3> positionsList = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        
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
        positionsList.Add(movementSource.position);

        if(HelpmeDragon)
        {
            Destroy(Instantiate(HelpmeDragon, movementSource.position, Quaternion.identity), 7);
        }
    }

    void EndMovement()
    {
        Debug.Log("End movement");
        isMoving = false;
    }

    void UpdateMovement()
    {
        Debug.Log("Update movement");
        Vector3 lastPosition = positionsList[positionsList.Count - 1];

        if (Vector3.Distance(movementSource.position, lastPosition) > newPositionThresholdDistance) { }
        {
            positionsList.Add(movementSource.position);

            if (HelpmeDragon)
            {
                Destroy(Instantiate(HelpmeDragon, movementSource.position, Quaternion.identity), 3);
            }
        }
        
    }
}
