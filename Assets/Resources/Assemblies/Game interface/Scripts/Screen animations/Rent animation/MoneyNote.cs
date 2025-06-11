using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyNote : MonoBehaviour {
    [SerializeField] private Sprite[] moneySprites;
    [SerializeField] private Image moneyImage;
    private Dictionary<MoneyNoteEnum, Sprite> enumToSprite;

    private void OnEnable() {
        enumToSprite = new Dictionary<MoneyNoteEnum, Sprite>() {
            { MoneyNoteEnum.ONE, moneySprites[0] },
            { MoneyNoteEnum.FIVE, moneySprites[1] },
            { MoneyNoteEnum.TEN, moneySprites[2] },
            { MoneyNoteEnum.TWENTY, moneySprites[3] },
            { MoneyNoteEnum.FIFTY, moneySprites[4] },
            { MoneyNoteEnum.ONE_HUNDRED, moneySprites[5] },
            { MoneyNoteEnum.FIVE_HUNDRED, moneySprites[6] }
        };
    }
    public void setup(MoneyNoteEnum moneyNoteEnum) {
        moneyImage.sprite = enumToSprite[moneyNoteEnum];
    }
}
