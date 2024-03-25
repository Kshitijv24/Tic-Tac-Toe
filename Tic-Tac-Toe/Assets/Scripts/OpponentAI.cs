using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentAI : MonoBehaviour
{
    [SerializeField] GameObject opponentAIIcon;
    [SerializeField] GameObject opponentAIGameObjectHolder;

    private bool isCoroutineRunning = false;

    private void Update()
    {
        if (!isCoroutineRunning)
        {
            StartCoroutine(OpponentAIMove());
        }
    }

    IEnumerator OpponentAIMove()
    {
        isCoroutineRunning = true;

        if (GridArea.Instance.allGridBlock.Count <= 0)
        {
            isCoroutineRunning = false;
            yield break;
        }

        if (!TurnManager.Instance.isPlayerTurn)
        {
            yield return new WaitForSeconds(1f);

            AudioManager.Instance.PlayClickSound(1f);
            int randomGameObject = Random.Range(0, GridArea.Instance.allGridBlock.Count);

            GameObject newIcon = Instantiate(
                opponentAIIcon,
                GridArea.Instance.allGridBlock[randomGameObject].transform.position,
                Quaternion.identity, opponentAIGameObjectHolder.transform);

            GridArea.Instance.allGridBlock[randomGameObject].isEmpty = false;
            GridArea.Instance.allGridBlock[randomGameObject].hasO = true;
            GridArea.Instance.allGridBlock.Remove(GridArea.Instance.allGridBlock[randomGameObject]);
            TurnManager.Instance.ChangeTurn();
        }

        isCoroutineRunning = false;
    }
}
