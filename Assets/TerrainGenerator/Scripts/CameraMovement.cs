using UnityEngine;

namespace TestUI 
{
    public class CameraMovement : MonoBehaviour
    {
        public float MoveSpeed;
        public Camera Cam;

        void Update()
        {
            ButtonUpdate();
        }

        private void ButtonUpdate() 
        {
            Vector3 moveInput = Vector3.zero;
            moveInput.x = Input.GetAxis("Horizontal");
            moveInput.z = Input.GetAxis("Vertical"); 

            transform.position += moveInput * (MoveSpeed * Time.deltaTime);

            if (Input.GetAxis("Mouse ScrollWheel") > 0.0f)
            {
                transform.position -= Vector3.up * 1.5f;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0.0f) 
            {
                transform.position += Vector3.up * 1.5f;
            }

        }

    }
}
