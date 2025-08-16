using System;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/Hubs/AuctionEventHub")]
public class AuctionEventHub : ScriptableObject {
    private static AuctionEventHub instance;
    [SerializeField] private PlayerIntEvent bidMade;
    [SerializeField] private GameEvent auctionFinished;
    [SerializeField] private GameEvent allAuctionsFinished;
    [SerializeField] private GameEvent auctionBuildingsBackButtonClicked;
    [SerializeField] private GameEvent auctionRemainingBuildingsButtonClicked;



    #region public
    public static AuctionEventHub Instance {
        get {
            if (instance == null) {
                instance = Resources.Load<AuctionEventHub>(
                    "ScriptableObjects/Events/0. Hubs/auction_event_hub"
                );
            }
            return instance;
        }
    }
    #endregion



    #region Invoking
    public void call_BidMade(PlayerInfo playerInfo, int bid) => bidMade.invoke(playerInfo, bid);
    public void call_AuctionFinished() => auctionFinished.invoke();
    public void call_AllAuctionsFinished() => allAuctionsFinished.invoke();
    public void call_AuctionBuildingsBackButtonClicked() => auctionBuildingsBackButtonClicked.invoke();
    public void call_AuctionRemainingBuildingsButtonClicked() => auctionRemainingBuildingsButtonClicked.invoke();
    #endregion



    #region Subscribing
    public void sub_BidMade(Action<PlayerInfo, int> a) => bidMade.Listeners += a;
    public void sub_AuctionFinished(Action a) => auctionFinished.Listeners += a;
    public void sub_AllAuctionsFinished(Action a) => allAuctionsFinished.Listeners += a;
    public void sub_AuctionBuildingsBackButtonClicked(Action a) => auctionBuildingsBackButtonClicked.Listeners += a;
    public void sub_AuctionRemainingBuildingsButtonClicked(Action a) => auctionRemainingBuildingsButtonClicked.Listeners += a;
    #endregion



    #region Unsubscribing
    public void unsub_AuctionFinished(Action a) => auctionFinished.Listeners -= a;
    public void unsub_AllAuctionsFinished(Action a) => allAuctionsFinished.Listeners -= a;
    public void unsub_BidMade(Action<PlayerInfo, int> a) => bidMade.Listeners -= a;
    public void unsub_AuctionBuildingsBackButtonClicked(Action a) => auctionBuildingsBackButtonClicked.Listeners -= a;
    public void unsub_AuctionRemainingBuildingsButtonClicked(Action a) => auctionRemainingBuildingsButtonClicked.Listeners -= a;
    #endregion
}
