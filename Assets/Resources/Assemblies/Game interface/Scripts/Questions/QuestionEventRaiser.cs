using UnityEngine;

public class QuestionEventRaiser : ScriptableObject {
    [SerializeField] private NoDataEvent fadeInEvent;
    [SerializeField] private PlayerEvent incomeTaxQuestion;
    [SerializeField] private PlayerPropertyEvent purchaseQuestion;
    [SerializeField] private PlayerPropertyEvent unmortgageQuestion;

    public void invokePurchaseQuestion(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        fadeInEvent.raiseEvent();
        purchaseQuestion.raiseEvent(playerInfo, propertyInfo);
    }
    public void invokeUnmortgageQuestion(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        fadeInEvent.raiseEvent();
        unmortgageQuestion.raiseEvent(playerInfo, propertyInfo);
    }
    public void invokeIncomeTaxQuestion(PlayerInfo playerInfo) {
        fadeInEvent.raiseEvent();
        incomeTaxQuestion.raiseEvent(playerInfo);
    }
}
