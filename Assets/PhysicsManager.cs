using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 重力を「画面の奥（Z軸のプラス方向）」に設定
        // これで、ドラッグを離すと箱の底（画像）に向かって宝物が落ちます
        Physics.gravity = new Vector3(0, 0, 9.81f);
    }
}
