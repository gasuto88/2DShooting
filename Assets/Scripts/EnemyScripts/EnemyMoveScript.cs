/*-------------------------------------------------
* EnemyMoveScript.cs
* 
* 作成日　2023/12/28
* 更新日　2024/ 1/17
*
* 作成者　本木大地
-------------------------------------------------*/
using UnityEngine;

/// <summary>
/// 敵を動かす
/// </summary>
public abstract class EnemyMoveScript : MonoBehaviour
{
    #region 定数

    // 敵の弾のTag
    protected const string ENEMY_BALL = "EnemyBall";

    // 弾の数
    protected const float EIGHT_BALL = 8;

    // 符号 ----------------------------
    protected const int PLUS = 1;
    protected const int MINUS = -1;
    // ---------------------------------

    #endregion

    #region フィールド変数

    // 敵の移動速度
    protected float _enemyMoveSpeed = 0.3f;

    // 敵の回転速度
    protected float _enemyRotationSpeed = 0f;

    // 射撃のクールタイム
    protected float _shotCoolTime = 0f;

    // 射撃時の経過時間
    protected float _shotTime = 0f;

    // 射撃ができる経過時間
    protected float _canShotTime = 0f;

    // 待機経過時間
    protected float _waitTime = 0f;

    // 敵の場所の指数
    protected int _positionIndex = 0;

    // 値の初期化判定
    protected bool isDefaultValue = false;

    // 自分のTransform
    protected Transform _myTransform = default;

    // 敵の目的地
    protected Vector3 _destination = default;

    // ゲームを管理するScript
    protected GameManagerScript _gameManagerScript = default;

    // 弾の個数を管理するSccript
    protected BallManagerScript _ballManagerScript = default;

    // 敵のHpを管理するScript
    protected EnemyHpManagerScript _enemyHpManagerScript = default;

    // プレイヤーを動かすScript
    protected PlayerMoveScript _playerMoveScript = default;

    // タイマーを計るScript
    protected TimerScript _timerScript = default;

    // 敵の目的地
    protected Vector3[] _destinations = default;

    #endregion

    #region プロパティ

    public float EnemyMoveSpeed { get => _enemyMoveSpeed; set => _enemyMoveSpeed = value; }

    public bool IsDefaultValue { get => isDefaultValue; set => isDefaultValue = value; }

    public Vector3[] Destinations 
    { get => _destinations; set => _destinations = value; }

    protected int PositionIndex 
    { get => _positionIndex; set => _positionIndex = value; }

    #endregion

    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Start()
    {
        //自分のTransformを設定
        _myTransform = transform;

        // GameManagerを取得
        GameObject gameMane = GameObject.FindGameObjectWithTag("GameManager");

        // GameManagerScriptを取得
        _gameManagerScript = gameMane.GetComponent<GameManagerScript>();

        // BallManagerScriptを取得
        _ballManagerScript = gameMane.GetComponent<BallManagerScript>();

        // EnemyHpManagerScriptを取得
        _enemyHpManagerScript = GetComponent<EnemyHpManagerScript>();

        // PlayerMoveScriptを取得
        _playerMoveScript
            = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoveScript>();

        // 敵の目的地を取得
        Vector3 upPoint = GameObject.FindGameObjectWithTag("UpPoint").transform.position;
        Vector3 leftPoint = GameObject.FindGameObjectWithTag("LeftPoint").transform.position;
        Vector3 downPoint = GameObject.FindGameObjectWithTag("DownPoint").transform.position;
        Vector3 rightPoint = GameObject.FindGameObjectWithTag("RightPoint").transform.position;

        // 敵の目的地を設定
        _destinations
            = new Vector3[] { upPoint,leftPoint,downPoint, rightPoint};
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    public abstract void Init();

    /// <summary>
    /// 実行処理
    /// </summary>
    public abstract void Execute();

    /// <summary>
    /// 目的地に行く処理
    /// </summary>
    public void GoToTargetPosition()
    {
        // 目標方向
        Vector2 target = _destination - _myTransform.position;

        // 目標方向に移動
        _myTransform.Translate(
            target * _enemyMoveSpeed * Time.deltaTime, Space.World);
    }

    /// <summary>
    /// 目的地の到着判定
    /// </summary>
    /// <param name="targetPos">目的地</param>
    /// <param name="movePos">移動座標</param>
    /// <returns>到着判定</returns>
    public bool CheckArriveTargetPosition(Vector3 targetPos, Vector3 movePos)
    {
        
        if ((-1f < targetPos.x - movePos.x && targetPos.x - movePos.x < 1f)
            && (-1f < targetPos.y - movePos.y && targetPos.y - movePos.y < 1f))
        {
            
            return true;
        }

        return false;
    }

    /// <summary>
    /// 目的地を変更する処理
    /// </summary>
    protected void ChengeTargetPosition(int sign)
    {
        _positionIndex += sign;

        if (4 <= _positionIndex)
        {
            _positionIndex = 0;
        }
        else if (_positionIndex <= -1)
        {
            _positionIndex = 3;
        }

        switch (_positionIndex)
        {
            case 0:
                _destination = _destinations[_positionIndex];
                break;
            case 1:
                _destination = _destinations[_positionIndex];
                break;
            case 2:
                _destination = _destinations[_positionIndex];
                break;
            case 3:
                _destination = _destinations[_positionIndex];
                break;
        }


    }
}