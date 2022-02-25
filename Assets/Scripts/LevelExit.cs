using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelExit : MonoBehaviour
{
    [SerializeField] float waitTime = 1f;
    IEnumerator NextLevel()
    {
        yield return new WaitForSecondsRealtime(waitTime);

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        FindObjectOfType<ScenePersist>().ResetScenePersist(); 
        SceneManager.LoadScene(nextSceneIndex);


    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag != "Player") return;

        StartCoroutine(NextLevel());

    }

}
