using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JailUIAlternator : MonoBehaviour {
    [SerializeField] private GameObject doublesGameObject;
    [SerializeField] private GameObject jailOptionsGameObject;



    #region MonoBehaviour
    private void Start() {
        UIEventHub.Instance.sub_TurnBegin((bool turnPlayerInJail) => setMode(turnPlayerInJail));
    }
    #endregion



    #region private
    private void setMode(bool turnPlayerInJail) {
        doublesGameObject.SetActive(!turnPlayerInJail);
        jailOptionsGameObject.SetActive(turnPlayerInJail);
    }
    #endregion
}
