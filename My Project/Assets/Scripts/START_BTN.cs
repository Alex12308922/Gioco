using UnityEngine;
using UnityEngine.SceneManagement;


public class START_BTN : MonoBehaviour
{
    public string sceneToLoad;
    public void loadscene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
