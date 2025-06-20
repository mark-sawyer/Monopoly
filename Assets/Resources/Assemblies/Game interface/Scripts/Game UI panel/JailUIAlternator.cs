using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JailUIAlternator : MonoBehaviour {
    [SerializeField] private GameObject doublesGameObject;
    [SerializeField] private GameObject jailOptionsGameObject;
    [SerializeField] private GameEvent jailTurnBeginUI;
    [SerializeField] private GameEvent regularTurnBegin;



    #region MonoBehaviour
    private void Start() {
        regularTurnBegin.Listeners += () => setMode(true);
        jailTurnBeginUI.Listeners += () => setMode(false);
    }
    #endregion



    #region private
    private void setMode(bool isRegular) {
        doublesGameObject.SetActive(isRegular);
        jailOptionsGameObject.SetActive(!isRegular);
    }
    #endregion
}
