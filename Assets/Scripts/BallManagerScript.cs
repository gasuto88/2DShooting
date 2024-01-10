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

/// <summary>
/// 弾の個数を管理するクラス
/// </summary>
public class BallManagerScript : MonoBehaviour 
{
    #region 定数

    // 弾のTag
    private const string BALL = "Ball";

    #endregion

    #region フィールド変数

    [SerializeField]
    private BallMoveScript _ballObject = default;

    private Queue<BallMoveScript> _ballQueue = default;

    [SerializeField, Header("生成する数"),Range(0,100)]
    private int _maxCount = 30;

    private Vector3 _setPosition = default;

    #endregion

    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Start()
    {
        // Queueを初期化       
        _ballQueue = new Queue<BallMoveScript>();

        // 指定した数分Queueに格納する
        for (int i = 0; i < _maxCount; i++)
        {
            // 弾を生成
            BallMoveScript tempObj 
                = Instantiate(_ballObject, _setPosition, Quaternion.identity, transform).GetComponent<BallMoveScript>();

            // 不可視化する
            tempObj.gameObject.SetActive(false);

            // 親子関係解除
            tempObj.transform.parent = null;

            // Queueに格納
            _ballQueue.Enqueue(tempObj);
        }
    }

    /// <summary>
    /// 弾を取り出す処理
    /// </summary>
    /// <param name="position">生成する座標</param>
    /// <returns></returns>
    public BallMoveScript BallOutput(Vector3 position,Quaternion rotation)
    {
        // Queueの中身が空だったら追加で作る
        if (_ballQueue.Count <= 0)
        {
            // 弾を生成
            BallMoveScript tempScript
                = Instantiate(_ballObject, _setPosition, Quaternion.identity, transform).GetComponent<BallMoveScript>();

            // 不可視化する
            tempScript.gameObject.SetActive(false);

            // 親子関係解除
            tempScript.transform.parent = null;

            // Queueに格納
            _ballQueue.Enqueue(tempScript);
        }

        //  オブジェクトを一つ取り出す
        _ballObject = _ballQueue.Dequeue();

        // 生成する座標を設定
        _ballObject.transform.position = position;

        // 生成する座標を設定
        _ballObject.transform.rotation = rotation;

        // オブジェクトを表示
        _ballObject.gameObject.SetActive(true);

        return _ballObject;
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