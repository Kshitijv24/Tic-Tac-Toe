using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentAI : MonoBehaviour
{
    [SerializeField] GameObject opponentAIIcon;
    [SerializeField] GameObject opponentAIGameObjectHolder;

    private void Update()
    {
        if (GridArea.Instance.allGridBlock.Count <= 0) return;

        if (!TurnManager.Instance.isPlayerTurn)
        {
            AudioManager.Instance.PlayClickSound(1f);
            int randomGameObject = Random.Range(0, GridArea.Instance.allGridBlock.Count);


            Instantiate(
                opponentAIIcon, 
                GridArea.Instance.allGridBlock[randomGameObject].transform.position, 
                Quaternion.identity, opponentAIGameObjectHolder.transform);

            GridArea.Instance.allGridBlock[randomGameObject].isEmpty = false;
            GridArea.Instance.allGridBlock[randomGameObject].hasO = true;
            GridArea.Instance.allGridBlock.Remove(GridArea.Instance.allGridBlock[randomGameObject]);
            TurnManager.Instance.ChangeTurn();
        }
    }
}
