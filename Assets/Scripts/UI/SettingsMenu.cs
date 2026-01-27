using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.SearchService;

public class SettingsMenu : MonoBehaviour {
    public TextMeshProUGUI gameSpeedText;
    public GameObject musicPlayerMute;
    public GameObject[] musicPlayerBars;
    public Sprite audioIcon;
    public Sprite audioIconMute;
    public Settings settings;

    void Start() {
        gameSpeedText.text = $"{Settings.gameSpeed}";
        UpdateMusicVolume(Settings.musicVolumePercentage);
    }

    public void ToggleSettingsEnabled() {
        if (gameObject.activeSelf) {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
        }
    }

    public void IncreaseGameSpeed() {
        if (Settings.gameSpeed >= 4) return;
        Settings.gameSpeed *= 2;
        gameSpeedText.text = $"{Settings.gameSpeed}";
    }

    public void DecreaseGameSpeed() {
        if (Settings.gameSpeed <= 1) return;
        Settings.gameSpeed /= 2;
        gameSpeedText.text = $"{Settings.gameSpeed}";
    }

    public void UpdateMusicVolume(int volumePercentage) {
        settings.UpdateMusicVolume(volumePercentage);

        if (volumePercentage > 0) {
            musicPlayerMute.GetComponent<Image>().sprite = audioIcon;
        } else {
            musicPlayerMute.GetComponent<Image>().sprite = audioIconMute;
        }

        for (int i = 0; i < 5; i++) {
            if (volumePercentage >= (i * 20) + 20) {
                musicPlayerBars[i].GetComponent<Image>().color = ColorPalette.GetColor(ColorEnum.Black);
            } else {
                musicPlayerBars[i].GetComponent<Image>().color = ColorPalette.GetColor(ColorEnum.Gray);
            }
        }
    }

    public void GoToMainMenu() {
        SceneLoader.LoadScene(Scene.MainMenu);
    }
}