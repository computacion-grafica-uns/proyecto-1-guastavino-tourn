using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private GameObject myCamera;

    private Vector3 pos = new Vector3(5, 1, -10);
    private Vector3 target = new Vector3(0, 0, 1);
    private Vector3 up = new Vector3(0, 1, 0);
    private bool orbitalMode = false;
    private float orbitDistance = 30f;
    private float orbitYaw = 0f;
    private float orbitPitch = 30f;
    private Vector3 orbitTarget = new Vector3(5f, 0f, 5f);

    private float cameraSpeed = 10f;
    private float mouseSensitivity = 2f;
    private float yaw = 0f;
    private float pitch = 0f;
    private float fov = 60;
    private float aspectRatio = 16 / (float)9;
    private float nearClipPlane = 0.1f;
    private float farClipPlane = 1000;
    
    void Start()
    {
        myCamera = new GameObject("Camera");
        myCamera.AddComponent<Camera>();
        myCamera.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
        myCamera.GetComponent<Camera>().backgroundColor = Color.black;
        myCamera.GetComponent<Camera>().nearClipPlane = 0.01f;
        myCamera.GetComponent<Camera>().farClipPlane = 10000f;


        ApplyProjectionMatrix();
        InitializeFirstPerson();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            orbitalMode = !orbitalMode;

            if (orbitalMode)
                InitializeOrbital();
            else
                InitializeFirstPerson();
        }

        if (orbitalMode)
            UpdateOrbital();
        else
            UpdateFirstPerson();
    }

    private void InitializeFirstPerson()
    {
        pos = new Vector3(5f, 1f, -10f);
        yaw = 0f;
        pitch = 0f;

        float pitchRad = pitch * Mathf.Deg2Rad;
        float yawRad = yaw * Mathf.Deg2Rad;

        Vector3 forward = new Vector3(
            Mathf.Cos(pitchRad) * Mathf.Sin(yawRad),
            Mathf.Sin(pitchRad),
            Mathf.Cos(pitchRad) * Mathf.Cos(yawRad)
        ).normalized;

        target = pos + forward;
        ApplyViewMatrix();
    }

    private void InitializeOrbital()
    {
        orbitDistance = 30f;
        orbitYaw = 180f;
        orbitPitch = 30f;
        orbitTarget = new Vector3(5f, 0f, 5f);

        float pitchRad = orbitPitch * Mathf.Deg2Rad;
        float yawRad = orbitYaw * Mathf.Deg2Rad;

        pos = orbitTarget + new Vector3(
            Mathf.Cos(pitchRad) * Mathf.Sin(yawRad),
            Mathf.Sin(pitchRad),
            Mathf.Cos(pitchRad) * Mathf.Cos(yawRad)
        ) * orbitDistance;

        target = orbitTarget;
        ApplyViewMatrix();
    }

    private void UpdateOrbital()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) orbitYaw += mouseSensitivity * 50f * Time.deltaTime;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) orbitYaw -= mouseSensitivity * 50f * Time.deltaTime;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) orbitPitch += mouseSensitivity * 50f * Time.deltaTime;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) orbitPitch -= mouseSensitivity * 50f * Time.deltaTime;

        orbitDistance -= Input.GetAxis("Mouse ScrollWheel") * 5f;
        if (Input.GetKey(KeyCode.Space)) orbitDistance -= cameraSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftShift)) orbitDistance += cameraSpeed * Time.deltaTime;

        float pitchRad = orbitPitch * Mathf.Deg2Rad;
        float yawRad = orbitYaw * Mathf.Deg2Rad;

        pos = orbitTarget + new Vector3(
            Mathf.Cos(pitchRad) * Mathf.Sin(yawRad),
            Mathf.Sin(pitchRad),
            Mathf.Cos(pitchRad) * Mathf.Cos(yawRad)
        ) * orbitDistance;

        target = orbitTarget;
        ApplyViewMatrix();
    }
    private void UpdateFirstPerson()
    {
        float rotateHorizontal = 0f;
        float rotateVertical = 0f;

        if (Input.GetKey(KeyCode.LeftArrow)) rotateHorizontal = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) rotateHorizontal = 1f;
        if (Input.GetKey(KeyCode.UpArrow)) rotateVertical = 1f;
        if (Input.GetKey(KeyCode.DownArrow)) rotateVertical = -1f;

        float rotationSpeed = 90f;

        yaw += rotateHorizontal * rotationSpeed * Time.deltaTime;
        pitch += rotateVertical * rotationSpeed * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -89f, 89f);

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

        Vector3 moveDir = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) moveDir -= flatForward;
        if (Input.GetKey(KeyCode.S)) moveDir += flatForward;
        if (Input.GetKey(KeyCode.A)) moveDir += right;
        if (Input.GetKey(KeyCode.D)) moveDir -= right;
        if (Input.GetKey(KeyCode.Space)) moveDir += up;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) moveDir -= up;

        if (moveDir != Vector3.zero)
        {
            moveDir.Normalize();
            pos += moveDir * cameraSpeed * Time.deltaTime;
        }

        target = pos + forward;
        ApplyViewMatrix();
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

        private Matrix4x4 CreateProjectionMatrix(float fov, float aspect, float n, float f)
    {
        float fovRad = fov * Mathf.Deg2Rad;
        Matrix4x4 perspectiveProjectMatrix = new Matrix4x4(
            new Vector4(-1 / (aspect * Mathf.Tan(fovRad / 2)), 0f, 0f, 0f),
            new Vector4(0f, 1 / Mathf.Tan(fovRad / 2), 0f, 0f),
            new Vector4(0f, 0f, (f + n) / (n - f), (2 * f * n) / (n - f)),
            new Vector4(0f, 0f, -1f, 0f)
        );

        return perspectiveProjectMatrix.transpose;
    }

    private void ApplyViewMatrix()
    {
        Matrix4x4 viewMatrix = CreateViewMatrix(pos, target, up);
        foreach (Renderer r in FindObjectsByType<Renderer>(FindObjectsSortMode.None))
        {
            r.material.SetMatrix("_ViewMatrix", viewMatrix);
            Matrix4x4 v = r.material.GetMatrix("_ViewMatrix");
            Debug.Log($"_ViewMatrix :\n{MatrixToString(v)}");
        }

        myCamera.transform.position = pos;
        myCamera.transform.LookAt(target, up);
    }
    private void ApplyProjectionMatrix()
    {
        Matrix4x4 proj = CreateProjectionMatrix(fov, aspectRatio, nearClipPlane, farClipPlane);
        foreach (Renderer r in FindObjectsByType<Renderer>(FindObjectsSortMode.None))
        {
                r.sharedMaterial.SetMatrix("_ProjectionMatrix", GL.GetGPUProjectionMatrix(proj, true));
        }
    }

    private string MatrixToString(Matrix4x4 m)
    {
        return
            $"{m.m00:F3}\t{m.m01:F3}\t{m.m02:F3}\t{m.m03:F3}\n" +
            $"{m.m10:F3}\t{m.m11:F3}\t{m.m12:F3}\t{m.m13:F3}\n" +
            $"{m.m20:F3}\t{m.m21:F3}\t{m.m22:F3}\t{m.m23:F3}\n" +
            $"{m.m30:F3}\t{m.m31:F3}\t{m.m32:F3}\t{m.m33:F3}";
    }
}
