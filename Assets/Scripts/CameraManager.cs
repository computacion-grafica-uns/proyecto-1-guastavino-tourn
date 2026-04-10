using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private GameObject myCamera;
    private Vector3 pos = new Vector3(5, 1, -10);
    private Vector3 target = new Vector3(0, 0, 1);
    private Vector3 up = new Vector3(0, 1, 0);
    private bool orbitalMode = false;
    private float orbitDistance = 45f;
    private float orbitYaw = 0f;
    private float orbitPitch = 30f;
    private Vector3 orbitTarget = new Vector3(0f, 0f, 0f);

    private float cameraSpeed = 10f;
    private float mouseSensitivity = 2f;
    private float yaw = 0f;
    private float pitch = 0f;
    void Start()
    {
        myCamera = new GameObject("Camera");
        myCamera.AddComponent<Camera>();
        myCamera.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
        myCamera.GetComponent<Camera>().backgroundColor = Color.black;
        myCamera.GetComponent<Camera>().nearClipPlane = 0.01f;
        myCamera.GetComponent<Camera>().farClipPlane = 10000f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            orbitalMode = !orbitalMode;

        if (orbitalMode)
            UpdateOrbital();
        else
            UpdateFirstPerson();
    }

    private void UpdateOrbital()
    {
        // Rotar alrededor del punto con A/D, subir/bajar con W/S
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) orbitYaw -= mouseSensitivity * 50f * Time.deltaTime;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) orbitYaw += mouseSensitivity * 50f * Time.deltaTime;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) orbitPitch += mouseSensitivity * 50f * Time.deltaTime;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) orbitPitch -= mouseSensitivity * 50f * Time.deltaTime;

        // Scroll o Shift/Space para acercar/alejar
        orbitDistance -= Input.GetAxis("Mouse ScrollWheel") * 5f;
        if (Input.GetKey(KeyCode.Space)) orbitDistance -= cameraSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftShift)) orbitDistance += cameraSpeed * Time.deltaTime;
        //orbitDistance = Mathf.Clamp(orbitDistance, 1f, 50f);

        //orbitPitch = Mathf.Clamp(orbitPitch, -89f, 89f);

        float pitchRad = orbitPitch * Mathf.Deg2Rad;
        float yawRad = orbitYaw * Mathf.Deg2Rad;

        // Calcular posición de la cámara alrededor del orbitTarget
        pos = orbitTarget + new Vector3(
            Mathf.Cos(pitchRad) * Mathf.Sin(yawRad),
            Mathf.Sin(pitchRad),
            Mathf.Cos(pitchRad) * Mathf.Cos(yawRad)
        ) * orbitDistance;

        target = orbitTarget;
        ApplyViewMatrix();
        Matrix4x4 viewMatrix = CreateViewMatrix(pos, target, up);
        foreach (Renderer r in FindObjectsByType<Renderer>(FindObjectsSortMode.None))
        {
            if (r.material.HasProperty("_ViewMatrix"))
                r.material.SetMatrix("_ViewMatrix", viewMatrix);
        }
    }
    private void UpdateFirstPerson()
    {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        //pitch = Mathf.Clamp(pitch, -89f, 89f);

        float pitchRad = pitch * Mathf.Deg2Rad;
        float yawRad = yaw * Mathf.Deg2Rad;

        Vector3 forward = new Vector3(
            Mathf.Cos(pitchRad) * Mathf.Sin(yawRad),
            Mathf.Sin(pitchRad),
            Mathf.Cos(pitchRad) * Mathf.Cos(yawRad)
        ).normalized;

        Vector3 right = Vector3.Cross(forward, up).normalized;
        Vector3 move = Vector3.zero;
        Vector3 flatForward = -new Vector3(forward.x, 0f, forward.z).normalized;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) move += flatForward;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) move -= flatForward;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) move += right;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) move -= right;
        if (Input.GetKey(KeyCode.Space)) move -= up;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) move += up;

        if (move != Vector3.zero)
            pos -= move.normalized * cameraSpeed * Time.deltaTime;

        target = pos + forward;
        ApplyViewMatrix();

        Matrix4x4 viewMatrix = CreateViewMatrix(pos, target, up);
        foreach (Renderer r in FindObjectsByType<Renderer>(FindObjectsSortMode.None))
        {
            if (r.material.HasProperty("_ViewMatrix"))
                r.material.SetMatrix("_ViewMatrix", viewMatrix);
        }
    }

    private Matrix4x4 CreateViewMatrix(Vector3 eye, Vector3 target, Vector3 up)
    {
        Vector3 f = (target - eye).normalized;
        Vector3 r = (Vector3.Cross(f, up)).normalized;
        Vector3 u = Vector3.Cross(r, f);

        Matrix4x4 viewMatrix = new Matrix4x4(
            new Vector4(r.x, r.y, r.z, -(r.x * eye.x + r.y * eye.y + r.z * eye.z)),
            new Vector4(u.x, u.y, u.z, -(u.x * eye.x + u.y * eye.y + u.z * eye.z)),
            new Vector4(-f.x, -f.y, -f.z, (f.x * eye.x + f.y * eye.y + f.z * eye.z)),
            new Vector4(0f, 0f, 0f, 1f)
        );

        return viewMatrix.transpose;
    }

    private void ApplyViewMatrix()
    {
        Matrix4x4 viewMatrix = CreateViewMatrix(pos, target, up);
        foreach (Renderer r in FindObjectsByType<Renderer>(FindObjectsSortMode.None))
        {
            if (r.material.HasProperty("_ViewMatrix"))
                r.material.SetMatrix("_ViewMatrix", viewMatrix);
        }

        myCamera.transform.position = pos;
        myCamera.transform.LookAt(target, up);
    }
}
