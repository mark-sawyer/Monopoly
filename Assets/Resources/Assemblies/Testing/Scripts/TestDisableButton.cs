using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDisableButton : MonoBehaviour {
    [SerializeField] TestButton testButton;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            testButton.interactable = !testButton.interactable;
        }
    }

    public void printPoop() {
        print("poop");
    }
}
