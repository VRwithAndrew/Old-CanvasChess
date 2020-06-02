using UnityEngine;
using UnityEngine.UI;

public class King : BasePiece
{
    private Rook mLeftRook = null;
    private Rook mRightRook = null;

    public override void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
    {
        // Base setup
        base.Setup(newTeamColor, newSpriteColor, newPieceManager);

        // King stuff
        mMovement = new Vector3Int(1, 1, 1);
        GetComponent<Image>().sprite = Resources.Load<Sprite>("T_King");
    }

    public override void Kill()
    {
        base.Kill();

        mPieceManager.mIsKingAlive = false;
    }

    protected override void CheckPathing()
    {
        // Normal pathing
        base.CheckPathing();

        // Right
        mRightRook = GetRook(1, 3);

        // Left
        mLeftRook = GetRook(-1, 4);
    }

    protected override void Move()
    {
        // Base move
        base.Move();

        // Left rook
        if (CanCastle(mLeftRook))
            mLeftRook.Castle();

        // Right rook
        if (CanCastle(mRightRook))
            mRightRook.Castle();
    }

    private bool CanCastle(Rook rook)
    {
        // For rook
        if (rook == null)
            return false;

        // Do the cells match?
        if (rook.mCastleTriggerCell != mCurrentCell)
            return false;

        // Check if same team, and hasn't moved
        if (rook.mColor != mColor || !rook.mIsFirstMove)
            return false;

        return true;
    }

    private Rook GetRook(int direction, int count)
    {
        // Has the king moved?
        if (!mIsFirstMove)
            return null;

        // Numbers and stuff
        int currentX = mCurrentCell.mBoardPosition.x;
        int currentY = mCurrentCell.mBoardPosition.y;

        // Go through the cells
        for (int i = 1; i < count; i++)
        {
            int offsetX = currentX + (i * direction);
            CellState cellState = mCurrentCell.mBoard.ValidateCell(offsetX, currentY, this);

            if (cellState != CellState.Free)
                return null;
        }

        // Try and get rook
        Cell rookCell = mCurrentCell.mBoard.mAllCells[currentX + (count * direction), currentY];
        Rook rook = null;

        // Check for cast
        if (rookCell.mCurrentPiece is Rook)
            rook = (Rook)rookCell.mCurrentPiece;

        // Add target cell to highlighed cells
        if (rook != null)
            mHighlightedCells.Add(rook.mCastleTriggerCell);

        return rook;
    }
}