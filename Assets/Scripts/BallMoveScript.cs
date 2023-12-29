/*-------------------------------------------------
* BallMoveScript.cs
* 
* 作成日　2023/12/25
* 更新日　2023/12/29
*
* 作成者　本木大地
-------------------------------------------------*/
using UnityEngine;

/// <summary>
/// 弾を動かすクラス
/// </summary>
public class BallMoveScript : MonoBehaviour 
{

    #region フィールド変数

    // 自分のTransform
    protected Transform myTransform = default;

    // ゲームを管理するScript
    protected GameManagerScript _gameManagerScript = default;

    // 弾の個数を管理するSccript
    protected BallManagerScript _ballManagerScript = default;

    #endregion

    /// <summary>
    /// 更新前処理
    /// </summary>
    protected virtual void Start () 
	{
        // 自分のTransformを設定
        myTransform = transform;

        // GameManagerを取得
        GameObject gameMane = GameObject.FindGameObjectWithTag("GameManager");

        // GameManagerScriptを取得
        _gameManagerScript = gameMane.GetComponent<GameManagerScript>();

        // BallManagerScriptを取得
        _ballManagerScript = gameMane.GetComponent<BallManagerScript>();
    }

    /// <summary>
    /// 弾を動かす処理
    /// </summary>
    protected virtual void BallMove()
    {
        // 弾がステージ範囲外だったら
        if (_gameManagerScript.CheckOutStage(myTransform.position))
        {
            // 弾をしまう
            _ballManagerScript.BallInput(this);
        }
    }
}