using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JailUIAlternator : MonoBehaviour {
    [SerializeField] private GameObject doublesGameObject;
    [SerializeField] private GameObject jailOptionsGameObject;



    #region MonoBehaviour
    private void Start() {
        UIEventHub uiEvents = UIEventHub.Instance;
        uiEvents.sub_PreRollStateStarting(setDoublesMode);
        uiEvents.sub_JailPreRollStateStarting(setJailMode);
    }
    #endregion



    #region private
    private void setDoublesMode() {
        doublesGameObject.SetActive(true);
        jailOptionsGameObject.SetActive(false);
    }
    private void setJailMode() {
        doublesGameObject.SetActive(false);
        jailOptionsGameObject.SetActive(true);
    }
    #endregion
}
