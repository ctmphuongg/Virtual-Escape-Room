using UnityEngine;

public class OpenLockedDoor : MonoBehaviour
{
    [SerializeField] private float openSpeed = 20f;
    
    private bool isOpening = false;
    private Quaternion targetRotation;

    void Start()
    {
        targetRotation = Quaternion.Euler(
            transform.eulerAngles.x,
            transform.eulerAngles.y + 90f,
            transform.eulerAngles.z
        );
    }

    void Update()
    {
        if (isOpening)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, openSpeed * Time.deltaTime);
        }
        
    }

    public void OpenDoor()
    {
        isOpening = true;
    }
}