using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSelector : MonoBehaviour
{   
    private Renderer greencubeRenderer;
    private Renderer whitecubeRenderer;
    private Renderer yellowcubeRenderer;
    private Renderer redcubeRenderer;
    public Text selectedText;

    // Start is called before the first frame update
    void Start()
    {
        // Find the parent GameObject by name
        GameObject colorParent = GameObject.Find("--color");

        // Find the green cube GameObject under the parent and get its renderer component
        GameObject greenCube = colorParent.transform.Find("green").gameObject;
        GameObject whiteCube = colorParent.transform.Find("white").gameObject;
        GameObject yellowCube = colorParent.transform.Find("yellow").gameObject;  
        GameObject redCube = colorParent.transform.Find("red").gameObject;
  

        Canvas[] canvases = colorParent.GetComponentsInChildren<Canvas>();

        GameObject panelObject = null;
        foreach (Canvas canvas in canvases)
        {
            Transform panelTransform = canvas.transform.Find("Panel");
            if (panelTransform != null)
            {
                panelObject = panelTransform.gameObject;
                break;
            }
        }

        
        // Get the Text component from the panelObject
        selectedText = panelObject.GetComponentInChildren<Text>();

        // Now you can work with the Text component
        if (selectedText != null)
        {
            // Access or modify the properties of the Text component
            selectedText.text = "Please choose your color";
        }

        greencubeRenderer = greenCube.GetComponent<Renderer>();
        whitecubeRenderer = whiteCube.GetComponent<Renderer>();
        yellowcubeRenderer = yellowCube.GetComponent<Renderer>();
        redcubeRenderer = redCube.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the left mouse button is pressed
        if (Input.GetMouseButton(0))
        {
            // Cast a ray from the mouse position into the scene
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits an object
            if (Physics.Raycast(ray, out hit))
            {
                // Green
                if (hit.collider.gameObject == greencubeRenderer.gameObject)
                {
                    // Set the color of the cube
                    greencubeRenderer.material.color = Color.green;

                    // Set the content of the text GameObject
                    selectedText.text = "Selected: Green";
                }

                // White
                if (hit.collider.gameObject == whitecubeRenderer.gameObject)
                {
                    // Set the color of the cube
                    whitecubeRenderer.material.color = Color.white;

                    // Set the content of the text GameObject
                    selectedText.text = "Selected: White";
                }

                // Yellow
                if (hit.collider.gameObject == yellowcubeRenderer.gameObject)
                {
                    // Set the color of the cube
                    yellowcubeRenderer.material.color = Color.yellow;

                    // Set the content of the text GameObject
                    selectedText.text = "Selected: Yellow";
                }

                // Red
                if (hit.collider.gameObject == redcubeRenderer.gameObject)
                {
                    // Set the color of the cube
                    redcubeRenderer.material.color = Color.red;

                    // Set the content of the text GameObject
                    selectedText.text = "Selected: red";
                }
            }
        }       
    }
}
