/*-------------------------------------------------
* TitleManagerScript.cs
* 
* 作成日　2024/ 1/25
* 更新日　2024/ 1/25
*
* 作成者　本木大地
-------------------------------------------------*/
using UnityEngine;

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

    #endregion

    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Start()
    {
        // 不可視化
        _modeSelectCanvas.enabled = false;
    }

    /// <summary>
    /// 難易度画面を表示する処理
    /// </summary>
    public void OnStart()
    {
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
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }

    /// <summary>
    /// タイトルに戻る処理
    /// </summary>
    public void OnBackTitle()
    {
        // 可視化
        _titleCanvas.enabled = true;

        // 不可視化
        _modeSelectCanvas.enabled = false;
    }
}