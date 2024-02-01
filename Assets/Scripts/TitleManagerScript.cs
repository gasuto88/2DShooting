/*-------------------------------------------------
* TitleManagerScript.cs
* 
* 作成日　2024/ 1/25
* 更新日　2024/ 1/25
*
* 作成者　本木大地
-------------------------------------------------*/
using UnityEngine;
using System.Collections;

/// <summary>
/// タイトルクラス
/// </summary>
public class TitleManagerScript : MonoBehaviour 
{

	#region フィールド変数

	[SerializeField]
	private Canvas _titleCanvas = default;
	[SerializeField]
	private Canvas _modeSelectCanvas = default;

    // 効果音
    private AudioSource _seAudioSouse = default;

    [SerializeField,Header("ボタン音")]
    private AudioClip _seButton = default;

    #endregion

    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Start()
    {
        // 不可視化
        _modeSelectCanvas.enabled = false;

        // 音源取得
        _seAudioSouse = GameObject.FindGameObjectWithTag("SEManager").GetComponent<AudioSource>();
    }

    /// <summary>
    /// 難易度画面を表示する処理
    /// </summary>
    public void OnStart()
    {
        // ボタン音再生
        SEButtonClick();

        // 不可視化
        _titleCanvas.enabled = false;

        // 可視化
        _modeSelectCanvas.enabled = true;
    }

    /// <summary>
    /// ゲームを終了する処理
    /// </summary>
	public void OnExit()
    {
        // ボタン音再生
        SEButtonClick();

        StartCoroutine(ExitCoroutine());
    }

    /// <summary>
    /// タイトルに戻る処理
    /// </summary>
    public void OnBackTitle()
    {
        // ボタン音再生
        SEButtonClick();

        // 可視化
        _titleCanvas.enabled = true;

        // 不可視化
        _modeSelectCanvas.enabled = false;
    }

    /// <summary>
    /// ボタンクリック音を再生
    /// </summary>
    public void SEButtonClick()
    {
        // 再生
        _seAudioSouse.PlayOneShot(_seButton);
    }

    /// <summary>
    /// 遅延処理
    /// </summary>
    private IEnumerator ExitCoroutine()
    {
        yield return new WaitForSeconds(0.4f);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }
}