using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OwnershipTag : MonoBehaviour {
    [SerializeField] private ScriptableObject propertySO;



    #region MonoBehaviour
    private void Start() {
        setupAppearance();
    }
    #endregion



    #region protected
    protected ScriptableObject PropertySO => propertySO;
    protected abstract void setupAppearance();
    #endregion
}
