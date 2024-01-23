/*-------------------------------------------------
* NormalMoveScript.cs
* 
* 作成日　2024/ 1/23
* 更新日　2024/ 1/23
*
* 作成者　本木大地
-------------------------------------------------*/
using UnityEngine;

public class NormalMoveScript : EnemyMoveScript
{

    #region フィールド変数

    [Space(10)]
    [Header("【Normal】")]
    [Space(10)]

    [SerializeField, Header("敵のHP"), Range(0, 1000)]
    private float _normalEnemyHp = 0f;

    [SerializeField, Header("敵の移動速度"), Range(0, 500)]
    private float _normalMoveSpeed = 0f;

    [SerializeField, Header("敵の回転速度"), Range(-500, 500)]
    private float _normalRotationSpeed = 0f;

    [SerializeField, Header("敵の弾の速度"), Range(0, 100)]
    private float _normalBallSpeed = default;

    [SerializeField, Header("敵の弾の回転速度"), Range(-22, 22)]
    private float _normalBallRotationSpeed = default;

    [SerializeField, Header("敵の弾の大きさ")]
    private Vector3 _normalBallScale = default;

    [SerializeField, Header("射撃のクールタイム"), Range(0, 2)]
    private float _normalShotCoolTime = default;

    [SerializeField, Header("射撃時間"), Range(0, 10)]
    protected float _defaultCanShotTime = 0f;

    [SerializeField, Header("待機時間"), Range(0, 10)]
    protected float _defaultWaitTime = 0f;

    #endregion

    /// <summary>
    /// 初期化処理
    /// </summary>
    public override void Init()
    {
        //敵のHpを設定
         _enemyHpManagerScript.EnemyHp = _normalEnemyHp;

        // 敵の移動速度を設定
        _enemyMoveSpeed = _normalMoveSpeed;

        // 敵の回転速度
        _enemyRotationSpeed = _normalRotationSpeed;

        // 弾の速度を設定
        _ballManagerScript.EnemyBallSpeed = _normalBallSpeed;

        // 弾の回転速度を設定
        _ballManagerScript.EnemyBallRotationSpeed = _normalBallRotationSpeed;

        // 弾の大きさを設定
        _ballManagerScript.BallScale = _normalBallScale;

        // 射撃のクールタイムを設定
        _shotCoolTime = _normalShotCoolTime;

        // 敵の目標座標を設定
        _destination = Destinations[_positionIndex];
    }

    /// <summary>
    /// 実行処理
    /// </summary>
    public override void Execute()
    {
        if (0f < _canShotTime)
        {
            // 経過時間を減算
            _canShotTime -= Time.deltaTime;

            // Normal時の行動
            NormalAction();
        }
        else if (_canShotTime <= 0f)
        {
            // 目標座標に移動する
            GoToTargetPosition();

            // 時間を減算
            _waitTime -= Time.deltaTime;

            // 時間経過したら
            // 目標座標に着いたら
            if (_waitTime <= 0f
                && CheckArriveTargetPosition(_destination, _myTransform.position))
            {
                // 目標座標を変更
                ChengeTargetPosition(PLUS);

                // 弾の回転を反対にする
                _ballManagerScript.EnemyBallRotationSpeed *= -1;

                // 敵の回転を反対にする
                _enemyRotationSpeed *= -1;

                // 射撃ができる時間を設定
                _canShotTime = _defaultCanShotTime;

                // 待機時間を設定
                _waitTime = _defaultWaitTime;
            }
        }
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    public override void Exit()
    {

    }

    /// <summary>
    /// Normal時の行動
    /// </summary>
    private void NormalAction()
    {
        // 敵のZ軸を回転
        _myTransform.Rotate(Vector3.forward * _enemyRotationSpeed * Time.deltaTime);

        // 時間経過したら
        if (_shotTime <= 0f)
        {
            for (int i = 0; i < EIGHT_BALL; i++)
            {
                // 弾を取り出す
                _ballManagerScript.BallOutput(_myTransform.position,
                _myTransform.rotation, ENEMY_BALL);

                // 敵のZ軸を回転
                _myTransform.Rotate(Vector3.forward * 22.5f);
            }

            // 射撃のクールタイムを設定
            _shotTime = _shotCoolTime;
        }

        // 時間を減算
        _shotTime -= Time.deltaTime;
    }
}