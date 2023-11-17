using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentCanvas : MonoBehaviour
{
    private Canvas canvas;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        canvas = GetComponent<Canvas>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameOver")
        {
            canvas.enabled = false;
            Destroy(gameObject);
        }
        else
        {
            canvas.enabled = true;
        }
    }
}
