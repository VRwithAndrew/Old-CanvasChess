using UnityEngine;
using UnityEngine.UI;

public class Rook : BasePiece
{
    [HideInInspector]
    public Cell mCastleTriggerCell = null;
    private Cell mCastleCell = null;

    public override void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
    {
        // Base setup
        base.Setup(newTeamColor, newSpriteColor, newPieceManager);

        // Rook stuff
        mMovement = new Vector3Int(7, 7, 0);
        GetComponent<Image>().sprite = Resources.Load<Sprite>("T_Rook");
    }

    public override void Place(Cell newCell)
    {
        // After being placed, set castle, need current cell
        base.Place(newCell);

        // Trigger cell
        int triggerOffset = mCurrentCell.mBoardPosition.x < 4 ? 2 : -1;
        mCastleTriggerCell = SetCell(triggerOffset);

        // Castle cell
        int castleOffset = mCurrentCell.mBoardPosition.x < 4 ? 3 : -2;
        mCastleCell = SetCell(castleOffset);
    }

    public void Castle()
    {
        // Set new target
        mTargetCell = mCastleCell;

        // Actually move
        Move();
    }

    private Cell SetCell(int offset)
    {
        // New position
        Vector2Int newPosition = mCurrentCell.mBoardPosition;
        newPosition.x += offset;

        // Return
        return mCurrentCell.mBoard.mAllCells[newPosition.x, newPosition.y];
    }
}
