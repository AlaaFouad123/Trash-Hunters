using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class LoadHome : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadSceneAfterDelay("Home")); 
    }
    IEnumerator LoadSceneAfterDelay(string sceneName)
    {
        yield return new WaitForSeconds(8f);
        SceneManager.LoadScene(sceneName);
    }
}
