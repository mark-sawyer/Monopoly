using UnityEngine;

public class RentAnimation : MonoBehaviour {
    [SerializeField] private ScreenCover screenCover;
    [SerializeField] private DebtEvent payingRentAnimationBegins;
    [SerializeField] private GameEvent rentAnimationOver;
    [SerializeField] private GameObject debtorCreditorPrefab;
    private GameObject debtorCreditorInstance;

    private void Start() {
        payingRentAnimationBegins.Listeners += startRentAnimation;
        rentAnimationOver.Listeners += endAnimation;
    }
    private void startRentAnimation(DebtInfo debt) {
        screenCover.startFadeIn();
        debtorCreditorInstance = Instantiate(debtorCreditorPrefab, transform);
        DebtorCreditor debtorCreditor = debtorCreditorInstance.GetComponent<DebtorCreditor>();
        debtorCreditor.setup(debt);
    }
    private void endAnimation() {
        Destroy(debtorCreditorInstance);
        debtorCreditorInstance = null;
        screenCover.startFadeOut();
    }
}
