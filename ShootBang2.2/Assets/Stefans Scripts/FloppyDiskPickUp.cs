using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloppyDiskPickUp : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(50 * Time.deltaTime, 50 * Time.deltaTime, -50 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject, 0.2f);
        }
    }
}
