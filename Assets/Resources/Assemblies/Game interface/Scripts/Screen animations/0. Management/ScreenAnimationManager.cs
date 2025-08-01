using System.Collections.Generic;
using UnityEngine;

public class ScreenAnimationManager : MonoBehaviour {
    #region Prefabs
    [SerializeField] private GameObject incomeTaxPrefab;
    [SerializeField] private GameObject purchaseQuestionPrefab;
    [SerializeField] private GameObject spinningPolicemanPrefab;
    [SerializeField] private GameObject cardFlipperPrefab;
    [SerializeField] private GameObject debtorCreditor;
    [SerializeField] private GameObject luxuryTax;
    [SerializeField] private GameObject unaffordableProperty;
    [SerializeField] private GameObject tradingCharacterSelection;
    [SerializeField] private GameObject[] chancePrefabs;
    [SerializeField] private GameObject[] communityChestPrefabs;
    #endregion
    #region Private attributes
    private GameObject screenAnimationInstance;
    private Dictionary<int, GameObject> chanceIDToPrefabDictionary = new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> communityChestIDToPrefabDictionary = new Dictionary<int, GameObject>();
    #endregion



    #region MonoBehaviour
    private void Start() {
        ScreenAnimationEventHub events = ScreenAnimationEventHub.Instance;
        initialiseChanceDictionary();
        initialiseCommunityChestyDictionary();
        events.sub_RemoveScreenAnimation(removeScreenAnimation);
        events.sub_RemoveScreenAnimationKeepCover(removeAnimationKeepCover);

        events.sub_SpinningPoliceman(() => startScreenAnimation(spinningPolicemanPrefab));
        events.sub_IncomeTaxQuestion((PlayerInfo playerInfo) => startScreenAnimation(incomeTaxPrefab, playerInfo));
        events.sub_PurchaseQuestion((PlayerInfo playerInfo, PropertyInfo propertyInfo) => {
            startScreenAnimation(purchaseQuestionPrefab, playerInfo, propertyInfo);
        });
        events.sub_CardShown(() => {
            CardInfo cardInfo = GameState.game.DrawnCard;
            GameObject cardPrefab;
            if (cardInfo.CardType == CardType.CHANCE) cardPrefab = chanceIDToPrefabDictionary[cardInfo.ID];
            else cardPrefab = communityChestIDToPrefabDictionary[cardInfo.ID];
            startScreenAnimation(cardFlipperPrefab, cardPrefab);
        });
        events.sub_PayingRentAnimationBegins((DebtInfo debtInfo) => startScreenAnimation(debtorCreditor, debtInfo));
        events.sub_LuxuryTaxAnimationBegins(() => startScreenAnimation(luxuryTax));
        events.sub_UnaffordableProperty((PropertyInfo propertyInfo) => startScreenAnimation(unaffordableProperty, propertyInfo));
        events.sub_TradeOpened(() => startScreenAnimation(tradingCharacterSelection));
    }
    #endregion


    
    #region Screen animation
    private void startScreenAnimation(GameObject prefab) {
        UIEventHub.Instance.call_FadeScreenCoverIn(InterfaceConstants.SCREEN_ANIMATION_COVER_ALPHA);
        screenAnimationInstance = Instantiate(prefab, transform);
        screenAnimationInstance.GetComponent<ScreenAnimation>().appear();
    }
    private void startScreenAnimation<T>(GameObject prefab, T t) {
        UIEventHub.Instance.call_FadeScreenCoverIn(InterfaceConstants.SCREEN_ANIMATION_COVER_ALPHA);
        screenAnimationInstance = Instantiate(prefab, transform);
        screenAnimationInstance.GetComponent<ScreenAnimation<T>>().setup(t);
        screenAnimationInstance.GetComponent<ScreenAnimation<T>>().appear();
    }
    private void startScreenAnimation<T1, T2>(GameObject prefab, T1 t1, T2 t2) {
        UIEventHub.Instance.call_FadeScreenCoverIn(InterfaceConstants.SCREEN_ANIMATION_COVER_ALPHA);
        screenAnimationInstance = Instantiate(prefab, transform);
        screenAnimationInstance.GetComponent<ScreenAnimation<T1, T2>>().setup(t1, t2);
        screenAnimationInstance.GetComponent<ScreenAnimation<T1, T2>>().appear();
    }
    private void removeScreenAnimation() {
        Destroy(screenAnimationInstance);
        screenAnimationInstance = null;
        UIEventHub.Instance.call_FadeScreenCoverOut();
    }
    private void removeAnimationKeepCover() {
        Destroy(screenAnimationInstance);
        screenAnimationInstance = null;
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
