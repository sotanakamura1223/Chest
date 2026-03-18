using UnityEngine;

public class Model : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float mZCoord;
    private Vector3 mOffset;
    private Rigidbody rb;

    // ドラッグ中に固定されるZ座標（高さ）を保持する変数
    private float currentGrabZ;

    // ドラッグ中に浮かせる高さ（カメラからの距離を調整）
    public float liftAmount = 2.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        // 1. カメラからオブジェクトまでの現在のスクリーン上の距離を取得
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        // 2. 物理演算をオフにする
        rb.isKinematic = true;

        // 3. マウスのワールド座標を取得し、オブジェクトとのズレ（オフセット）を計算
        // ここで「少し浮かせる」ために mZCoord を調整する
        mZCoord -= liftAmount;

        mOffset = gameObject.transform.position - GetMouseWorldPos();

        // 4. 現在の浮いた状態のZ座標を記録しておく
        currentGrabZ = gameObject.transform.position.z;
    }

    void OnMouseDrag()
    {
        // マウス位置に合わせて移動
        Vector3 currentMousePos = GetMouseWorldPos() + mOffset;

        // Z座標は「掴んだ時の高さ」で固定する（これでブレなくなります）
        currentMousePos.z = currentGrabZ;

        transform.position = currentMousePos;
    }

    void OnMouseUp()
    {
        Debug.Log("宝を掴みました！"); // これが出ないならColliderかRaycasterの問題
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        Debug.Log("カメラからの距離: " + mZCoord); // これが0だと動きません
                                           // ...残りの処理
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
