/*-------------------------------------------------
* PlayerHpManagerScript.cs
* 
* 作成日　2024/ 1/26
* 更新日　2024/ 1/26
*
* 作成者　本木大地
-------------------------------------------------*/
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤーのHpを管理するクラス
/// </summary>
public class PlayerHpManagerScript : MonoBehaviour
{
    #region 定数

    // 残機の透明度
    private const float MAX_ALPHA = 1;
    private const float MIN_ALPHA = 0;

    #endregion

    #region フィールド変数

    [SerializeField, Header("プレイヤーの残機"), Range(0, 3)]
    private int _playerLife = 3;

    [SerializeField, Header("残機の点滅速度"), Range(0, 30)]
    private float _flashSpeed = 0f;

    [SerializeField, Header("残機の点滅回数"), Range(0, 20)]
    private int _flashMaxCount = 0;

    [SerializeField, Header("ダメージエフェクト速度"), Range(0, 20)]
    private float _damageEfectSpeed = 0f;

    [SerializeField, Header("ダメージエフェクト回数"), Range(0, 5)]
    private int _damageMaxCount = 0;

    // 残機UIの不透明度
    private SpriteRenderer[] _lifeAlphas = default;

    // プレイヤーの不透明度
    private SpriteRenderer _playerAlpha = default;

    private int _flashCount = 0;

    // ダメージエフェクト回数
    private int _damageCount = 0;

    // 点滅終了判定
    private bool isFlashingEnd = false;

    // ダメージエフェクトの終了判定
    private bool isDamageEfectEnd = false;

    // ゲームの状態を管理する
    private GameManagerScript _gameManagerScript = default;

    // ダメージの不透明度
    private Image _damageAlpha = default;

    private DamageState _damageState = DamageState.ON;

    // 点滅状態
    private FlashState _flashState = FlashState.OFF;

    // 黒色
    private Color _black = Color.black;

    // 透明
    private Color _clear = Color.clear;

    // 赤色
    private Color _red = new Color(1f, 0f, 0f, 0.3f);

    private enum FlashState
    {
        ON,
        OFF
    }
    private enum DamageState
    {
        ON,
        OFF
    }

    #endregion

    #region プロパティ

    public int PlayerLife { get => _playerLife; set => _playerLife = value; }

    public bool IsFlashingEnd { get => isFlashingEnd; set => isFlashingEnd = value; }

    public bool IsDamageEfectEnd { get => isDamageEfectEnd; set => isDamageEfectEnd = value; }

    #endregion

    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Start()
    {
        // プレイヤーの残機を取得
        SpriteRenderer lifeOne
            = GameObject.FindGameObjectWithTag("LifeOne").GetComponent<SpriteRenderer>();
        SpriteRenderer lifeTwo
            = GameObject.FindGameObjectWithTag("LifeTwo").GetComponent<SpriteRenderer>();
        SpriteRenderer lifeThree
            = GameObject.FindGameObjectWithTag("LifeThree").GetComponent<SpriteRenderer>();

        // プレイヤーの残機を設定
        _lifeAlphas = new SpriteRenderer[] { null, lifeOne, lifeTwo, lifeThree };

        // プレイヤーのSpriteRendererを取得
        _playerAlpha = GetComponent<SpriteRenderer>();

        // GameManagerScriptを取得
        _gameManagerScript
            = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();

        // ダメージエフェクトのImageを取得
        _damageAlpha
            = GameObject.FindGameObjectWithTag("DamagePanel").GetComponent<Image>();
    }

    /// <summary>
    /// プレイヤーの残機UIを点滅させる処理
    /// </summary>
    public void FlashingLifeUI()
    {
        // 点滅状態
        switch (_flashState)
        {
            // 点ける
            case FlashState.ON:

                // 不透明にする
                _lifeAlphas[_playerLife].color += _black * _flashSpeed * Time.deltaTime;

                // 不透明になったら
                if (MAX_ALPHA <= _lifeAlphas[_playerLife].color.a)
                {
                    _flashState = FlashState.OFF;

                    _flashCount++;
                }

                break;

            // 消す
            case FlashState.OFF:

                // 透明にする
                _lifeAlphas[_playerLife].color -= _black * _flashSpeed * Time.deltaTime;

                // 透明になったら
                if (_lifeAlphas[_playerLife].color.a <= MIN_ALPHA)
                {
                    _flashState = FlashState.ON;
                }

                break;
        }

        // 点滅が終わったら
        if (_flashMaxCount <= _flashCount)
        {
            // 透明にする
            _lifeAlphas[_playerLife].color = _clear;

            // プレイヤーの残機
            _playerLife--;

            // 初期化 -----------------------
            isFlashingEnd = false;
            _flashCount = 0;
            _flashState = FlashState.OFF;
            // ------------------------------
        }
    }

    /// <summary>
    /// ダメージエフェクトを点滅させる処理
    /// </summary>
    public void FlashingDamageEfect()
    {
        switch (_damageState)
        {
            // 点ける
            case DamageState.ON:

                // 赤色にする
                _damageAlpha.color += _red * _damageEfectSpeed * Time.deltaTime;

                // 赤色になったら
                if(0.3f <= _damageAlpha.color.a)
                {
                    _damageState = DamageState.OFF;
                }

                break;

            // 消す
            case DamageState.OFF:

                // 透明にする
                _damageAlpha.color -= _red * _damageEfectSpeed * Time.deltaTime;

                // 透明になったら
                if (_damageAlpha.color.a <= 0f)
                {
                    _damageState = DamageState.ON;

                    _damageCount++;
                }

                break;
        }     

        // 点滅が終わったら
        if (_damageMaxCount <= _damageCount)
        {
            // ダメージエフェクトの終了判定
            isDamageEfectEnd = false;

            // 初期化 ----------------------
            _damageAlpha.color = _clear;

            _damageCount = 0;

            _damageState = DamageState.ON;
            // ------------------------------
        }
    }

    /// <summary>
    /// プレイヤーのHpを削除する処理
    /// </summary>
    public void DeletePLayerHp()
    {
        // 透明にする
        _playerAlpha.color = _clear;
        _lifeAlphas[_playerLife].color = _clear;

        // ゲーム終了判定
        _gameManagerScript.IsGameOver = true;
    }
}