using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader {
    public static void LoadScene(Scene scene) {
        SceneManager.LoadScene(scene.ToString());
    }

    public static void ExitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stops play mode in Unity
#endif

        Application.Quit(); //Only works when the game is live
    }
}
