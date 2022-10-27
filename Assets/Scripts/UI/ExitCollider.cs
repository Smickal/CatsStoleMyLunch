using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ExitCollider : MonoBehaviour
{
    // Start is called before the first frame update
    SceneManagement sceneManagement;

    [SerializeField] GameObject textPopUp;
    [SerializeField] float timeToDestroyText = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(!FindObjectOfType<CountEnemy>().CheckForEnemyCount())
            {
                GameObject textPop = Instantiate(textPopUp);
                Destroy(textPop, timeToDestroyText);
                return;
            }

            sceneManagement = FindObjectOfType<SceneManagement>();
            //sceneManagement.LoadNextScene();
            int MaxScene = SceneManager.sceneCountInBuildSettings;
            int currentScene = SceneManager.GetActiveScene().buildIndex;

            Debug.Log(currentScene + " " + MaxScene);
            if (currentScene == MaxScene - 1)
                sceneManagement.ReturnToMainMenu();
            else
                sceneManagement.LoadNextScene();
        }
    }

}
