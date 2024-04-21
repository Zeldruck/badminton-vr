using UnityEngine;
using UnityEngine.SceneManagement;

public class ExerciseScoreManager : MonoBehaviour
{
    private int _goodPoints, _badPoints;
    private int _numberOfShot;

    [SerializeField] private GameObject _finishMenu;
    [SerializeField] private TMPro.TMP_Text _scoreText;

    public void AddGoodPoint()
    {
        _goodPoints++;
        _numberOfShot++;
    }

    public void AddBadPoints()
    {
        _badPoints++;
        _numberOfShot++;
    }

    public void Reset()
    {
        _goodPoints = 0;
        _badPoints = 0;
        _numberOfShot = 0;
    }

    public void ExerciseFinished()
    {
        _finishMenu.SetActive(true);
        
#if  UNITY_EDITOR
        Debug.Log("Exercise finished!");
#endif
        
        _scoreText.text = $"{_goodPoints} / {_numberOfShot}";
    }

    public void MenuScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
