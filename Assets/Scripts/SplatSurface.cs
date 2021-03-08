using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatSurface : MonoBehaviour
{
    [SerializeField] private Material materialTemplate = null;

    private Camera surfaceCamera;
    private RenderTexture renderTexture;
    private Material material;

    private bool reset = false;


    private void Start()
    {
        Renderer renderer = GetComponent<Renderer>();

        // We must reset rotation to correctly calculate renderer bounds
        Quaternion rotation = transform.rotation;
        transform.rotation = Quaternion.identity;
        Vector2 boundsSize = renderer.bounds.size;
        transform.rotation = rotation; // restore after calculating bounds

        // Calculate resolution required for the render texture
        int resX = Mathf.FloorToInt(Screen.width * boundsSize.x / (2f * Camera.main.orthographicSize * Camera.main.aspect));
        int resY = Mathf.FloorToInt(Screen.height * boundsSize.y / (2f * Camera.main.orthographicSize));

        renderTexture = new RenderTexture(resX, resY, 8);
        material = new Material(materialTemplate); // Material using custom masked shader
        material.SetTexture("_MaskTex", renderTexture);
        renderer.material = material;

        surfaceCamera = gameObject.AddComponent<Camera>();
        surfaceCamera.targetTexture = renderTexture;
        surfaceCamera.orthographic = true;
        surfaceCamera.orthographicSize = boundsSize.y / 2f;
        surfaceCamera.backgroundColor = Color.black;
        surfaceCamera.clearFlags = CameraClearFlags.SolidColor;
        surfaceCamera.cullingMask = LayerMask.GetMask("Mask");

        reset = true;
    }

    private void Update()
    {
        if (reset)
        {
            reset = false;
            surfaceCamera.clearFlags = CameraClearFlags.Nothing; // don't clear render target each frame
        }
    }

    private void OnDestroy()
    {
        material.SetTexture("_MaskTex", null);
        renderTexture.Release();
    }


    public void Reset()
    {
        // Setting clear flags to SolidColor for 1 frame in order to clear the splat render texture
        surfaceCamera.clearFlags = CameraClearFlags.SolidColor;
        reset = true;
    }
}
