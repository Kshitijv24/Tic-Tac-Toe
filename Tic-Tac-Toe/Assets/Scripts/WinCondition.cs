using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    [SerializeField] GameObject xWonPopUp;
    [SerializeField] List<GridBlock> blockList;
    bool gameWon = false;

    private void Update()
    {
        if (blockList == null) return;
        if (gameWon) return;

        HandleWinCondition();
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

    private void ShowWinPopUp()
    {
        if (gameWon)
        {
            Instantiate(xWonPopUp, xWonPopUp.transform.position, Quaternion.identity);
        }
    }
}
