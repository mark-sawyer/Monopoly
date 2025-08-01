using System.Collections.Generic;
using UnityEngine;

public class OwnedTradables : MonoBehaviour {
    [SerializeField] private OwnedIcon[] ownedIcons;



    public void setup(IEnumerable<TradableInfo> tradableInfos) {
        int i = 0;
        foreach (TradableInfo tradableInfo in tradableInfos) {
            OwnedIcon ownedIcon = ownedIcons[i];
            GameObject ownedIconGameObject = ownedIcon.gameObject;
            ownedIconGameObject.SetActive(true);
            ownedIcon.setupOwnedIcon(tradableInfo);
            i++;
        }
    }
}
