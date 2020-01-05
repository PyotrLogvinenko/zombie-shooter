using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillsPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponent<EndGame>().HandleWin();
        }
    }
}
