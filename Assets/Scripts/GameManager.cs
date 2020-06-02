using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public Board mBoard;
    public PieceManager mPieceManager;

    void Start()
    {
        // Create the board
        mBoard.Create();

        // Create pieces
        mPieceManager.Setup(mBoard);
    }
}
