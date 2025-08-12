using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/PostRaiseMoneyState")]
internal class PostRaiseMoneyState : State {
    private bool updateAnimationsOver;



    #region State
    public override void enterState() {
        PlayerInfo[] getPlayersNeedingUIUpdate() {
            PlayerPanelManager playerPanelManager = PlayerPanelManager.Instance;
            List<PlayerInfo> playersToUpdate = new List<PlayerInfo>();
            for (int i = 0; i < GameState.game.NumberOfPlayers; i++) {
                PlayerInfo playerInfo = GameState.game.getPlayerInfo(i);
                if (!playerInfo.IsActive) continue;

                PlayerPanel playerPanel = playerPanelManager.getPlayerPanel(i);
                if (!playerPanel.NeedsMoneyUpdate) continue;

                playersToUpdate.Add(playerInfo);
            }
            return playersToUpdate.ToArray();
        }


        updateAnimationsOver = false;
        UIEventHub.Instance.sub_AllExpiredPropertyVisualsUpdated(updateAnimationsOverListener);

        PlayerInfo[] playersNeedingUIUpdate = getPlayersNeedingUIUpdate();
        if (playersNeedingUIUpdate.Length > 0) {
            UIEventHub.Instance.call_UpdateUIMoney(playersNeedingUIUpdate);
            WaitFrames.Instance.beforeAction(
                FrameConstants.MONEY_UPDATE,
                () => UIEventHub.Instance.call_UpdateExpiredPropertyVisuals()
            );
        }
        else {
            UIEventHub.Instance.call_UpdateExpiredPropertyVisuals();
        }
    }
    public override bool exitConditionMet() {
        return updateAnimationsOver;
    }
    public override void exitState() {
        UIEventHub.Instance.unsub_AllExpiredPropertyVisualsUpdated(updateAnimationsOverListener);
    }
    public override State getNextState() {
        return allStates.getState<ResolveDebtState>();
    }
    #endregion



    #region private
    private void updateAnimationsOverListener() {
        WaitFrames.Instance.beforeAction(
            50,
            () => updateAnimationsOver = true
        );
    }
    #endregion
}
