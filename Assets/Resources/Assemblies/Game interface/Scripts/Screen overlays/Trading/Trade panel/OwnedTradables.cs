using System.Collections.Generic;
using UnityEngine;

public class OwnedTradables : MonoBehaviour {
    [SerializeField] private UnplacedOwnedIcon[] ownedIcons;



    public void setup(PlayerInfo playerInfo) {
        IEnumerable<TradableInfo> tradableInfos = playerInfo.TradableInfos;
        int i = 0;
        foreach (TradableInfo tradableInfo in tradableInfos) {
            UnplacedOwnedIcon ownedIcon = ownedIcons[i];
            GameObject ownedIconGameObject = ownedIcon.gameObject;
            ownedIconGameObject.SetActive(true);
            ownedIcon.setupOwnedIcon(tradableInfo, playerInfo);
            i++;
        }
    }
}
