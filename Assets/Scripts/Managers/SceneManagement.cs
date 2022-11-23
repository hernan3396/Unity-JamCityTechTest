using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
    }
}
