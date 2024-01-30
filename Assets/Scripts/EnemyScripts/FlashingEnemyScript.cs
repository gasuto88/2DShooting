/*-------------------------------------------------
* FlashingEnemyScript.cs
* 
* 作成日　2024/ 1/30
* 更新日　2024/ 1/30
*
* 作成者　本木大地
-------------------------------------------------*/
using UnityEngine;

/// <summary>
/// 敵を点滅させる
/// </summary>
public class FlashingEnemyScript : MonoBehaviour 
{

    #region 定数

    // プレイヤーの透明度
    private const float MAX_ALPHA = 1;
    private const float MIN_ALPHA = 0;

    #endregion

    #region フィールド変数

    [SerializeField, Header("被弾時のの点滅速度"), Range(0, 30)]
    private float _flashSpeed = 0f;

    [SerializeField, Header("被弾時の点滅回数"), Range(0, 15)]
    private int _flashMaxCount = 0;

    // 点滅終了判定
    private bool isFlashingEnd = false;

    // 点滅回数
    private int _flashCount = 0;

    // 点滅状態
    private FlashState _flashState = FlashState.OFF;

    // 透明度
    private SpriteRenderer _enemyAlpha = default;

    // 黒色
    private Color _black = Color.black;

    // 透明
    private Color _clear = Color.clear;

    private enum FlashState
    {
        ON,
        OFF
    }


    #endregion

    #region プロパティ

    public bool IsFlashing { get => isFlashingEnd; set => isFlashingEnd = value; }

    #endregion

    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Start()
    {
        // 敵のSpriteRendererを取得
        _enemyAlpha = GameObject.FindGameObjectWithTag("EnemyImage").GetComponent<SpriteRenderer>();
    }

    public void FlashingEnemy()
    {
        // 点滅状態
        switch (_flashState)
        {
            // 点ける
            case FlashState.ON:

                // 不透明にする
                _enemyAlpha.color += _black * _flashSpeed * Time.deltaTime;

                // 不透明になったら
                if (MAX_ALPHA <= _enemyAlpha.color.a)
                {
                    _flashState = FlashState.OFF;

                    _flashCount++;
                }

                break;

            // 消す
            case FlashState.OFF:

                // 透明にする
                _enemyAlpha.color -= _black * _flashSpeed * Time.deltaTime;

                // 透明になったら
                if (_enemyAlpha.color.a <= 0)
                {
                    _flashState = FlashState.ON;
                }

                break;
        }

        // 点滅が終わったら
        if (_flashMaxCount <= _flashCount)
        {
            // 初期化 -----------------------
            isFlashingEnd = false;
            _flashCount = 0;
            _flashState = FlashState.OFF;
            // ------------------------------
        }
    }
}
