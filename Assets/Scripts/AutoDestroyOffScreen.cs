using UnityEngine;

public class AutoDestroyOffScreen : MonoBehaviour
{
    new private Renderer renderer;
    public float activationDelay = 0.5f; // 延迟时间
    private bool isActivated = false;

    public float extraMargin = 5f; // 额外边距，使销毁范围比摄像机视野稍大

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        Invoke(nameof(Activate), activationDelay); // 延迟启用
    }

    private void Activate()
    {
        isActivated = true;
    }

    private void Update()
    {
        if (!isActivated)
            return;

        if (!IsVisibleFrom(renderer, Camera.main))
        {
            Destroy(gameObject);
        }
    }

    private bool IsVisibleFrom(Renderer renderer, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);

        // 扩展AABB包围盒的范围
        Bounds expandedBounds = renderer.bounds;
        expandedBounds.Expand(extraMargin);

        return GeometryUtility.TestPlanesAABB(planes, expandedBounds);
    }
}
