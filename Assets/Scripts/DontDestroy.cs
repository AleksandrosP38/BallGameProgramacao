using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyExceptGameOver : MonoBehaviour
{
    private void Awake()
    {
        if (SceneManager.GetActiveScene().name != "GameOver")
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
