using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharacterController2D : MonoBehaviour
{

    ImageCapture ic;
    RawImageCapture raw;
    ModeSwitcher switcher;

    public Vector2Int playerPosition;
    public Vector2 playerPositionFloat;
    public Vector2 moveDirection;
    Vector2 pointerPosition;

    public Vector3 respawnPosition;
    public Vector3 worldPosition;
    public Vector3 checkPointPosition;
    public Vector3 initialSpawnPoint;

    public GameObject player2D;

    public RectTransform playerRectTransform;
    public RectTransform pointer;
    public Transform playerTransform;
    public Image playerImage;
    public Image animatedPlayerImage;
    public Image pointerImage;

    public int colliderRadius;
    public int collisionResolution = 20;

    float playerSpd = 600;
    float playerGravity = 10;
    float JumpHeight = 2.5f;

    List<Vector2> collisionPoints;

    public bool isInCollider;
    public bool isGrounded;
    public bool isGrounded2;
    public bool isOnScreen;
    public bool jumping;

    bool killFromOuchie;

    Camera visualCamera;

    public UnityEvent onDie;
    public UnityEvent onRespawnCheckpoint;

    public void _Start()
    {
        ic = UpdateDriver.ud.GetComponent<ImageCapture>();
        raw = UpdateDriver.ud.GetComponent<ImageCapture>().raw;
        switcher = UpdateDriver.ud.GetComponent<ModeSwitcher>();

        visualCamera = GameObject.FindGameObjectWithTag("Visual Camera").GetComponent<Camera>();

        pointer = GameObject.FindGameObjectWithTag("Pointer 2D").GetComponent<RectTransform>();
        pointerImage = pointer.gameObject.GetComponent<Image>();

        player2D = GameObject.FindGameObjectWithTag("Player 2D");
        playerRectTransform = player2D.GetComponent<RectTransform>();
        playerTransform = player2D.transform;
        playerImage = player2D.GetComponent<Image>();//TEMPORARY
        animatedPlayerImage = GameObject.FindGameObjectWithTag("AnimatedPlayer").GetComponent<Image>();

        //1920/x==30
        //1080/y==60

        //sets Constant size for player in different resolutions. 
        playerRectTransform.sizeDelta = new Vector2(Screen.width/64, Screen.height/18);
        animatedPlayerImage.rectTransform.sizeDelta = new Vector2(playerRectTransform.sizeDelta.y*1.5f, playerRectTransform.sizeDelta.y * 1.5f);

        playerPosition = CharacterController2D.Vector2IntFromVector3(playerRectTransform.anchoredPosition);
        playerPositionFloat = playerPosition;
        colliderRadius = Mathf.RoundToInt(playerRectTransform.sizeDelta.x/2f);

        collisionPoints = new List<Vector2>();
        cast3DPoint();

        if (initialSpawnPoint != new Vector3(-1, -1, -1)) { worldPosition = initialSpawnPoint;  }
        respawnPosition = worldPosition;

        onDie = new UnityEvent();
        onRespawnCheckpoint = new UnityEvent();
    }

    public void _Update()
    {
        isOnScreen = playerRectTransform.anchoredPosition3D.z > 0;

        if (UpdateDriver.ud.GetComponent<ModeSwitcher>().firstPerson) { inactiveMode(); return; }

        if (ic.texture == null) { return; }

        movePlayer();

        playerPosition = CharacterController2D.Vector2IntFromVector3(playerPositionFloat);
        playerRectTransform.anchoredPosition = playerPosition;
        raw.capturePos = new Vector2Int(playerPosition.x - (raw.captureDimensions.x/2), playerPosition.y - (raw.captureDimensions.y / 2));

        //set 3d position in space
        cast3DPoint();

        killConditions();

        Destroy(ic.texture);//SUPER SUPER IMPORTANT FOR PERFORMANCE
    }

    void killConditions()
    {
        bool offscreen = playerPositionFloat.x < 0 ||
            playerPositionFloat.x > Screen.width ||
            playerPositionFloat.y < 0 ||
            playerPositionFloat.y > Screen.height ||
            !isOnScreen;

        if (offscreen)
        {
            DIE("out of screen");
        }

        if (killFromOuchie)
        {
            DIE("touched an ouchie");
            killFromOuchie = false;
        }
    }

    void cast3DPoint()
    {
        Physics.Raycast(visualCamera.ScreenPointToRay(playerRectTransform.anchoredPosition), out RaycastHit hit, Mathf.Infinity, UpdateDriver.layerMask);// does not discriminate on the layer
        worldPosition = hit.point;
    }

    void inactiveMode()
    {
        moveDirection = Vector2.zero;
        
        playerRectTransform.anchoredPosition3D = visualCamera.WorldToScreenPoint(worldPosition);

        playerPositionFloat = playerRectTransform.anchoredPosition;
        playerPosition = Vector2IntFromVector3(playerPositionFloat);
        collisionPoints.Clear();

        
        //offscreenPointer();
    }

    public bool checkForColliderBetween()
    {
        Physics.Linecast(UpdateDriver.ud.GetComponent<CharacterController3D>().head.position, worldPosition, out RaycastHit hit, UpdateDriver.layerMask);
        return hit.collider != null && (
            hit.distance < Vector3.Distance(UpdateDriver.ud.GetComponent<CharacterController3D>().head.position, worldPosition)-.1f
            );
    }

    void offscreenPointer()
    {
        bool offscreen = playerPositionFloat.x < 0 ||
            playerPositionFloat.x > Screen.width ||
            playerPositionFloat.y < 0 ||
            playerPositionFloat.y > Screen.height ||
            !isOnScreen;

        pointerImage.enabled = offscreen;
        if (offscreen)
        {
            pointerPosition = playerPositionFloat;
            pointerPosition.x = Mathf.Clamp(pointerPosition.x, 25, Screen.width - 25);
            pointerPosition.y = Mathf.Clamp(pointerPosition.y, 25, Screen.height - 25);

            pointer.anchoredPosition = pointerPosition;
        }
    }

    void movePlayer()
    {
        moveDirection.x = Input.GetAxis("Horizontal_2D");
        moveDirection.y -= playerGravity * Time.deltaTime;

        playerPositionFloat += moveDirection * playerSpd * Time.deltaTime;

        calculateCollisions();
        foreach (Vector2 v in collisionPoints)
        {
            playerPositionFloat -= v * moveDirection.magnitude * playerSpd * Time.deltaTime;
        }

        if (isGrounded) 
        { 
            moveDirection.y = 0;
        }

        if (isGrounded || isGrounded2)
        {
            if (Input.GetButton("Jump_2D"))
            {
                moveDirection.y = JumpHeight;
            }
        }
    }

    void calculateCollisions()
    {
        collisionPoints.Clear();
        //----------------------------------
        Vector2Int outOfBoundsDelta = new Vector2Int(0, 0);

        if (raw.capturePos.x < 0) { outOfBoundsDelta.x = raw.capturePos.x; }
        else if (raw.capturePos.x + raw.captureDimensions.x > Screen.width) { outOfBoundsDelta.x = (raw.capturePos.x + raw.captureDimensions.x) - Screen.width; }

        if (raw.capturePos.y < 0) { outOfBoundsDelta.y = raw.capturePos.y; }
        else if (raw.capturePos.y + raw.captureDimensions.y > Screen.height) { outOfBoundsDelta.y = (raw.capturePos.y + raw.captureDimensions.y) - Screen.height; }
        
        //----------------------------------
        int radiusHalf = colliderRadius / 2;
        float collisionResolutionDegree = collisionResolution / (Mathf.PI*2);
        isInCollider = false;
        isGrounded = false;

        for (int i = 0; i < collisionResolution; i++)
        {
            Vector2 checkPoint = new Vector2(Mathf.Cos(i / (collisionResolutionDegree)), Mathf.Sin(i / (collisionResolutionDegree))) * colliderRadius;
            checkPoint += outOfBoundsDelta;
            bool topcolliders = i < radiusHalf;
            if (topcolliders) { checkPoint += new Vector2(0, colliderRadius); }
            else { checkPoint -= new Vector2(0,colliderRadius); }
            
            Vector2Int checkPointInt = CharacterController2D.Vector2IntFromVector3(checkPoint);
            checkPointInt += raw.captureDimensions / 2;
            Color detectedColor = ic.texture.GetPixel(checkPointInt.x, checkPointInt.y);
            if (detectedColor != ColorInfo.cutoutColor_2D)
            {
                if (detectedColor == ColorInfo.platformColor_2D)//IF YOU DETECT A PLATFORM COLOR
                {
                    if (!topcolliders) { isGrounded = true; }
                    isInCollider = true;
                    collisionPoints.Add(checkPoint.normalized);
                }
                else if (detectedColor == ColorInfo.damageColor_2D)
                {
                    killFromOuchie = true;
                }              
            }
        }

        Vector2 groundPixelPos = Vector2.down * colliderRadius * 3.5f;
        groundPixelPos += outOfBoundsDelta;
        Vector2Int groundPixelPosInt = Vector2IntFromVector3(groundPixelPos);
        groundPixelPosInt += raw.captureDimensions / 2;
        
        Color groundPixel = ic.texture.GetPixel(groundPixelPosInt.x,groundPixelPosInt.y);
        
        isGrounded2 = false;
        
        if (groundPixel != ColorInfo.cutoutColor_2D)
        {
            isGrounded2 = true;
        }

        //check if grounded

    }

    //respawns player at checkpoint
    public void respawnAtCheckPoint()
    {
        respawnPosition = checkPointPosition;
        worldPosition = respawnPosition;
        switcher.toggleMode(true);
        onRespawnCheckpoint.Invoke();
    }

    //this method returns the raycast information of whatever collider is behind the 2d player's canvas location. 
    //useful for interactable objects in scene. 
    public RaycastHit colliderAtPlayerPosition()
    {
        Physics.Raycast(visualCamera.ScreenPointToRay(playerRectTransform.anchoredPosition), out RaycastHit hit, Mathf.Infinity, UpdateDriver.layerMask);
        return hit;
    }

    //the death method also invokes an publically accessible event. 
    //possible uses: 
    //- trigger dialogue on death
    //- play sound 
    public void DIE(string str = "Died")
    {
        worldPosition = respawnPosition;

        if (switcher.force2DMode)
        {
            inactiveMode();
        }
        else
        {
            switcher.toggleMode(true);
        }

        

        onDie.Invoke();
        print($"2D Character died from '{str}'.");
    }

    //converts vector3 and vector 2 into vector2 int
    public static Vector3 Vector3FromVector2Int(Vector2Int v)
    {
        return new Vector3(v.x,v.y,0);
    }
    //converts vector2int into vector3
    public static Vector2Int Vector2IntFromVector3(Vector3 v)
    {
        return new Vector2Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
    }
}
