using UnityEngine;
using System.Collections;

public class MouseLookCamera : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    public float clampAngle = 80.0f;

    private float m_fRotY = 0.0f; // rotation around the up/y axis
    private float m_fRotX = 0.0f; // rotation around the right/x axis

    private Quaternion m_qOriginalRotation;

    void Start()
    {
        m_fRotY = 0F;
        m_fRotX = 0F;

        m_qOriginalRotation = transform.localRotation;
    }

    void Update()
    {


        m_fRotX += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        m_fRotY += Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        m_fRotX = Mathf.Clamp(m_fRotX, -clampAngle, clampAngle);

        Quaternion xQuaternion = Quaternion.AngleAxis(m_fRotX, Vector3.up);
        Quaternion yQuaternion = Quaternion.AngleAxis(m_fRotY, -Vector3.right);

        transform.localRotation = m_qOriginalRotation * xQuaternion * yQuaternion;
    }
}
