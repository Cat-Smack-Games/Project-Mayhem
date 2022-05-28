using Dummiesman;
using ObjParser;
using ProjectMayhemContentFramework.Content;
using System;
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
    private Vector3 lastMousePos = Vector3.zero;

    //
    public GameObject currentModel;
    private GameObject pointer;
    private bool pointerRendered = false;
    
    //
    private Vector3 cameraPos = Vector3.zero;
    private Vector3 camRot = Vector3.zero;
    private float cameraYaw = 0f;
    private float cameraPitch = 0f;
    private float rotSpeed = 2f;
    private float moveSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        UI.Popup.Create("Dev", "In early development", true , Test1);
        if (Game.LocalPlayer().SessionTicket == null)
        {
            Game.Auth(); 
        }
        pointer = (GameObject) Instantiate(Resources.Load("Pointer"), Vector3.zero, Quaternion.identity);
        pointerRendered = true;
    }
    public void Test1()
    {
        Debug.Log("Yes!");
    }
    public void Test2()
    {
        Debug.Log("No!");
    }

    // Update is called once per frame
    void Update()
    {
        RenderPointer();
        RenderContent();
        CameraMovement();


    }

    private void CameraMovement()
    {
        if(Input.GetKey(KeyCode.Mouse1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            cameraYaw += rotSpeed * Input.GetAxis("Mouse X");
            cameraPitch += rotSpeed * Input.GetAxis("Mouse Y");
            Camera.main.transform.eulerAngles = new Vector3(cameraPitch, cameraYaw, 0f);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetKey(KeyCode.W))
        {
            Camera.main.transform.position += Camera.main.transform.forward * Time.deltaTime * moveSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Camera.main.transform.position -= Camera.main.transform.forward * Time.deltaTime * moveSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            Camera.main.transform.position -= Camera.main.transform.right * Time.deltaTime * moveSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Camera.main.transform.position += Camera.main.transform.right * Time.deltaTime * moveSpeed;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            Camera.main.transform.position += Vector3.up * Time.deltaTime * moveSpeed;
        }
        if (Input.GetKey(KeyCode.E))
        {
            Camera.main.transform.position += Vector3.down * Time.deltaTime * moveSpeed;
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

    public void SetSelectedModel(GameObject obj)
    {
        currentModel = obj;
    }
    public GameObject GetSelectedModel()
    {
        return currentModel;
    }

    public void RenderContent()
    {
        if (contentSelection.value == 0 && !textureContentRendered) // Textures
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
        else if (contentSelection.value == 1 && !modelContentRendered) // Models
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
    public void RenderPointer()
    {
        Plane plane = new Plane(Vector3.up, 0);
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            pointer.SetActive(true);
            pointer.transform.position = ray.GetPoint(distance);
        }
        else pointer.SetActive(false); 

    }
}
