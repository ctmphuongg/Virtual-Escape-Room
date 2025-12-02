using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class ChairSocketValidator : MonoBehaviour
{
    [System.Serializable]
    public class ChairSocket
    {
        public GameObject chair;
        public GameObject correctObject; // The correct object for this socket
        [HideInInspector] public UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socketInteractor;
        [HideInInspector] public bool hasCorrectObject = false;
    }

    [Header("Chair Configuration")]
    [SerializeField] private List<ChairSocket> chairSockets = new List<ChairSocket>(4);

    [Header("Picture Configuration")]
    [SerializeField] private Transform pictureTransform;
    [SerializeField] private float moveDistanceX = 1f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private AudioSource audioSource;

    private bool allCorrect = false;
    private bool pictureMoved = false;
    private Vector3 pictureStartPosition;
    private Vector3 pictureTargetPosition;

    void Start()
    {
        // Initialize socket interactors for each chair
        foreach (ChairSocket cs in chairSockets)
        {
            if (cs.chair != null)
            {
                // Find the socket interactor in the child object
                cs.socketInteractor = cs.chair.GetComponentInChildren<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();
                
                if (cs.socketInteractor != null)
                {
                    // Subscribe to socket events
                    cs.socketInteractor.selectEntered.AddListener((args) => OnSocketSelectEntered(cs, args));
                    cs.socketInteractor.selectExited.AddListener((args) => OnSocketSelectExited(cs, args));
                }
                else
                {
                    Debug.LogWarning($"No XRSocketInteractor found in children of {cs.chair.name}");
                }
            }
        }

        // Store picture's initial position
        if (pictureTransform != null)
        {
            pictureStartPosition = pictureTransform.position;
            pictureTargetPosition = pictureStartPosition + new Vector3(moveDistanceX, 0, 0);
        }
        else
        {
            Debug.LogError("Picture Transform is not assigned!");
        }
    }

    void OnSocketSelectEntered(ChairSocket chairSocket, SelectEnterEventArgs args)
    {
        // Check if the attached object matches the correct object reference
        GameObject attachedObject = args.interactableObject.transform.gameObject;
        chairSocket.hasCorrectObject = (attachedObject == chairSocket.correctObject);

        Debug.Log($"{chairSocket.chair.name} attached: {attachedObject.name} - Correct: {chairSocket.hasCorrectObject}");

        CheckAllSockets();
    }

    void OnSocketSelectExited(ChairSocket chairSocket, SelectExitEventArgs args)
    {
        chairSocket.hasCorrectObject = false;
        Debug.Log($"{chairSocket.chair.name} detached object");

        CheckAllSockets();
    }

    void CheckAllSockets()
    {
        // Check if all chairs have the correct objects
        bool previousState = allCorrect;
        allCorrect = true;

        foreach (ChairSocket cs in chairSockets)
        {
            if (!cs.hasCorrectObject)
            {
                allCorrect = false;
                break;
            }
        }

        // Trigger picture movement if state changed to all correct
        if (allCorrect && !previousState)
        {
            Debug.Log("All chairs have correct objects! Moving picture...");
            pictureMoved = false; // Reset to allow movement
            audioSource.Play();
        }
    }

    void Update()
    {
        // Smoothly move the picture when all sockets are correct
        if (allCorrect && !pictureMoved && pictureTransform != null)
        {
            pictureTransform.position = Vector3.MoveTowards(
                pictureTransform.position,
                pictureTargetPosition,
                moveSpeed * Time.deltaTime
            );

            // Check if picture reached target
            if (Vector3.Distance(pictureTransform.position, pictureTargetPosition) < 0.01f)
            {
                pictureTransform.position = pictureTargetPosition;
                pictureMoved = true;
                Debug.Log("Picture movement complete!");
            }
        }
    }

    void OnDestroy()
    {
        // Unsubscribe from events to prevent memory leaks
        foreach (ChairSocket cs in chairSockets)
        {
            if (cs.socketInteractor != null)
            {
                cs.socketInteractor.selectEntered.RemoveListener((args) => OnSocketSelectEntered(cs, args));
                cs.socketInteractor.selectExited.RemoveListener((args) => OnSocketSelectExited(cs, args));
            }
        }
    }
}