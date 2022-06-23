using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UpdateDriver : MonoBehaviour
{
    public bool displayGizmos;

    public static UpdateDriver ud;
    public static int layerMask = ~(1 << 7);

    InteractableObjectLoader objectLoader;
    ImageCapture imgCapture;
    CharacterController2D cc2d;
    AnimationStateController animStates;
    SpeechScript speech;
    CharacterController3D cc3d;
    SFXScript sfx;
    MusicScript music;
    ModeSwitcher switcher;
    FreeMouseEnabler mouseEnabler;
    DeveloperTools devTools;
    OverlayController overlay;

    PauseSettingsScript pause;


    public int returnScene;
    public Shader flatShader;
    public bool mouseLocked;
    public AudioClip[] phones;
    public AudioClip song;
    public EchoDialogueObject_3D[] dialogueObjs;

    void Awake()
    {
        ud = this;

        this.gameObject.AddComponent<InteractableObjectLoader>();
        objectLoader = GetComponent<InteractableObjectLoader>();
        objectLoader.shader = flatShader;

        this.gameObject.AddComponent<ImageCapture>();
        imgCapture = GetComponent<ImageCapture>();

        this.gameObject.AddComponent<CharacterController2D>();
        cc2d = GetComponent<CharacterController2D>();

        this.gameObject.AddComponent<AnimationStateController>();
        animStates = GetComponent<AnimationStateController>();

        this.gameObject.AddComponent<SpeechScript>();
        speech = GetComponent<SpeechScript>();

        this.gameObject.AddComponent<CharacterController3D>();
        cc3d = GetComponent<CharacterController3D>();

        this.gameObject.AddComponent<SFXScript>();
        sfx = GetComponent<SFXScript>();

        this.gameObject.AddComponent<MusicScript>();
        music = GetComponent<MusicScript>();

        this.gameObject.AddComponent<ModeSwitcher>();
        switcher = GetComponent<ModeSwitcher>();

        this.gameObject.AddComponent<FreeMouseEnabler>();
        mouseEnabler = GetComponent<FreeMouseEnabler>();

        this.gameObject.AddComponent<DeveloperTools>();
        devTools = GetComponent<DeveloperTools>();

        this.gameObject.AddComponent<OverlayController>();
        overlay = GetComponent<OverlayController>();

        this.gameObject.AddComponent<PauseSettingsScript>();
        pause = GetComponent<PauseSettingsScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        objectLoader._Start();
        imgCapture._Start();
        cc2d._Start();
        animStates._Start();
        speech._Start();
        cc3d._Start();
        sfx._Start();
        music._Start();
        switcher._Start();
        overlay._Start();

        pause._Start();

        mouseEnabler.toggleMouse(mouseLocked);
        mouseEnabler._Start();
        devTools._Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (!devTools.devModeEnabled)
        {
            if (!pause.gamePaused)
            {
                cc2d._Update();
                animStates._Update();
                speech._Update();
                cc3d._Update();
                switcher._Update();
                overlay._Update();
            }
            pause._Update();
        }

        mouseEnabler._Update();
        devTools._Update();
    }

    public void speak(int id)
    {
        speech.Speak(dialogueObjs[id]);
    }
}
