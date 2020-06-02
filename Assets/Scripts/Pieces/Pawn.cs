using UnityEngine;
using UnityEngine.UI;

public class Pawn : BasePiece
{
    public override void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
    {
        // Base setup
        base.Setup(newTeamColor, newSpriteColor, newPieceManager);

        // Pawn Stuff
        mMovement = mColor == Color.white ? new Vector3Int(0, 1, 1) : new Vector3Int(0, -1, -1);
        GetComponent<Image>().sprite = Resources.Load<Sprite>("T_Pawn");
    }

    protected override void Move()
    {
        base.Move();

        CheckForPromotion();
    }

    private bool MatchesState(int targetX, int targetY, CellState targetState)
    {
        CellState cellState = CellState.None;
        cellState = mCurrentCell.mBoard.ValidateCell(targetX, targetY, this);

        if (cellState == targetState)
        {
            mHighlightedCells.Add(mCurrentCell.mBoard.mAllCells[targetX, targetY]);
            return true;
        }

        return false;
    }

    private void CheckForPromotion()
    {
        // Target position
        int currentX = mCurrentCell.mBoardPosition.x;
        int currentY = mCurrentCell.mBoardPosition.y;

        // Check if pawn has reached the end of the board
        CellState cellState = mCurrentCell.mBoard.ValidateCell(currentX, currentY + mMovement.y, this);

        if (cellState == CellState.OutOfBounds)
        {
            Color spriteColor = GetComponent<Image>().color;
            mPieceManager.PromotePiece(this, mCurrentCell, mColor, spriteColor);
        }
    }

    protected override void CheckPathing()
    {
        // Target position
        int currentX = mCurrentCell.mBoardPosition.x;
        int currentY = mCurrentCell.mBoardPosition.y;

        // Top left
        MatchesState(currentX - mMovement.z, currentY + mMovement.z, CellState.Enemy);

        // Forward
        if (MatchesState(currentX, currentY + mMovement.y, CellState.Free))
        {
            // If the first forward cell is free, and first move, check for next
            if (mIsFirstMove)
            {
                MatchesState(currentX, currentY + (mMovement.y * 2), CellState.Free);
            }
        }

        // Top right
        MatchesState(currentX + mMovement.z, currentY + mMovement.z, CellState.Enemy);
    }
}
