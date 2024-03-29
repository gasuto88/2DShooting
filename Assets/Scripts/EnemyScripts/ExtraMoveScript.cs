/*-------------------------------------------------
* ExtraMoveScript.cs
* 
* 作成日　2024/ 1/23
* 更新日　2024/ 1/23
*
* 作成者　本木大地
-------------------------------------------------*/
using UnityEngine;

/// <summary>
/// エクストラモードの挙動クラス
/// </summary>
public class ExtraMoveScript : EnemyMoveScript
{

    #region フィールド変数

    [Header("【EXTRA】")]
    [Space(10)]

    [SerializeField, Header("敵のHP"), Range(0, 1000)]
    private float _extraEnemyHp = 0f;

    [SerializeField, Header("敵の移動速度"), Range(0, 500)]
    private float _extraMoveSpeed = 0f;

    [SerializeField, Header("敵の回転速度"), Range(-500, 500)]
    private float _extraRotationSpeed = 0f;

    [SerializeField, Header("敵の弾の速度"), Range(0, 100)]
    private float _extraBallSpeed = default;

    [SerializeField, Header("敵の弾の回転速度"), Range(-50, 50)]
    private float _extraBallRotationSpeed = default;

    [SerializeField, Header("敵の弾の大きさ")]
    private Vector3 _extraBallScale = default;

    [SerializeField, Header("射撃のクールタイム"), Range(0, 2)]
    private float _extraShotCoolTime = default;

    #endregion

    /// <summary>
    /// 初期化処理
    /// </summary>
    public override void Init()
    {
        // 敵のHpを設定
        _enemyHpManagerScript.EnemyHp = _extraEnemyHp;

        // 敵の最大Hpを設定
        _enemyHpManagerScript.EnemyMaxHp = _extraEnemyHp;

        // 敵の移動速度を設定
        EnemyMoveSpeed = _extraMoveSpeed;

        // 敵の回転速度
        _enemyRotationSpeed = _extraRotationSpeed;

        // 弾の速度を設定
        _ballManagerScript.EnemyBallSpeed = _extraBallSpeed;

        // 弾の回転速度を設定
        _ballManagerScript.EnemyBallRotationSpeed = _extraBallRotationSpeed;

        // 弾の大きさを設定
        _ballManagerScript.BallScale = _extraBallScale;

        // 射撃のクールタイムを設定
        _shotCoolTime = _extraShotCoolTime;

        // 敵の目標座標を設定
        _destination = Destinations[PositionIndex];
    }

    /// <summary>
    /// 実行処理
    /// </summary>
    public override void Execute()
    {
        // 目標座標に移動する
        GoToTargetPosition();

        // Extra時の行動
        ExtraAction();

        // 目標座標に着いたら
        if (CheckArriveTargetPosition(_destination, _myTransform.position))
        {
            // 目標座標を変更
            ChengeTargetPosition(MINUS);
        }
    }

    /// <summary>
    /// Extra時の行動処理
    /// </summary>
    private void ExtraAction()
    {
        // 時間経過したら
        if (_shotTime <= 0f)
        {
            // ランダム角度
            int randomAngle = 0;

            // 弾を取り出す
            _ballManagerScript.BallOutput(_myTransform.position,
            _myTransform.rotation, ENEMY_BALL);

            // ランダム角度を取得
            randomAngle = Random.Range(0, 180);

            // 敵のZ軸を回転
            _myTransform.Rotate(Vector3.forward * randomAngle);

            // 射撃のクールタイムを設定
            _shotTime = _shotCoolTime;
        }

        // 時間を減算
        _shotTime -= Time.deltaTime;
    }
}