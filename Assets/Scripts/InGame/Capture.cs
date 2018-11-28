using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Capture : MonoBehaviour {

    private Camera camera;

    public Tilemap mapTile;

    public static string ScreenShotName(int width, int height)
    {
        return string.Format("{0}/screenshots/screen_{1}x{2}_{3}.png",
                             Application.dataPath,
                             width, height,
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    // Use this for initialization
    void Start () {

        camera = GetComponent<Camera>();

        RenderTexture rt = new RenderTexture(mapTile.size.x * 13 + 530, mapTile.size.y * 13, 24);
        camera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(mapTile.size.x * 13, mapTile.size.y * 13, TextureFormat.RGB24, false);
        camera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, mapTile.size.x * 13, mapTile.size.y * 13), 0, 0);
        camera.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = ScreenShotName(mapTile.size.x * 25, mapTile.size.y * 25);
        System.IO.File.WriteAllBytes(filename, bytes);
        Debug.Log(string.Format("Took screenshot to: {0}", filename));

        this.gameObject.SetActive(false);
    }
}
