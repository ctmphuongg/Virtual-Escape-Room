using System.Collections;
using UnityEngine;

namespace NavKeypad
{
    public class OpenLockedDoor : MonoBehaviour
    {
        [Header("Rotation Settings")]
        [SerializeField] private float openAngle = 90f;
        [SerializeField] private float openSpeed = 2f;
        
        [Header("Auto Close")]
        [SerializeField] private bool autoClose = false;
        [SerializeField] private float autoCloseDelay = 3f;
        
        [Header("Sound Effects")]
        [SerializeField] private AudioClip doorOpenSfx;
        [SerializeField] private AudioClip doorCloseSfx;
        [SerializeField] private AudioSource audioSource;
        
        private Quaternion closedRotation;
        private Quaternion openRotation;
        private bool isOpen = false;
        private bool isAnimating = false;
        
        private void Awake()
        {
            closedRotation = transform.localRotation;
            openRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);
        }
        
        public void OpenDoor()
        {
            if (!isOpen && !isAnimating)
            {
                StartCoroutine(AnimateDoor(true));
            }
        }
        
        public void CloseDoor()
        {
            if (isOpen && !isAnimating)
            {
                StartCoroutine(AnimateDoor(false));
            }
        }
        
        public void ToggleDoor()
        {
            if (isAnimating) return;
            
            if (isOpen)
                CloseDoor();
            else
                OpenDoor();
        }
        
        private IEnumerator AnimateDoor(bool opening)
        {
            isAnimating = true;
            isOpen = opening;
            
            // Play sound
            if (audioSource != null)
            {
                AudioClip sfx = opening ? doorOpenSfx : doorCloseSfx;
                if (sfx != null) audioSource.PlayOneShot(sfx);
            }
            
            Quaternion startRot = transform.localRotation;
            Quaternion endRot = opening ? openRotation : closedRotation;
            float elapsedTime = 0f;
            float duration = 1f / openSpeed;
            
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / duration);
                transform.localRotation = Quaternion.Slerp(startRot, endRot, t);
                yield return null;
            }
            
            transform.localRotation = endRot;
            isAnimating = false;
            
            // Auto close
            if (autoClose && opening)
            {
                yield return new WaitForSeconds(autoCloseDelay);
                CloseDoor();
            }
        }
    }
}