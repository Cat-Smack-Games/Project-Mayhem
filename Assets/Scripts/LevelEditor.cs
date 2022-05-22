using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class LevelEditor : MonoBehaviour
{
    public TMPro.TMP_Dropdown contentSelection;
    public GameObject textureContent;
    private bool textureContentRendered;
    public GameObject modelContent;
    // Start is called before the first frame update
    void Start()
    {
        if (Game.LocalPlayer().SessionTicket == null)
        {
            Game.Auth(); 
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(contentSelection.value == 0 && !textureContentRendered) // Textures
        {
            foreach (KeyValuePair<string, ProjectMayhemContentFramework.Content.Texture> pair in ContentManager.GetTextures())
            {
                GameObject texture = Instantiate(Resources.Load("TextureRenderer") as GameObject);
                texture.name = pair.Key;
               
                Bitmap bitmap = new Bitmap(pair.Value.texture);
                Texture2D convertedTx = new Texture2D(200,200);

                MemoryStream msFinger = new MemoryStream();
                bitmap.Save(msFinger, bitmap.RawFormat);
                convertedTx.LoadImage(msFinger.ToArray());
                texture.GetComponent<Image>().sprite = Sprite.Create(convertedTx, new Rect(0.0f, 0.0f, 100, 100), new Vector2(0.5f, 0.5f), 100.0f); 
                texture.transform.SetParent(textureContent.transform, false);

            }
            textureContentRendered = true;
        }
        else if (contentSelection.value == 1) // Models
        {

        }
    }
}
