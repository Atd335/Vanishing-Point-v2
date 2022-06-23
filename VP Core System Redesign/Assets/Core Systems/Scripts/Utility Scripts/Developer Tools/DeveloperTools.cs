using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeveloperTools : MonoBehaviour
{

    KeyCode devToggle = KeyCode.F1;
    public bool devModeEnabled;
    string stringToEdit;
    string commandHistory;
    Texture2D bg;

    ModeSwitcher switcher;

    public void _Start()
    {
        switcher = UpdateDriver.ud.GetComponent<ModeSwitcher>();

        bg = new Texture2D(1,1);
        bg.SetPixel(0, 0, new Color(0, 0, 0, .8f));
        bg.Apply();
    }
    public void _Update()
    {
        if (Input.GetKeyDown(devToggle)) 
        { 
            devModeEnabled = !devModeEnabled; 
            stringToEdit = "";
        }

        if (developmentTools && !devModeEnabled)
        {
            // create keybind commands here.
            if (Input.GetKeyDown(KeyCode.K))
            {
                switcher.setCheckpoint(new Vector3(-1, -1, -1));
                switcher.respawnAtCheckpoint();
            }
        }

        if (!devModeEnabled) { commandHistory = ""; return; }

        textinputs();
    }

    void textinputs()
    {
        stringToEdit += Input.inputString;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            takeCommand(stringToEdit);
            stringToEdit = "";
        }
        
        if (Input.GetKeyDown(KeyCode.Backspace) && stringToEdit.Length > 0) 
        {
            try{stringToEdit = stringToEdit.Substring(0, stringToEdit.Length - 2);}
            catch (System.Exception){}
        }
        
        if (Input.GetKeyDown(KeyCode.Delete)) { stringToEdit = ""; }
    }

    private void OnGUI()
    {
        if (!devModeEnabled) { return; }
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), bg);

        GUIStyle style = GUI.skin.GetStyle("TextField");
        style.alignment = TextAnchor.MiddleLeft;
        style.fontSize = 25;
        GUI.Box(new Rect(0, Screen.height-40, Screen.width, 40), stringToEdit, style);

        GUIStyle style2 = GUI.skin.GetStyle("Box");
        style2.alignment = TextAnchor.LowerLeft;
        style2.fontSize = 25;
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height-40),commandHistory,style2);
    }

    void takeCommand(string str)
    {
        commandHistory += $"> {str}\n";
        string formSTR = str.ToLower();
        formSTR = formSTR.TrimEnd();

        string initStr = formSTR.Split(' ')[0];

        switch (initStr)
        {
            case "hello":
                commandHistory += "Hello yourself.";
                break;
            case "cls":
                commandHistory ="";
                break;
            case "developer":
                toggleDevMode();
                break;
            case "reset":
                resetScene();
                break;
            case "loadscene":
                loadScene(formSTR.Split(' ')[1]);
                break;
            case "resetbuild":
                resetBuild();
                break;
            default:
                commandHistory += $"Command '{str}' not recognized.";
                break;
        }

        commandHistory += '\n';
        commandHistory += '\n';
    }

    public static bool developmentTools;

    void loadScene(string id)
    {
        try
        {
            int ID = -1;
            ID = int.Parse(id);
            if (SceneManager.GetSceneByBuildIndex(ID).buildIndex == -1) {}
            SceneManager.LoadScene(ID);
            commandHistory += $"Loading Scene {ID}...";
        }
        catch (System.Exception)
        {
            commandHistory += $"No scene with ID '{id}' found.";
        }
    }

    void resetBuild()
    {
        SceneManager.LoadSceneAsync(0);
        commandHistory += $"resetting build...";
    }

    void resetScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        commandHistory += $"resetting scene...";
    }

    void toggleDevMode()
    {
        developmentTools = !developmentTools;
        if (developmentTools) { commandHistory += $"developer tools <color=green>enabled.</color>"; }
        else { commandHistory += $"developer tools <color=red>disabled.</color>"; }
        
    }
}
