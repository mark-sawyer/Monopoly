using System.Collections.Generic;
using UnityEngine;

public class ScreenOverlayManager : MonoBehaviour {
    #region Screen overlay prefabs
    [SerializeField] private GameObject numberOfPlayers;
    [SerializeField] private GameObject tokenSelection;
    [SerializeField] private GameObject incomeTaxPrefab;
    [SerializeField] private GameObject purchaseQuestionPrefab;
    [SerializeField] private GameObject spinningPolicemanPrefab;
    [SerializeField] private GameObject cardFlipperPrefab;
    [SerializeField] private GameObject debtorCreditor;
    [SerializeField] private GameObject luxuryTax;
    [SerializeField] private GameObject unaffordableProperty;
    [SerializeField] private GameObject tradingCharacterSelection;
    [SerializeField] private GameObject resolveDebtPanel;
    [SerializeField] private GameObject auctionManager;
    [SerializeField] private GameObject resolveMortgage;
    [SerializeField] private GameObject winnerAnnouncement;
    #endregion
    #region Private attributes
    [SerializeField] private GameObject[] chancePrefabs;
    [SerializeField] private GameObject[] communityChestPrefabs;
    private GameObject screenOverlayInstance;
    private Dictionary<int, GameObject> chanceIDToPrefabDictionary = new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> communityChestIDToPrefabDictionary = new Dictionary<int, GameObject>();
    #endregion



    #region MonoBehaviour
    private void Start() {
        ScreenOverlayEventHub events = ScreenOverlayEventHub.Instance;
        initialiseChanceDictionary();
        initialiseCommunityChestyDictionary();
        float alpha = InterfaceConstants.SCREEN_ANIMATION_COVER_ALPHA;

        events.sub_RemoveScreenOverlay(removeScreenOverlay);
        events.sub_RemoveScreenOverlayKeepCover(removeOverlayKeepCover);

        events.sub_PlayerNumberSelection(() => startScreenOverlay(numberOfPlayers, alpha));
        events.sub_PlayerNumberConfirmed((int players) => startScreenOverlay(tokenSelection, players, alpha));
        events.sub_SpinningPoliceman(() => startScreenOverlay(spinningPolicemanPrefab, alpha));
        events.sub_IncomeTaxQuestion((PlayerInfo playerInfo) => startScreenOverlay(incomeTaxPrefab, playerInfo, alpha));
        events.sub_PurchaseQuestion((PlayerInfo playerInfo, PropertyInfo propertyInfo) => {
            startScreenOverlay(purchaseQuestionPrefab, playerInfo, propertyInfo, alpha);
        });
        events.sub_CardShown(() => {
            CardInfo cardInfo = GameState.game.DrawnCard;
            GameObject cardPrefab;
            if (cardInfo.CardType == CardType.CHANCE) cardPrefab = chanceIDToPrefabDictionary[cardInfo.ID];
            else cardPrefab = communityChestIDToPrefabDictionary[cardInfo.ID];
            startScreenOverlay(cardFlipperPrefab, cardPrefab, alpha);
        });
        events.sub_PayingRentAnimationBegins((DebtInfo debtInfo) => startScreenOverlay(debtorCreditor, debtInfo, alpha));
        events.sub_LuxuryTaxAnimationBegins(() => startScreenOverlay(luxuryTax, alpha));
        events.sub_UnaffordableProperty((PropertyInfo propertyInfo) => startScreenOverlay(unaffordableProperty, propertyInfo, alpha));
        events.sub_TradeOpened(() => startScreenOverlay(tradingCharacterSelection, alpha));
        events.sub_ResolveDebt((DebtInfo debtInfo) => startScreenOverlay(resolveDebtPanel, debtInfo, 1));
        events.sub_AuctionsBegin((Queue<PropertyInfo> propertyInfos) => startScreenOverlay(auctionManager, propertyInfos, 1));
        events.sub_ResolveMortgage((PlayerInfo pl, PropertyInfo pr) => startScreenOverlay(resolveMortgage, pl, pr, alpha));
        events.sub_WinnerAnnounced((PlayerInfo winner) => startScreenOverlay(winnerAnnouncement, winner, alpha));
    }
    #endregion


    
    #region Screen overlays
    private void startScreenOverlay(GameObject prefab, float coverAlpha) {
        UIEventHub.Instance.call_FadeScreenCoverIn(coverAlpha);
        screenOverlayInstance = Instantiate(prefab, transform);
        screenOverlayInstance.GetComponent<ScreenOverlay>().appear();
    }
    private void startScreenOverlay<T>(GameObject prefab, T t, float coverAlpha) {
        UIEventHub.Instance.call_FadeScreenCoverIn(coverAlpha);
        screenOverlayInstance = Instantiate(prefab, transform);
        screenOverlayInstance.GetComponent<ScreenOverlay<T>>().setup(t);
        screenOverlayInstance.GetComponent<ScreenOverlay<T>>().appear();
    }
    private void startScreenOverlay<T1, T2>(GameObject prefab, T1 t1, T2 t2, float coverAlpha) {
        UIEventHub.Instance.call_FadeScreenCoverIn(coverAlpha);
        screenOverlayInstance = Instantiate(prefab, transform);
        screenOverlayInstance.GetComponent<ScreenOverlay<T1, T2>>().setup(t1, t2);
        screenOverlayInstance.GetComponent<ScreenOverlay<T1, T2>>().appear();
    }
    private void removeScreenOverlay() {
        Destroy(screenOverlayInstance);
        screenOverlayInstance = null;
        UIEventHub.Instance.call_FadeScreenCoverOut();
    }
    private void removeOverlayKeepCover() {
        Destroy(screenOverlayInstance);
        screenOverlayInstance = null;
    }
    #endregion



