using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestGetCornersToo : MonoBehaviour {
    [SerializeField] private int start;
    [SerializeField] private int roll;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            int tempSpace = start;
            int remaining = roll;
            List<int> indices = new List<int>();
            void updateValues(int added) {
                indices.Add(tempSpace + added);
                tempSpace += added;
                remaining -= added;
            }

            int toTen = GameConstants.SPACES_ON_EDGE - (start % GameConstants.SPACES_ON_EDGE);
            if (toTen <= remaining) updateValues(toTen);
            while (remaining >= GameConstants.SPACES_ON_EDGE) updateValues(GameConstants.SPACES_ON_EDGE);
            if (remaining > 0) updateValues(remaining);
            foreach (int x in indices.Select(x => x % 40)) {
                print(x);
            }
        }
    }
}
