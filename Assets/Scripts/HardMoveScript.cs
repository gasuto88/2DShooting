/*-------------------------------------------------
* HardMoveScript.cs
* 
* 作成日　2024/ 1/23
* 更新日　2024/ 1/23
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using UnityEngine;


/// <summary>
/// ハードモードの挙動
/// </summary>
public class HardMoveScript : EnemyMoveScript
{

    #region フィールド変数

    [Header("【Hard】")]
    [Space(10)]

    [SerializeField, Header("敵のHP"), Range(0, 1000)]
    private float _hardEnemyHp = 0f;

    [SerializeField, Header("敵の移動速度"), Range(0, 500)]
    private float _hardMoveSpeed = 0f;

    [SerializeField, Header("敵の回転速度"), Range(-500, 500)]
    private float _hardRotationSpeed = 0f;

    [SerializeField, Header("敵の弾の速度"), Range(0, 100)]
    private float _hardBallSpeed = default;

    [SerializeField, Header("敵の弾の回転速度"), Range(-22, 22)]
    private float _hardBallRotationSpeed = default;

    [SerializeField, Header("敵の弾の大きさ")]
    private Vector3 _hardBallScale = default;

    [SerializeField, Header("射撃のクールタイム"), Range(0, 2)]
    private float _hardShotCoolTime = default;

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
        _enemyHpManagerScript.EnemyHp = _hardEnemyHp;

        // 敵の移動速度を設定
        _enemyMoveSpeed = _hardMoveSpeed;

        // 敵の回転速度
        _enemyRotationSpeed = _hardRotationSpeed;

        // 弾の速度を設定
        _ballManagerScript.EnemyBallSpeed = _hardBallSpeed;

        // 弾の回転速度を設定
        _ballManagerScript.EnemyBallRotationSpeed = _hardBallRotationSpeed;

        // 弾の大きさを設定
        _ballManagerScript.BallScale = _hardBallScale;

        // 射撃のクールタイムを設定
        _shotCoolTime = _hardShotCoolTime;
    }

    /// <summary>
    /// 実行処理
    /// </summary>
    public override void Execute()
    {
        //目標座標に移動する
        GoToTargetPosition();

        // Hard時の行動
        HardAction();

        // 目標座標に着いたら
        if (CheckArriveTargetPosition(_destination, _myTransform.position))
        {
            // 目標座標を変更
            ChengeTargetPosition(PLUS);
        }
    }

    /// <summary>
    /// Hard時の行動
    /// </summary>
    private void HardAction()
    {
        //// 敵のZ軸を回転
        _myTransform.Rotate(Vector3.forward * _enemyRotationSpeed * Time.deltaTime);

        // 時間経過したら
        if (_shotTime <= 0f)
        {
            //float angleSpace = 1f;

            for (int i = 0; i < 36; i++)
            {
                // 弾を取り出す
                _ballManagerScript.BallOutput(_myTransform.position,
                _myTransform.rotation, ENEMY_BALL);

                // 敵のZ軸を回転
                _myTransform.Rotate(Vector3.forward * 10f);
            }

            // 射撃のクールタイムを設定
            _shotTime = _shotCoolTime;
        }

        // 時間を減算
        _shotTime -= Time.deltaTime;
    }
}