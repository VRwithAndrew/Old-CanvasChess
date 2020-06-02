using UnityEngine;
using UnityEngine.UI;

public class Bishop : BasePiece
{
    public override void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
    {
        // Base setup
        base.Setup(newTeamColor, newSpriteColor, newPieceManager);

        // Bishop stuff
        mMovement = new Vector3Int(0, 0, 7);
        GetComponent<Image>().sprite = Resources.Load<Sprite>("T_Bishop");
    }
}
