using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    [SerializeField] private GameObject[] targets;
    [SerializeField] private float _minimumOrthographicSize = 0.01f;
    [SerializeField] private Camera camera;

    [SerializeField][Min(1f)] private float spacingFactor = 1;

    private Vector3 _targetPosition;
    private float _targetSize;

    private void Awake()
    {
        if (!camera) camera = Camera.main;

        _targetPosition = camera.transform.position;
        _targetSize = camera.orthographicSize;
    }

    private void Update()
    {
        // Using the extensions from above
        if (targets.TryGetBounds(out var bounds))
        {
            _targetPosition = bounds.center;
            _targetPosition.z = camera.transform.position.z;

            _targetSize = CalculateOrthographicSize(bounds);
        }
        _targetPosition.x = _targetPosition.x + 1.5f;
        camera.transform.position = Vector3.Lerp(camera.transform.position, _targetPosition, 5 * Time.deltaTime);
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, _targetSize, 5 * Time.deltaTime);
    }

    private float CalculateOrthographicSize(Bounds boundingBox)
    {
        var orthographicSize = camera.orthographicSize;

        Vector2 min = boundingBox.min;
        Vector2 max = boundingBox.max;

        var width = (max - min).x * spacingFactor;
        var height = (max - min).y * spacingFactor;

        if (width > height)
        {
            orthographicSize = Mathf.Abs(width) / camera.aspect / 2f;
        }
        else
        {
            orthographicSize = Mathf.Abs(height) / 2f;
        }

        return Mathf.Max(orthographicSize, _minimumOrthographicSize);
    }



}


public static class Extensions
{
    public static bool TryGetBounds(this GameObject obj, out Bounds bounds)
    {
        var renderers = obj.GetComponentsInChildren<Renderer>();
        return renderers.TryGetBounds(out bounds);
    }

    public static bool TryGetBounds(this GameObject[] gameObjects, out Bounds bounds)
    {
        var renderers = gameObjects.Where(g => g).SelectMany(g => g.GetComponentsInChildren<Renderer>()).ToArray();
        return renderers.TryGetBounds(out bounds);
    }

    public static bool TryGetBounds(this Renderer[] renderers, out Bounds bounds)
    {
        bounds = default;

        if (renderers.Length == 0)
        {
            return false;
        }

        bounds = renderers[0].bounds;

        for (var i = 1; i < renderers.Length; i++)
        {
            bounds.Encapsulate(renderers[i].bounds);
        }

        return true;
    }

    // Facto how far away the camera should be 
    private const float cameraDistance = 1f;

    public static bool TryGetFocusTransforms(this Camera camera, GameObject targetGameObject, out Vector3 targetPosition, out Quaternion targetRotation)
    {
        targetPosition = default;
        targetRotation = default;

        if (!targetGameObject.TryGetBounds(out var bounds))
        {
            return false;
        }

        var objectSizes = bounds.max - bounds.min;
        var objectSize = Mathf.Max(objectSizes.x, objectSizes.y, objectSizes.z);
        // Visible height 1 meter in front
        var cameraView = 2.0f * Mathf.Tan(0.5f * Mathf.Deg2Rad * camera.fieldOfView);
        // Combined wanted distance from the object
        var distance = cameraDistance * objectSize / cameraView;
        // Estimated offset from the center to the outside of the object
        distance += 0.5f * objectSize;
        targetPosition = bounds.center - distance * camera.transform.forward;

        targetRotation = Quaternion.LookRotation(bounds.center - targetPosition);

        return true;
    }

    public static bool TryGetFocusTransforms(this Camera camera, GameObject[] targetGameObjects, out Vector3 targetPosition, out Quaternion targetRotation)
    {
        targetPosition = default;
        targetRotation = default;

        if (!targetGameObjects.TryGetBounds(out var bounds))
        {
            return false;
        }

        var objectSizes = bounds.max - bounds.min;
        var objectSize = Mathf.Max(objectSizes.x, objectSizes.y, objectSizes.z);
        var cameraView = 2.0f * Mathf.Tan(0.5f * Mathf.Deg2Rad * camera.fieldOfView);
        var distance = cameraDistance * objectSize / cameraView;
        distance += 0.5f * objectSize;
        targetPosition = bounds.center - distance * camera.transform.forward;

        targetRotation = Quaternion.LookRotation(bounds.center - targetPosition);

        return true;
    }
}