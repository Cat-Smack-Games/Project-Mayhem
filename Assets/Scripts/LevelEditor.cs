using Dummiesman;
using ObjParser;
using ProjectMayhemContentFramework.Content;
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
    private bool modelContentRendered;
    private List<GameObject> texturesRendered = new List<GameObject>();
    private List<GameObject> modelsRendered = new List<GameObject>();
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
            ResetContent();
            foreach (KeyValuePair<string, ProjectMayhemContentFramework.Content.Texture> pair in ContentManager.GetTextures())
            {
                GameObject texture = Instantiate(Resources.Load("TextureRenderer") as GameObject);
                texture.name = pair.Key;

                Texture2D convertedTx = ContentManager.SystemImageToTexture2D(pair.Value.texture);
                texture.GetComponent<Image>().sprite = Sprite.Create(convertedTx, new Rect(0.0f, 0.0f, 100, 100), new Vector2(0.5f, 0.5f), 100.0f); 
                texture.transform.SetParent(textureContent.transform, false);
                texturesRendered.Add(texture);

            }
            textureContentRendered = true;
        }
        else if (contentSelection.value == 1 && !modelContentRendered) // Textures
        {
            ResetContent();
            foreach (KeyValuePair<string, Model> pair in ContentManager.GetModels())
            {
                GameObject model = Instantiate(Resources.Load("ModelRenderer") as GameObject);
                model.name = pair.Key;
                
                GameObject gameobj = ContentManager.ModelToGameObject(pair.Value);
               
                model.transform.SetParent(modelContent.transform, false);
                modelsRendered.Add(model);

            }
            modelContentRendered = true;
        }
    }
    private void ResetContent()
    {
        ResetTextureContent();
        ResetModelContent();
    }
    void ResetTextureContent()
    {
        foreach(GameObject obj in texturesRendered)
        {
           GameObject.Destroy(obj);
            
        }
        texturesRendered.Clear();
        textureContentRendered = false;
    }
    void ResetModelContent()
    {
        foreach (GameObject obj in modelsRendered)
        {
            GameObject.Destroy(obj);

        }
        modelsRendered.Clear();
        modelContentRendered = false;
    }
}
