using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenToPlayerClicks : MonoBehaviour
{
    [SerializeField] GameObject playerIcon;
    [SerializeField] GameObject playerMoveIcons;

    bool isEmpty = true;

    private void OnMouseDown()
    {
        if (isEmpty)
        {
            Instantiate(playerIcon, transform.position, transform.rotation, playerMoveIcons.transform);
            isEmpty = false;
        }
    }
}
