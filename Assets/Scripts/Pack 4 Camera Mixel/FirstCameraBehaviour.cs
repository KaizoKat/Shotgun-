using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstCameraBehaviour : MonoBehaviour
{
    public enum PixelScreenMode { Resize, Scale}

    [System.Serializable]
    public struct ScreenSize
    {
        public int width , height;
    }

    [Header("Scaling Settings")]
    public PixelScreenMode mode;
    public ScreenSize TSS = new ScreenSize { width = 256, height = 144};
    public uint SSF = 1;

    private Camera renderCamera;
    private RenderTexture renderTexture;
    private Texture2D finalTexture;
    private int screenWidth, screenHeight;

    [Header("Display")]
    public RawImage display;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (CheckResizeScreen())
            Init();
    }

    public bool CheckResizeScreen()
    {
        
        return Screen.width != screenWidth || screenHeight != screenHeight;

    }

    public void Init()
    {
        //init the cam + screen size
        if (!renderCamera) renderCamera = GetComponent<Camera>();
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        //error prevention
        if(SSF < 1) SSF = 1;
        if(TSS.width < 1) TSS.width = 1;
        if(TSS.height < 1) TSS.height = 1;

        //render texture size calculation
        int width = mode == PixelScreenMode.Resize ? (int)TSS.width : screenWidth / (int)SSF;
        int height = mode == PixelScreenMode.Resize ? (int)TSS.height : screenHeight / (int)SSF;

        //render texture
        renderTexture = new RenderTexture(width, height, 24)
        {
            filterMode = FilterMode.Point,
            graphicsFormat = UnityEngine.Experimental.Rendering.GraphicsFormat.RGB_BC6H_SFloat
        };



        //out to camera
        renderCamera.targetTexture = renderTexture;

        //attach texture to display UI
        display.enabled = true;
        display.texture = renderTexture;
    }
}
