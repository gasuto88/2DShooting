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

public class PlayerHpManagerScript : MonoBehaviour
{
    // 残機の透明度
    private const float MAX_ALPHA = 1;
    private const float MIN_ALPHA = 0;

    #region フィールド変数

    [SerializeField, Header("プレイヤーの残機"), Range(0, 3)]
    private int _playerLife = 3;

    [SerializeField, Header("残機の点滅速度"), Range(0, 10)]
    private float _flashSpeed = 0f;

    [SerializeField, Header("残機の点滅回数"), Range(0, 10)]
    private float _flashCount = 0f;

    [SerializeField,Header("ダメージエフェクト速度"),Range(0,10)]
    private float _damageEfectSpeed = 0f;

    // プレイヤーの残機
    private SpriteRenderer[] _playerLifes = default;

    // 残機の透明度
    private float _alpha = 1;

    private float _damageAlpha = 0f;

    private int _alphaCount = 0;

    // ダメージ判定
    private bool isDamage = false;

    // プレイヤー動かす
    private PlayerMoveScript _playerMoveScript = default;

    // ゲームの状態を管理する
    private GameManagerScript _gameManagerScript = default;

    private SpriteRenderer _playerSprite = default;

    private Image _damagePanel = default;

    private AlphaState _alphaState = AlphaState.Down;

    private DamageState _damageState = DamageState.UP;

    private enum AlphaState
    {
        UP,
        Down
    }

    private enum DamageState
    {
        UP,
        Down
    }

    #endregion

    #region プロパティ

    public int PlayerLife { get => _playerLife; set => _playerLife = value; }

    public bool IsDamage { get => isDamage; set => isDamage = value; }

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
        _playerLifes = new SpriteRenderer[] { null, lifeOne, lifeTwo, lifeThree };

        // PlayerMoveScriptを取得
        _playerMoveScript = GetComponent<PlayerMoveScript>();

        // GameManagerScriptを取得
        _gameManagerScript
            = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();

        // プレイヤーのSpriteRendererを取得
        _playerSprite = GetComponent<SpriteRenderer>();

        _damagePanel 
            = GameObject.FindGameObjectWithTag("DamagePanel").GetComponent<Image>();
    }

    /// <summary>
    /// プレイヤーの残機を表示する処理
    /// </summary>
    public void DisplayPlayerHp()
    {
        float playerAlpha = 0f;

        switch (_alphaState)
        {
            case AlphaState.UP:

                _alpha += _flashSpeed * Time.deltaTime;

                if (MAX_ALPHA <= _alpha)
                {
                    _alphaState = AlphaState.Down;
                }

                break;

            case AlphaState.Down:

                _alpha -= _flashSpeed * Time.deltaTime;

                if (_alpha <= MIN_ALPHA)
                {
                    _alphaCount++;

                    _alphaState = AlphaState.UP;
                }

                break;
        }

        // 残機の透明度を表示
        _playerLifes[_playerLife].color = new Color(1f, 1f, 1f, _alpha);

        playerAlpha = _alpha;

        // プレイヤーの透明度を表示
        _playerSprite.color = new Color(1f, 1f, 1f, playerAlpha);

        if (_flashCount <= _alphaCount)
        {
            // 残機を減算
            _playerLife--;

            // 残機がなくなったら
            if (_playerLife <= 0)
            {
                Debug.LogError("ゲームオーバー");

                // ゲーム終了判定
                _gameManagerScript.IsGameOver = true;
            }

            // 初期化
            _alphaCount = 0;

            _alpha = MAX_ALPHA;

            // プレイヤーの透明度を表示
            _playerSprite.color = new Color(1f, 1f, 1f, _alpha);

            // 衝突判定を初期化
            _playerMoveScript.IsCollision = false;
        }
    }

    public void DisplayDamageEfect()
    {
        switch (_damageState)
        {
            case DamageState.UP:

                _damageAlpha += _damageEfectSpeed * Time.deltaTime;

                if(0.3f <= _damageAlpha)
                {
                    _damageState = DamageState.Down;
                }

                break;

            case DamageState.Down:

                _damageAlpha -= _damageEfectSpeed * Time.deltaTime;
           
                break;
        }

        _damagePanel.color = new Color(1f, 0f, 0f, _damageAlpha);

        if (_damageAlpha <= 0f)
        {
            isDamage = false;

            _damageAlpha = 0f;

            _damagePanel.color = new Color(1f, 0f, 0f, _damageAlpha);

            _damageState = DamageState.UP;
        }
    }
}