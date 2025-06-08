using UnityEngine;

[CreateAssetMenu(fileName = "BackThreeSpacesCard", menuName = "Card/Mechanic/BackThreeSpacesCard")]
internal class BackThreeSpacesCard : CardMechanic, BackThreeSpacesCardInfo {
    internal override void execute() {
        Player player = (Player)Game.TurnPlayer;
        Space space = player.Space;
        int spaceIndex = space.Index;
        int newIndex = Modulus.exe(spaceIndex - 3, GameConstants.TOTAL_SPACES);
        Space newSpace = (Space)Game.getSpaceInfo(newIndex);
        player.changeSpace(newSpace);
    }
}

public interface BackThreeSpacesCardInfo : CardMechanicInfo { }