    #region private
    private void initialiseChanceDictionary() {
        chanceIDToPrefabDictionary.Add(1, chancePrefabs[0]);
        chanceIDToPrefabDictionary.Add(2, chancePrefabs[1]);
        chanceIDToPrefabDictionary.Add(3, chancePrefabs[2]);
        chanceIDToPrefabDictionary.Add(4, chancePrefabs[3]);
        chanceIDToPrefabDictionary.Add(5, chancePrefabs[4]);
        chanceIDToPrefabDictionary.Add(6, chancePrefabs[5]);
        chanceIDToPrefabDictionary.Add(7, chancePrefabs[6]);
        chanceIDToPrefabDictionary.Add(8, chancePrefabs[7]);
        chanceIDToPrefabDictionary.Add(9, chancePrefabs[8]);
        chanceIDToPrefabDictionary.Add(10, chancePrefabs[9]);
        chanceIDToPrefabDictionary.Add(11, chancePrefabs[9]);
        chanceIDToPrefabDictionary.Add(12, chancePrefabs[10]);
        chanceIDToPrefabDictionary.Add(13, chancePrefabs[11]);
        chanceIDToPrefabDictionary.Add(14, chancePrefabs[12]);
        chanceIDToPrefabDictionary.Add(15, chancePrefabs[13]);
        chanceIDToPrefabDictionary.Add(16, chancePrefabs[14]);
    }
    private void initialiseCommunityChestyDictionary() {
        communityChestIDToPrefabDictionary.Add(1, communityChestPrefabs[0]);
        communityChestIDToPrefabDictionary.Add(2, communityChestPrefabs[1]);
        communityChestIDToPrefabDictionary.Add(3, communityChestPrefabs[2]);
        communityChestIDToPrefabDictionary.Add(4, communityChestPrefabs[3]);
        communityChestIDToPrefabDictionary.Add(5, communityChestPrefabs[4]);
        communityChestIDToPrefabDictionary.Add(6, communityChestPrefabs[5]);
        communityChestIDToPrefabDictionary.Add(7, communityChestPrefabs[6]);
        communityChestIDToPrefabDictionary.Add(8, communityChestPrefabs[7]);
        communityChestIDToPrefabDictionary.Add(9, communityChestPrefabs[8]);
        communityChestIDToPrefabDictionary.Add(10, communityChestPrefabs[9]);
        communityChestIDToPrefabDictionary.Add(11, communityChestPrefabs[10]);
        communityChestIDToPrefabDictionary.Add(12, communityChestPrefabs[11]);
        communityChestIDToPrefabDictionary.Add(13, communityChestPrefabs[12]);
        communityChestIDToPrefabDictionary.Add(14, communityChestPrefabs[13]);
        communityChestIDToPrefabDictionary.Add(15, communityChestPrefabs[14]);
        communityChestIDToPrefabDictionary.Add(16, communityChestPrefabs[15]);
    }
    #endregion
}
