/*-------------------------------------------------
* BallPoolScript.cs
* 
* 作成日　2023/12/25
* 更新日　2023/12/25
*
* 作成者　本木大地
-------------------------------------------------*/
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弾をため込むクラス
/// </summary>
public class BallPoolScript : MonoBehaviour 
{
    #region フィールド変数

    [SerializeField]
    private GameObject _ballObject = default;

    private Queue<GameObject> _ballQueue = default;

    [SerializeField, Header("生成する数")]
    private int _maxCount = 30;

    private Vector3 _setPosition = default;

    #endregion

    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Start()
    {
        // Queueを初期化       
        _ballQueue = new Queue<GameObject>();

        // 指定した数分Queueに格納する
        for (int i = 0; i < _maxCount; i++)
        {
            // 弾を生成
            GameObject tempObj 
                = Instantiate(_ballObject, _setPosition, Quaternion.identity, transform);

            // 不可視化する
            tempObj.SetActive(false);

            // 親子関係解除
            tempObj.transform.parent = null;

            // Queueに格納
            _ballQueue.Enqueue(tempObj);
        }
    }

    /// <summary>
    /// 弾を取り出す処理
    /// </summary>
    /// <param name="_position">生成する座標</param>
    /// <returns></returns>
    public void Output(Vector3 _position)
    {
        // Queueの中身が空だったら追加で作る
        if (_ballQueue.Count <= 0)
        {
            // 弾を生成
            GameObject tempObj 
                = Instantiate(_ballObject, _setPosition, Quaternion.identity, transform);

            // 不可視化する
            tempObj.SetActive(false);

            // 親子関係解除
            tempObj.transform.parent = null;

            // Queueに格納
            _ballQueue.Enqueue(tempObj);
        }

        //  オブジェクトを一つ取り出す
        _ballObject = _ballQueue.Dequeue();

        // 生成する座標を設定
        _ballObject.transform.position = _position;

        // オブジェクトを表示
        _ballObject.gameObject.SetActive(true);
    }

    /// <summary>
    /// 弾をしまう処理
    /// </summary>
    /// <param name="gameObject">しまう物</param>
    public void Input(GameObject gameObject)
    {
        // オブジェクトを非表示
        gameObject.SetActive(false);

        // Queueに格納
        _ballQueue.Enqueue(gameObject);
    }
}