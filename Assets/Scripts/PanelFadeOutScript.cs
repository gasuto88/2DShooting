/*-------------------------------------------------
* FadeOutScript.cs
* 
* 作成日　2024/ 1/26
* 更新日　2024/ 1/30
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 画面をフェードアウトするクラス
/// </summary>
public class PanelFadeOutScript : MonoBehaviour
{

    #region フィールド変数

    [SerializeField, Header("フェードアウトする速度"), Range(0, 5)]
    private float _fadeOutSpeed = 0f;

    // フェードアウト画面
    private Image _fadeOutPanel = default;

    // ゲームの状態を管理するクラス
    private GameManagerScript _gameManagerScript = default;

    #endregion

    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Start()
    {
        // フェードアウト画面を取得
        _fadeOutPanel = GameObject.FindGameObjectWithTag("FadeOut").GetComponent<Image>();

        // GameManagerScriptを取得
        _gameManagerScript = GetComponent<GameManagerScript>();
    }

    /// <summary>
    /// 画面をフェードアウトさせる処理
    /// </summary>
    public void PanelFadeOut(Color color)
    {
        // 画面を暗くする
        _fadeOutPanel.color += color * _fadeOutSpeed * Time.deltaTime;

        // 暗くなったら
        if(1f <= _fadeOutPanel.color.a)
        {
            // タイトルに戻る
            _gameManagerScript.LoadTitleScene();
        }

    }
}