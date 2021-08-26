using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    int currentIndexScene;
    [SerializeField] float TimeToWait = 2f;
    [SerializeField] float LevelExitSlowMotion = 0.2f;

    void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        Time.timeScale = LevelExitSlowMotion;
        yield return new WaitForSeconds(TimeToWait);
        Time.timeScale = 1f;
        currentIndexScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndexScene + 1);
    }
}
