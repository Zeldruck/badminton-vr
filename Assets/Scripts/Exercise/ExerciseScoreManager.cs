using UnityEngine;

public class ExerciseScoreManager : MonoBehaviour
{
    private int _goodPoints, _badPoints;
    private int _numberOfShot;

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
        // TODO
        Debug.Log("Exercise finished!");
    }
}
