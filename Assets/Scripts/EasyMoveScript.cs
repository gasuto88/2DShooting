/*-------------------------------------------------
* EasyMoveScript.cs
* 
* 作成日　2024/ 1/22
* 更新日　2024/ 1/22
*
* 作成者　本木大地
-------------------------------------------------*/
using UnityEngine;

public class EasyMoveScript : EnemyMoveScript
{

    #region フィールド変数

    [Space(10)]
    [Header("【Easy】")]
    [Space(10)]

    [SerializeField, Header("敵のHP"), Range(0, 1000)]
    private float _easyEnemyHp = 0f;

    [SerializeField, Header("敵の移動速度"), Range(0, 500)]
    private float _easyMoveSpeed = 0f;

    [SerializeField, Header("敵の回転速度"), Range(-500, 500)]
    private float _easyRotationSpeed = 0f;

    [SerializeField, Header("敵の弾の速度"), Range(0, 100)]
    private float _easyBallSpeed = default;

    [SerializeField, Header("敵の弾の回転速度"), Range(-22, 22)]
    private float _easyBallRotationSpeed = default;

    [SerializeField, Header("敵の弾の大きさ")]
    private Vector3 _easyBallScale = default;

    [SerializeField, Header("射撃のクールタイム"), Range(0, 2)]
    private float _easyShotCoolTime = default;

    [SerializeField, Header("射撃時間"), Range(0, 10)]
    protected float _shootingTime = 0f;

    [SerializeField, Header("待機時間"), Range(0, 10)]
    protected float _waitingTime = 0f;

    #endregion


    public override void Init()
    {
        //敵のHpを設定
        _enemyHpManagerScript.EnemyHp = _easyEnemyHp;

        // 敵の移動速度を設定
        _enemyMoveSpeed = _easyMoveSpeed;

        // 敵の回転速度
        _enemyRotationSpeed = _easyRotationSpeed;

        // 弾の速度を設定
        _ballManagerScript.EnemyBallSpeed = _easyBallSpeed;

        // 弾の回転速度を設定
        _ballManagerScript.EnemyBallRotationSpeed = _easyBallRotationSpeed;

        // 弾の大きさを設定
        _ballManagerScript.BallScale = _easyBallScale;

        // 射撃のクールタイムを設定
        _shotCoolTime = _easyShotCoolTime;

    　  
        if(PositionIndex == 0)
        { 
            // 敵の目標座標を設定
            _destination = Destinations[PositionIndex];
        }
        else
        {
            // 敵の目標座標を設定
            _destination = Destinations[3];
        }

        // タイマーを生成
        _timerScript = new TimerScript(_shootingTime);

        //// タイマー開始
        //_timerScript.TimerStart(_shootingTime);
    }

    public override void Execute()
    {       
        if (_timerScript.Execute() == TimerScript.TimerState.Execute)
        {
            // Easy時の行動
            EasyAction();
        }
        else if (_timerScript.Execute() == TimerScript.TimerState.End)
        {
            // 目標座標に移動する
            GoToTargetPosition();

            // 時間を減算
            //_waitTime -= Time.deltaTime;

            // 時間経過したら
            // 目標座標に着いたら
            if (//_waitTime <= 0f
                CheckArriveTargetPosition(_destination, _myTransform.position))
            {
                // 目標座標を変更
                ChengeTargetPosition(MINUS);

                // 弾の回転を反対にする
                _ballManagerScript.EnemyBallRotationSpeed *= -1;

                // 敵の回転を反対にする
                _enemyRotationSpeed *= -1;

                //待機時間を設定
                //_waitTime = _waitingTime;

                // タイマーを初期化
                _timerScript.Reset();
            }
        }
    }

    public override void Exit()
    {

    }

    /// <summary>
    /// Easy時の行動
    /// </summary>
    private void EasyAction()
    {
        // 敵のZ軸を回転
        _myTransform.Rotate(Vector3.forward * _enemyRotationSpeed * Time.deltaTime);

        // 時間経過したら
        if (_shotTime <= 0f)
        {
            for (int i = 0; i < 10; i++)
            {
                // 弾を取り出す
                _ballManagerScript.BallOutput(_myTransform.position,
            _myTransform.rotation, ENEMY_BALL);

                _myTransform.Rotate(Vector3.forward * 18f);
            }
            // 射撃のクールタイムを設定
            _shotTime = _shotCoolTime;

        }

        // 時間を減算
        _shotTime -= Time.deltaTime;
    }
}