using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class TextLoadingAnimation : MonoBehaviour
{
    public int dotCount = 3;
    string _originalText;
    TextMeshProUGUI _tmComponent;
    Sequence _sequence;

    public void StartTextSequence()
    {
        _tmComponent = GetComponentInChildren<TextMeshProUGUI>(true);
        _originalText = _tmComponent.text;
        TextSequence(dotCount);
    }

    public void StopTextSequence()
    {
        if (_sequence != null && _sequence.IsPlaying())
            _sequence.Kill();
    }

    void TextSequence(int count)
    {
        _sequence = DOTween.Sequence();

        for (int i = 0; i < count; i++)
        {
            _sequence.AppendCallback(() => _tmComponent.text += ".");
            _sequence.AppendInterval(0.25f);
        }

        _sequence.AppendCallback(() => _tmComponent.text = _originalText);
        _sequence.AppendInterval(0.25f);
        _sequence.Play().SetLoops(-1);
    }
}
