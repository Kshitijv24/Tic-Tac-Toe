using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBlock : MonoBehaviour
{
    public bool isEmpty = true;

    [SerializeField] GameObject playerIcon;
    [SerializeField] GameObject playerGameObjectHolder;

    private void OnMouseDown()
    {
        if (isEmpty && TurnManager.Instance.isPlayerTurn)
        {
            Instantiate(playerIcon, transform.position, transform.rotation, playerGameObjectHolder.transform);

            isEmpty = false;
            GridArea.Instance.allGridBlock.Remove(this);
            TurnManager.Instance.ChangeTurn();
        }
    }
}
