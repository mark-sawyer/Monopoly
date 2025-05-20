using UnityEngine;

public class QuestionEventRaiser : ScriptableObject {
    [SerializeField] private GameEvent questionAsked;
    [SerializeField] private PlayerEvent incomeTaxQuestion;
    [SerializeField] private PlayerPropertyEvent purchaseQuestion;
    [SerializeField] private PlayerPropertyEvent unmortgageQuestion;

    public void invokePurchaseQuestion(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        questionAsked.invoke();
        purchaseQuestion.invoke(playerInfo, propertyInfo);
    }
    public void invokeUnmortgageQuestion(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        questionAsked.invoke();
        unmortgageQuestion.invoke(playerInfo, propertyInfo);
    }
    public void invokeIncomeTaxQuestion(PlayerInfo playerInfo) {
        questionAsked.invoke();
        incomeTaxQuestion.invoke(playerInfo);
    }
}
