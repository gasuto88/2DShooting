/*-------------------------------------------------
* AudioManagerScript.cs
* 
* 作成日　2024/ 1/31
* 更新日　2024/ 2/ 1
*
* 作成者　本木大地
-------------------------------------------------*/
using UnityEngine;

/// <summary>
/// 音を管理するクラス
/// </summary>
public class AudioManagerScript : MonoBehaviour 
{

	#region フィールド変数

	// 効果音の音源
	private AudioSource _seAudioSource = default;

	[SerializeField, Header("選択ボタンの効果音	")]
	private AudioClip _seSelectButton = default;

	[SerializeField,Header("プレイヤーの射撃音")]
	private AudioClip _sePlayerShot = default;

	#endregion

	/// <summary>
    /// 更新前処理
    /// </summary>
	private void Start () 
	{
		// 効果音の音源
		_seAudioSource = GetComponent<AudioSource>();
	}
	
	/// <summary>
	/// 選択ボタンの効果音
	/// </summary>
	public void SESelectButton()
    {
		_seAudioSource.PlayOneShot(_seSelectButton);
    }
}