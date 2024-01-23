/*-------------------------------------------------
* BallManagerScript.cs
* 
* 作成日　2023/12/25
* 更新日　2023/12/29
*
* 作成者　本木大地
-------------------------------------------------*/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 弾の個数を管理するクラス
/// </summary>
public class BallManagerScript : MonoBehaviour 
{
    #region 定数

    // 弾のTag
    private const string BALL = "Ball";

    // プレイヤーの弾のTag
    private const string PLAYER_BALL = "PlayerBall";

    // 敵の弾のTag
    private const string ENEMY_BALL = "EnemyBall";

    #endregion

    #region フィールド変数

    [SerializeField, Header("生成する数"), Range(0, 100)]
    private int _maxCount = 30;

    [SerializeField]
    private BallMoveScript _ballMoveScript = default;

    [SerializeField, Header("プレイヤーの弾の見た目")]
    private Sprite _playerBallImage = default;

    [SerializeField, Header("敵の弾の見た目")]
    private Sprite _enemyBallImage = default;

    // プレイヤーの弾の速度
    private float _playerBallSpeed = 0f;

    // プレイヤーの弾の大きさ
    private Vector3 _playerBallScale = default;

    // 敵の弾の速度
    private float _enemyBallSpeed = default;
    
    // 敵の弾の回転速度
    private float _enemyBallRotationSpeed = default;

    // 弾の大きさ
    private Vector3 _ballScale = default;

    private SpriteRenderer _playerRenderer = default;
    private SpriteRenderer _enemyRenderer = default;

    private Queue<BallMoveScript> _ballQueue = default;

    private Vector3 _setPosition = default;

    #endregion

    #region プロパティ
    public float PLayerBallSpeed 
    { get => _playerBallSpeed; set => _playerBallSpeed = value; }

    public Vector3 PlayerBallScale 
    { get => _playerBallScale; set => _playerBallScale = value; }

    public float EnemyBallSpeed 
    { get => _enemyBallSpeed; set => _enemyBallSpeed = value; }

    public float EnemyBallRotationSpeed 
    { get => _enemyBallRotationSpeed; set => _enemyBallRotationSpeed = value; }

    public Vector3 BallScale { get => _ballScale; set => _ballScale = value; }
    #endregion

    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Start()
    {
        // Queueを初期化       
        _ballQueue = new Queue<BallMoveScript>();

        // 指定した数Queueに格納する
        for (int i = 0; i < _maxCount; i++)
        {
            // 弾を生成
            BallMoveScript tempObj 
                = Instantiate(_ballMoveScript, _setPosition, Quaternion.identity, transform).GetComponent<BallMoveScript>();

            // 不可視化する
            tempObj.gameObject.SetActive(false);

            // 親子関係解除
            tempObj.transform.parent = null;

            // Queueに格納
            _ballQueue.Enqueue(tempObj);
        }

        // SpriteRendererを取得
        _playerRenderer
            = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        _enemyRenderer
            = GameObject.FindGameObjectWithTag("Enemy").GetComponent<SpriteRenderer>();

    }

    /// <summary>
    /// 弾を取り出す処理
    /// </summary>
    /// <param name="position">生成する座標</param>
    /// <returns></returns>
    public void BallOutput(Vector3 position,Quaternion rotation,string tagName)
    {
        // Queueの中身が空だったら追加で作る
        if (_ballQueue.Count <= 0)
        {
            // 弾を生成
            BallMoveScript tempScript
                = Instantiate(_ballMoveScript, _setPosition, 
                Quaternion.identity, transform).GetComponent<BallMoveScript>();

            // 不可視化する
            tempScript.gameObject.SetActive(false);

            // 親子関係解除
            tempScript.transform.parent = null;

            // Queueに格納
            _ballQueue.Enqueue(tempScript);
        }

        //  オブジェクトを一つ取り出す
        _ballMoveScript = _ballQueue.Dequeue();

        // 生成する座標を設定
        _ballMoveScript.transform.position = position;

        // 生成する座標を設定
        _ballMoveScript.transform.rotation = rotation;

        // オブジェクトを表示
        _ballMoveScript.gameObject.SetActive(true);

        // Tagを設定
        _ballMoveScript.tag = tagName;

        // 敵だったら
        if (tagName == ENEMY_BALL)
        {
            EnemyBallMoveScript _enemyBallMoveScript
                = _ballMoveScript.GetComponent<EnemyBallMoveScript>();

            // 弾にEnemyBallMoveScriptを有効にする
            _enemyBallMoveScript.enabled = true;

            // 敵の弾の回転速度を設定
            _enemyBallMoveScript.BallRotationSpeed = _enemyBallRotationSpeed;

            // 敵の弾の速度を設定
            _enemyBallMoveScript.BallSpeed = _enemyBallSpeed;

            // 弾の大きさを設定
            _enemyBallMoveScript.transform.localScale = _ballScale;

            // 弾の見た目を設定
            _enemyBallMoveScript.GetComponent<SpriteRenderer>().sprite = _enemyBallImage;
        }
        // プレイヤーだったら
        else if(tagName == PLAYER_BALL)
        {
            PlayerBallMoveScript _playerBallMoveScript
                = _ballMoveScript.GetComponent<PlayerBallMoveScript>();

            // プレイヤーの弾の速度を設定
            _playerBallMoveScript.PlayerBallSpeed = _playerBallSpeed;            

            // 弾にPlayerBallMoveScriptを有効にする
            _playerBallMoveScript.enabled = true;

            // 弾の大きさを設定
            _playerBallMoveScript.transform.localScale = _playerBallScale;

            // 弾の見た目を設定
            _playerBallMoveScript.GetComponent<SpriteRenderer>().sprite = _playerBallImage;
        }
    }

    /// <summary>
    /// 弾をしまう処理
    /// </summary>
    /// <param name="gameObject">しまう物</param>
    public void BallInput(BallMoveScript inputScript)
    {
        // TagをBallに設定
        inputScript.tag = BALL;

        // オブジェクトを非表示
        inputScript.gameObject.SetActive(false);

        // Queueに格納
        _ballQueue.Enqueue(inputScript);
    }
}