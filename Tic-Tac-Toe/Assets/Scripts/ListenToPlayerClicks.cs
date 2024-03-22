using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenToPlayerClicks : MonoBehaviour
{
    [SerializeField] GameObject playerIcon;

    private void OnMouseDown()
    {
        Instantiate(playerIcon, transform.position, transform.rotation);
    }
}
