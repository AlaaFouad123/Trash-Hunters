using System.Collections;
using TMPro;
using UnityEngine;

public class TextAnim : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    [SerializeField][TextArea] private string[] stringArray;

    [SerializeField] private float timeBtwnChars;
    [SerializeField] private float timeBtwnWords;

    private int i = 0;

    private void Start()
    {
        EndCheck();
    }

    public void EndCheck()
    {
        if (i <= stringArray.Length - 1)
        {
            _textMeshPro.text = stringArray[i];
            StartCoroutine(TextVisible());
        }
    }

    private IEnumerator TextVisible()
    {
        _textMeshPro.ForceMeshUpdate();
        int totalVisibleCharacters = _textMeshPro.textInfo.characterCount;
        int counter = 0;

        while (true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            _textMeshPro.maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalVisibleCharacters)
            {
                i += 1;
                Invoke(nameof(EndCheck), timeBtwnWords);
                break;
            }

            counter += 1;
            yield return new WaitForSeconds(timeBtwnChars);
        }
    }
}