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

public class FadeOutScript : MonoBehaviour
{

    #region フィールド変数

    [SerializeField, Header("フェードアウトする速度"), Range(0, 5)]
    private float _fadeOutSpeed = 0f;

    private Image _fadeOutPanel = default;

    private float _fadeOutAlpha = 0f;

    // ゲームの状態を管理する
    private GameManagerScript _gameManagerScript = default;

    #endregion

    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Start()
    {
        _fadeOutPanel = GameObject.FindGameObjectWithTag("FadeOut").GetComponent<Image>();

        _gameManagerScript = GetComponent<GameManagerScript>();
    }

    public void FadeOut()
    {
        _fadeOutPanel.color = new Color(0f, 0f, 0f, _fadeOutAlpha);

        _fadeOutAlpha += _fadeOutSpeed * Time.deltaTime;

        if(1f <= _fadeOutAlpha)
        {
            _gameManagerScript.OnExit();
        }

    }
}