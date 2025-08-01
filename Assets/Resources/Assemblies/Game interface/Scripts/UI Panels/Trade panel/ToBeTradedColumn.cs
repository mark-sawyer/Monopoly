using UnityEngine;

public class ToBeTradedColumn : MonoBehaviour {
    private enum SpaceState {
        RECEIVER,
        FILLED,
        OFF
    }
    [SerializeField] private ToBeTradedSpace[] toBeTradedSpaces;


    #region public
    public void setup(PlayerInfo playerInfo) {
        foreach (ToBeTradedSpace toBeTradedSpace in toBeTradedSpaces) {
            toBeTradedSpace.setup(playerInfo);
        }
    }
    public void shiftIconsUp() {
        int spaces = toBeTradedSpaces.Length;
        for (int i = 0; i < spaces - 1; i++) {
            ToBeTradedSpace spaceOne = toBeTradedSpaces[i];
            ToBeTradedSpace spaceTwo = toBeTradedSpaces[i + 1];
            SpaceState stateOne = getSpaceState(spaceOne);
            SpaceState stateTwo = getSpaceState(spaceTwo);
            handleSpacePairs(stateOne, stateTwo, spaceOne, spaceTwo);
        }
    }
    #endregion



    #region private
    private SpaceState getSpaceState(ToBeTradedSpace space) {
        if (space.ReceiverOn) return SpaceState.RECEIVER;
        else if (space.FilledOn) return SpaceState.FILLED;
        else return SpaceState.OFF;
    }
    private void handleSpacePairs(
        SpaceState stateOne, SpaceState stateTwo,
        ToBeTradedSpace spaceOne, ToBeTradedSpace spaceTwo
    ) {
        if (stateOne == SpaceState.FILLED && stateTwo == SpaceState.FILLED) return;
        if (stateOne == SpaceState.FILLED && stateTwo == SpaceState.RECEIVER) return;
        if (stateOne == SpaceState.RECEIVER && stateTwo == SpaceState.OFF) return;
        if (stateOne == SpaceState.OFF && stateTwo == SpaceState.OFF) return;

        if (stateOne == SpaceState.RECEIVER && stateTwo == SpaceState.FILLED) {
            swapEmptyAndOn(spaceOne, spaceTwo);
        }
        else if (stateOne == SpaceState.RECEIVER && stateTwo == SpaceState.RECEIVER) {
            spaceTwo.turnOff();
        }
        else {
            throw new System.Exception("SpaceState combination shouldn't occur.");
        }            
    }
    private void swapEmptyAndOn(ToBeTradedSpace spaceOne, ToBeTradedSpace spaceTwo) {
        PlacedOwnedIcon placedOwnedIcon = spaceTwo.PlacedOwnedIcon;
        UnplacedOwnedIcon ownedIconSource = placedOwnedIcon.OwnedIconSource;
        spaceTwo.turnOnEmpty();
        spaceOne.turnOnFill();
        spaceOne.changeIcon(ownedIconSource);
    }
    #endregion
}
