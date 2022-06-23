using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public static int sceneToLoad = 0;
    public static int loadScreenIndex = -1;//set this to the index of the loading screen this should never change.
    //index is -1 because there is no loading screen as of yet. 

    public static void setIndex(int index)
    {
        sceneToLoad = index;
    }

    public static void loadSceneImmediate()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public static void loadSceneImmediate(int index)
    {
        sceneToLoad = index;
        SceneManager.LoadScene(sceneToLoad);
    }

    public void loadFromLoadingScreen(int index)
    {
        sceneToLoad = index;
        SceneManager.LoadScene(loadScreenIndex);
    }

    public void loadFromLoadingScreen()
    {
        SceneManager.LoadScene(loadScreenIndex);
    }
}
