using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    [HideInInspector] public bool gameWon = false;
    [HideInInspector] public bool gameLose = false;

    [SerializeField] GameObject xWonPopUp;
    [SerializeField] GameObject oWonPopUp;
    [SerializeField] OpponentAI opponentAI;

    [SerializeField] List<GridBlock> blockList;

    private void Update()
    {
        if (blockList == null) return;

        if (gameWon) return;
        if (gameLose) return;

        HandleWinCondition();
        HandleLoseCondition();
    }

    private void HandleWinCondition()
    {
        if (blockList[0].hasX &&
            blockList[1].hasX &&
            blockList[2].hasX)
        {
            gameWon = true;
            ShowWinPopUp();
        }

        else if (blockList[0].hasX &&
                 blockList[3].hasX &&
                 blockList[6].hasX)
        {
            gameWon = true;
            ShowWinPopUp();
        }

        else if (blockList[0].hasX &&
                 blockList[4].hasX &&
                 blockList[8].hasX)
        {
            gameWon = true;
            ShowWinPopUp();
        }

        else if (blockList[1].hasX &&
                 blockList[4].hasX &&
                 blockList[7].hasX)
        {
            gameWon = true;
            ShowWinPopUp();
        }

        else if (blockList[2].hasX &&
                 blockList[5].hasX &&
                 blockList[8].hasX)
        {
            gameWon = true;
            ShowWinPopUp();
        }

        else if (blockList[3].hasX &&
                 blockList[4].hasX &&
                 blockList[5].hasX)
        {
            gameWon = true;
            ShowWinPopUp();
        }

        else if (blockList[6].hasX &&
                 blockList[7].hasX &&
                 blockList[8].hasX)
        {
            gameWon = true;
            ShowWinPopUp();
        }

        else if (blockList[2].hasX &&
                 blockList[4].hasX &&
                 blockList[6].hasX)
        {
            gameWon = true;
            ShowWinPopUp();
        }
    }

    private void HandleLoseCondition()
    {
        if (blockList[0].hasO &&
            blockList[1].hasO &&
            blockList[2].hasO)
        {
            gameLose = true;
            ShowLosePopUp();
        }

        else if (blockList[0].hasO &&
                 blockList[3].hasO &&
                 blockList[6].hasO)
        {
            gameLose = true;
            ShowLosePopUp();
        }

        else if (blockList[0].hasO &&
                 blockList[4].hasO &&
                 blockList[8].hasO)
        {
            gameLose = true;
            ShowLosePopUp();
        }

        else if (blockList[1].hasO &&
                 blockList[4].hasO &&
                 blockList[7].hasO)
        {
            gameLose = true;
            ShowLosePopUp();
        }

        else if (blockList[2].hasO &&
                 blockList[5].hasO &&
                 blockList[8].hasO)
        {
            gameLose = true;
            ShowLosePopUp();
        }

        else if (blockList[3].hasO &&
                 blockList[4].hasO &&
                 blockList[5].hasO)
        {
            gameLose = true;
            ShowLosePopUp();
        }

        else if (blockList[6].hasO &&
                 blockList[7].hasO &&
                 blockList[8].hasO)
        {
            gameLose = true;
            ShowLosePopUp();
        }

        else if (blockList[2].hasO &&
                 blockList[4].hasO &&
                 blockList[6].hasO)
        {
            gameLose = true;
            ShowLosePopUp();
        }
    }

    private void ShowWinPopUp()
    {
        if (gameWon)
        {
            StopPlayerMovesAfterWinOrLose();
            StopOpponentAIMovesAfterWinOrLose();

            AudioManager.Instance.PlayWinSound(0.4f);
            Instantiate(xWonPopUp, xWonPopUp.transform.position, Quaternion.identity);
        }
    }

    private void ShowLosePopUp()
    {
        if(gameLose)
        {
            StopPlayerMovesAfterWinOrLose();
            StopOpponentAIMovesAfterWinOrLose();

            AudioManager.Instance.PlayLoseSound(0.4f);
            Instantiate(oWonPopUp, oWonPopUp.transform.position, Quaternion.identity);
        }
    }

    private void StopPlayerMovesAfterWinOrLose()
    {
        if (gameWon || gameLose)
        {
            foreach (GridBlock gridBlock in GridArea.Instance.allGridBlock)
            {
                gridBlock.gameObject.SetActive(false);
            }
        }
    }

    private void StopOpponentAIMovesAfterWinOrLose()
    {
        if (gameWon || gameLose)
        {
            opponentAI.gameObject.SetActive(false);
        }
    }
}
