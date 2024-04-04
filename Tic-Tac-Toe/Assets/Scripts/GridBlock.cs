using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBlock : MonoBehaviour
{
    public BlockState currentBlockState = BlockState.Empty;

    [SerializeField] GameObject playerIcon;
    [SerializeField] GameObject playerGameObjectHolder;

    private void OnMouseDown()
    {
        if (currentBlockState == BlockState.Empty && TurnManager.Instance.isPlayerTurn)
        {
            AudioManager.Instance.PlayClickSound(1f);
            Instantiate(playerIcon, transform.position, transform.rotation, playerGameObjectHolder.transform);

            currentBlockState = BlockState.X;
            GridArea.Instance.allGridBlock.Remove(this);
            TurnManager.Instance.ChangeTurn();
        }
    }
}
