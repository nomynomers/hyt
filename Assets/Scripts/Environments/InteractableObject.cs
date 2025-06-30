using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InteractableObject : MonoBehaviour
{
    [Header("Settings")]
    public string interactionText = "E to interact";
    public Key interactionKey = Key.E;
    public float textOffsetY = 1.5f;
    public Color textColor = Color.white;
    public string sceneToLoad = ""; // Name of scene to load
    
    private bool playerInRange = false;
    private bool hasInteracted = false;
    private GameObject textObject;
    private TextMesh textMesh;
    
    void Start()
    {
        CreateFloatingText();
    }
    
    void CreateFloatingText()
    {
        // Create a child GameObject for the text
        textObject = new GameObject("InteractionText");
        textObject.transform.SetParent(transform);
        textObject.transform.localPosition = new Vector3(0, textOffsetY, 0);
        
        // Add TextMesh component
        textMesh = textObject.AddComponent<TextMesh>();
        textMesh.text = interactionText;
        textMesh.fontSize = 20;
        textMesh.color = textColor;
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.alignment = TextAlignment.Center;
        
        // Add MeshRenderer for visibility
        MeshRenderer meshRenderer = textObject.GetComponent<MeshRenderer>();
        meshRenderer.sortingOrder = 10; // Render on top
        
        // Hide initially
        textObject.SetActive(false);
    }
    
    void Update()
    {
        // Make text face camera
        if (textObject.activeInHierarchy && Camera.main != null)
        {
            textObject.transform.LookAt(Camera.main.transform);
            textObject.transform.Rotate(0, 180, 0); // Flip to face camera correctly
        }
        
        // Check for interaction input when player is in range
        if (playerInRange && Keyboard.current[interactionKey].wasPressedThisFrame)
        {
            Interact();
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player entered the trigger
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            ShowText();
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the player left the trigger
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            HideText();
        }
    }
    
    void ShowText()
    {
        if (textObject != null && !hasInteracted)
        {
            textObject.SetActive(true);
        }
    }
    
    void HideText()
    {
        if (textObject != null)
        {
            textObject.SetActive(false);
        }
    }
    
    void Interact()
    {
        // Add your interaction logic here
        Debug.Log("Player interacted with " + gameObject.name);
        
        // Load the specified scene
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("No scene specified to load!");
        }
        
        // Mark as interacted and hide text
        hasInteracted = true;
        HideText();
    }
}