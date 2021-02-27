using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDoor : MonoBehaviour
{

    public GameObject door;
    public GameObject currentPosition;
    public GameObject moveToPos;

    public bool isPlayer = false;

    private void Start()
    {
        currentPosition.transform.localPosition = door.transform.localPosition;
        Debug.Log(currentPosition.transform.localPosition + "Initial Pos");
        moveToPos.transform.localPosition = new Vector3(currentPosition.transform.localPosition.x - 1f, currentPosition.transform.localPosition.y,currentPosition.transform.localPosition.z);
        Debug.Log(moveToPos.transform.localPosition + "Move To Pos");
    }

    private void Update()
    {
        if (isPlayer)
        {
            Open();
        }

        if (!isPlayer)
        {
            Close();
        }
    }

    public void Open()
    {
        door.transform.Translate(moveToPos.transform.localPosition.x, moveToPos.transform.localPosition.y, moveToPos.transform.localPosition.z);
    }

    public void Close()
    {
        door.transform.Translate(currentPosition.transform.localPosition.x, currentPosition.transform.localPosition.y, currentPosition.transform.localPosition.z);
    }

    /*private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detected");
        if (isPlayer)
        {
            Debug.Log("Player detected");
            door.transform.Translate(moveToPos.transform.position * Time.deltaTime);
            Debug.Log("moved to pos");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isPlayer)
        {
            door.transform.Translate(position.transform.position * Time.deltaTime);
        }
    }*/
}
