using Unity.Rendering;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    public static RenderMesh BulletRenderer;

    [SerializeField]private RenderMesh _bulletRendrer;

    private void Awake()
    {
        BulletRenderer = _bulletRendrer;
    }
}
