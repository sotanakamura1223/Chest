using UnityEngine;

public class Treasure3D : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float maxDurability = 100f; // 最大耐久値
    private float currentDurability;

    // ダメージを受ける最小の衝撃（Rigidbodyの質量と速度に依存）
    public float damageThreshold = 1.0f;

    // ダメージの倍率
    public float damageMultiplier = 5.0f;

    public bool isBroken = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentDurability = maxDurability;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // マウスで掴んでいる間（isKinematic=true）は衝撃を無視
        // また、すでに壊れている場合も無視
        if (rb.isKinematic || isBroken) return;

        // ぶつかった時の衝撃力（力積）を取得
        // velocity（速度）よりも impulse（力積）の方が、
        // 物体の「重さ」も考慮されるため、感覚的な「ぶつけ方」に近い判定ができます
        float impactForce = collision.impulse.magnitude / Time.fixedDeltaTime;

        if (impactForce > damageThreshold)
        {
            float damage = (impactForce - damageThreshold) * damageMultiplier;
            TakeDamage(damage);
        }
    }

    void TakeDamage(float amount)
    {
        currentDurability -= amount;

        // 耐久値を画面上のUI（HPゲージなど）に反映させるためのイベント（オプション）
        // OnDurabilityChanged?.Invoke(currentDurability / maxDurability);

        if (currentDurability <= 0)
        {
            currentDurability = 0;
            if (!isBroken)
            {
                isBroken = true;
                BreakTreasure();
            }
        }
        else
        {
            Debug.Log($"宝物に衝撃！ 耐久残り: {currentDurability:F1}");
        }
    }

    void BreakTreasure()
    {
        Debug.Log("<color=red>宝物が壊れた！冒険者は落胆するだろう...</color>");

        // 演出：モデルを「破片」に差し替える、または
        // Materialの色を暗くする、などの処理
        GetComponent<MeshRenderer>().material.color = Color.gray;
    }
}
