using UnityEngine;
using UnityEngine.UI;

public class MapConnector : MonoBehaviour {
    Color color;
    float fadeTime = 1f;
    private void Start() {
        color = ColorPalette.AddTransparency(GetComponent<Image>().color, 0);
        GetComponent<Image>().color = color;
    }

    void Update() {
        if (color.a < 1) {
            color = ColorPalette.AddTransparency(color, (color.a * 100) + (Time.deltaTime * 100 / fadeTime));
            GetComponent<Image>().color = color;
        }
    }
}
