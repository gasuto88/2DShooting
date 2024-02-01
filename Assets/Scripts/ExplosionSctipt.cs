/*-------------------------------------------------
* ExplosionSctipt.cs
* 
* 作成日　2024/ 1/23
* 更新日　2024/ 1/23
*
* 作成者　本木大地
-------------------------------------------------*/
using UnityEngine;

/// <summary>
/// 爆発させるクラス
/// </summary>
public class ExplosionSctipt : MonoBehaviour 
{
	#region 定数

	// 爆発画像
	private const int FOUR_EXPLOSION = 4;
	private const int LAST_EXPLOSION = 7;

	private const string PLAYER = "Player";
	private const string ENEMY = "Enemy";

    #endregion


    #region フィールド変数

    // --------------------------------------------

    [SerializeField,Header("爆発切り替え時間"),Range(0,100)]
	private float _explosionTime = default;

	[Header("【爆発画像】")]

	[SerializeField]
	private Sprite _explosionOne = default;
	[SerializeField]
	private Sprite _explosionTwo = default;
	[SerializeField]
	private Sprite _explosionThree = default;
	[SerializeField]
	private Sprite _explosionFour = default;
	[SerializeField]
	private Sprite _explosionFive = default;
	[SerializeField]
	private Sprite _explosionSix = default;
	[SerializeField]
	private Sprite _explosionSeven = default;
	[SerializeField]
	private Sprite _explosionEight = default;

	private Sprite[] _explosions = default;
	// ---------------------------------------------

	// タイマー
	private TimerScript _timerScript = default;

	// 爆発段階
	private int _explosionIndex = 0;

	// 爆発判定
	private bool isExplosionEnd = false;

	// プレイヤーを動かすクラス
	private PlayerMoveScript _playerMoveScript = default;

	// 敵を点滅させるクラス
	private FlashingEnemyScript _flashingEnemyScript = default;

	#endregion

	#region プロパティ

	public bool IsExplosionEnd { get => isExplosionEnd; set => isExplosionEnd = value; }

    #endregion

    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Start () 
	{
		// 爆発画像を設定
		_explosions = new Sprite[] {
			_explosionOne,_explosionTwo,_explosionThree,_explosionFour,
			_explosionFour,_explosionFive,_explosionSix,_explosionSeven,_explosionEight};

		// タイマー生成
		_timerScript = new TimerScript(_explosionTime,TimerScript.TimerState.End);

		// PlayerMoveScriptを取得
		_playerMoveScript = GameObject.FindGameObjectWithTag(PLAYER).GetComponent<PlayerMoveScript>();

		// FlashingEnemyScriptを取得
		_flashingEnemyScript = GameObject.FindGameObjectWithTag(ENEMY).GetComponent<FlashingEnemyScript>();


	}
	
	/// <summary>
	/// 爆発させる処理
	/// </summary>
	public void OnExplosion(SpriteRenderer explosion)
    {
		// タイマーが終了したら
		if(_timerScript.Execute() == TimerScript.TimerState.End)
        {
			// 爆発画像を設定
			explosion.sprite = _explosions[_explosionIndex];

			if(_explosionIndex == FOUR_EXPLOSION)
            {
				if(explosion.tag == PLAYER)
                {
					// プレイヤーを消す
					_playerMoveScript.DeletePlayer();
				}
				else if(explosion.tag == ENEMY)
                {
					// 敵を消す
					_flashingEnemyScript.DeleteEnemy();
				}
				
            }
			// 最後の画像だったら
			else if(LAST_EXPLOSION <= _explosionIndex)
            {
				// 透明にする
				explosion.sprite = null;

				// 爆発終了判定
				isExplosionEnd = true;
            }

			// タイマー初期化
			_timerScript.Reset();

			_explosionIndex++;
        }
    }
}