using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckPos : MonoBehaviour
{
    public GameObject player;
    public CharacterController controller;
    public float targetPos;

    private void Start()
    {
        controller = player.gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        targetPos = controller.height / 2;

        transform.position = new Vector3(controller.transform.position.x, controller.transform.position.y - targetPos, controller.transform.position.z);
    }
}
