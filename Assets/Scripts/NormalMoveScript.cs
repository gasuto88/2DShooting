/*-------------------------------------------------
* NormalMoveScript.cs
* 
* 作成日　2024/ 1/23
* 更新日　2024/ 1/23
*
* 作成者　本木大地
-------------------------------------------------*/
using UnityEngine;

/// <summary>
/// ノーマルモードの挙動
/// </summary>
public class NormalMoveScript : EnemyMoveScript
{

    #region フィールド変数

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
    protected float _shootingTime = 0f;

    #endregion

    /// <summary>
    /// 初期化処理
    /// </summary>
    public override void Init()
    {
        //敵のHpを設定
         _enemyHpManagerScript.EnemyHp = _normalEnemyHp;

        // 敵の移動速度を設定
        EnemyMoveSpeed = _normalMoveSpeed;

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

        // タイマーを生成
        _timerScript = new TimerScript(_shootingTime);
    }

    /// <summary>
    /// 実行処理
    /// </summary>
    public override void Execute()
    {
        if (_timerScript.Execute() == TimerScript.TimerState.Execute)
        {
            // Normal時の行動
            NormalAction();
        }
        else if (_timerScript.Execute() == TimerScript.TimerState.End)
        {
            // 目標座標に移動する
            GoToTargetPosition();

            // 目標座標に着いたら
            if (CheckArriveTargetPosition(_destination, _myTransform.position))
            {
                // 目標座標を変更
                ChengeTargetPosition(PLUS);

                // 弾の回転を反対にする
                _ballManagerScript.EnemyBallRotationSpeed *= -1;

                // 敵の回転を反対にする
                _enemyRotationSpeed *= -1;

                // タイマー初期化
                _timerScript.Reset();
            }
        }
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