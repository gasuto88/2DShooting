/*-------------------------------------------------
* FadeOutScript.cs
* 
* 作成日　2024/ 1/26
* 更新日　2024/ 1/26
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 画面をフェードアウトする
/// </summary>
public class PanelFadeOutScript : MonoBehaviour
{

    #region フィールド変数

    [SerializeField, Header("フェードアウトする速度"), Range(0, 5)]
    private float _fadeOutSpeed = 0f;

    // フェードアウト画面
    private Image _fadeOutPanel = default;

    // 透明度
    private float _fadeOutAlpha = 0f;

    // ゲームの状態を管理する
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
    public void PanelFadeOut()
    {
        _fadeOutPanel.color = new Color(0f, 0f, 0f, _fadeOutAlpha);

        _fadeOutAlpha += _fadeOutSpeed * Time.deltaTime;

        if(1f <= _fadeOutAlpha)
        {
            _gameManagerScript.OnExit();
        }

    }
}