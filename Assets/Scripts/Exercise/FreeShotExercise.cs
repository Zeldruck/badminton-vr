using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FreeShotExercise : MonoBehaviour
{
    private GameObject _currSC;

    [Header("Range")]
    [SerializeField] private float _xArc;
    [SerializeField] private float _minStrength;
    [SerializeField] private float _maxStrength;

    [Header("Util")]
    [SerializeField] private SCExercise _shuttleCockPrefab;
    [SerializeField] private ExerciseScoreManager _scoreManager;

    [Header("Exercise Parameters")]
    [SerializeField] private int _shuttleCockExerciseNumber;
    [SerializeField] private float _waitTimeBetweenScore;

    private void Awake()
    {
        _scoreManager = _scoreManager ?? FindAnyObjectByType<ExerciseScoreManager>();
    }

    private void Start()
    {
        var fieldManager = FieldManager.instance;

        fieldManager._offFieldEvent += (side) => ShotResult(side, FiledColliderEvent.EFieldType.OffField);
        fieldManager._inLeftFieldEvent += (side) => ShotResult(side, FiledColliderEvent.EFieldType.InLeftField);
        fieldManager._inRightFieldEvent += (side) => ShotResult(side, FiledColliderEvent.EFieldType.InRightField);
        fieldManager._inFrontFieldEvent += (side) => ShotResult(side, FiledColliderEvent.EFieldType.InFrontField);
        fieldManager._inBackFieldEvent += (side) => ShotResult(side, FiledColliderEvent.EFieldType.InBackField);
        fieldManager._netEvent += () => ShotResult(-1, FiledColliderEvent.EFieldType.Net);

        StartCoroutine(SCExercise());
    }

    private IEnumerator SCExercise()
    {
        var waitTime = new WaitForSeconds(_waitTimeBetweenScore);
        
        int scCount = 0;

        while (scCount < _shuttleCockExerciseNumber)
        {
            LaunchShuttleCock();

            // Wait for sc to hit something and callback
            yield return new WaitUntil(() => _currSC == null);

            // Then wait before sending another one
            yield return waitTime;

            scCount++;
        }
    }

    private void LaunchShuttleCock()
    {
        float randomStrength = Random.Range(_maxStrength, _maxStrength + _minStrength);
        float randomArc = Random.Range(-_xArc, _xArc);

        var nSC = Instantiate(_shuttleCockPrefab, transform.position, Quaternion.identity);
        nSC.Rb.velocity = (transform.forward + new Vector3(Mathf.Cos(Mathf.Deg2Rad * (randomArc + 90f)), 0, Mathf.Sin(Mathf.Deg2Rad * (randomArc + 90f))).normalized).normalized * randomStrength;
        _currSC = nSC.gameObject;
    }

    private void ShotResult(int side, FiledColliderEvent.EFieldType eType)
    {
        bool isGoodSide = side == 1;

        switch (eType)
        {
            case FiledColliderEvent.EFieldType.OffField:
                BadShot();
                break;

            case FiledColliderEvent.EFieldType.InLeftField:
                if (isGoodSide) GoodShot();
                else MissedShot();
                break;

            case FiledColliderEvent.EFieldType.InRightField:
                if (isGoodSide) GoodShot();
                else MissedShot();
                break;

            case FiledColliderEvent.EFieldType.InFrontField:
                if (isGoodSide) GoodShot();
                else MissedShot();
                break;

            case FiledColliderEvent.EFieldType.InBackField:
                if (isGoodSide) GoodShot();
                else MissedShot();
                break;

            case FiledColliderEvent.EFieldType.Net:
                BadShot();
                break;
        }

        Destroy(_currSC);
    }

    private void GoodShot()
    {
        _scoreManager.AddGoodPoint();

        Debug.Log("Good Shot!");
    }

    private void BadShot()
    {
        _scoreManager.AddBadPoints();

        Debug.Log("Bad Shot!");
    }

    private void MissedShot()
    {
        _scoreManager.AddBadPoints();

        Debug.Log("Shot Missed!");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.25f);

        Gizmos.DrawLine(transform.position, transform.position + new Vector3(Mathf.Cos(Mathf.Deg2Rad * (_xArc + 90f)), 0, Mathf.Sin(Mathf.Deg2Rad * (_xArc + 90f))).normalized * _maxStrength);
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(Mathf.Cos(Mathf.Deg2Rad * (-_xArc + 90f)), 0, Mathf.Sin(Mathf.Deg2Rad * (-_xArc + 90f))).normalized * _maxStrength);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(Mathf.Cos(Mathf.Deg2Rad * (_xArc + 90f)), 0, Mathf.Sin(Mathf.Deg2Rad * (_xArc + 90f))).normalized * _minStrength);
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(Mathf.Cos(Mathf.Deg2Rad * (-_xArc + 90f)), 0, Mathf.Sin(Mathf.Deg2Rad * (-_xArc + 90f))).normalized * _minStrength);

        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
    }
}
