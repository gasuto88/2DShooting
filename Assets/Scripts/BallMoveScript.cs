/*-------------------------------------------------
* BallMoveScript.cs
* 
* 作成日　2023/12/25
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

	[SerializeField,Header("弾の速度"),Range(0,100)]
	private float _ballSpeed = default;

	private Transform myTransform = default;

	private GameManagerScript _gameManagerScript = default;

	private BallPoolScript _ballPoolScript = default;

	#endregion

	/// <summary>
    /// 更新前処理
    /// </summary>
	private void Start () 
	{
		// 自分のTransformを設定
		myTransform = transform;

		// GameManagerScriptを取得
		_gameManagerScript
			= GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();

		// BallPoolScriptを取得
		_ballPoolScript = GameObject.FindGameObjectWithTag("Player").GetComponent<BallPoolScript>();
	}

	/// <summary>
	/// 更新処理
	/// </summary>
    private void Update()
    {
		BallMove();
	}
	/// <summary>
	/// 弾を動かす処理
	/// </summary>
    public void BallMove()
    {
		myTransform.Translate(Vector2.up * _ballSpeed * Time.deltaTime);

        if (_gameManagerScript.CheckOutStage(myTransform.position))
        {
			_ballPoolScript.Input(myTransform.gameObject);
        }
    }
}