using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using Unity.VisualScripting;

public class FreeShotExercise : MonoBehaviour
{
    private GameObject _currSC;

    private bool _canShoot = false;
    private float _timerWaitNextShot = 0f;

    private float _nextShotArc, _nextShotStrength;

    [Header("Range")]
    [SerializeField] private float _xArc;
    [SerializeField] private float _minStrength;
    [SerializeField] private float _maxStrength;
    [SerializeField] private Transform _model;

    [Header("Util")]
    [SerializeField] private hitVolant _shuttleCockPrefab;
    [SerializeField] private ExerciseScoreManager _scoreManager;
    [SerializeField] private Transform _playerTransform;
    [Space]
    [SerializeField] private XRBaseController lController;
    [SerializeField] private XRBaseController rController;

    [Header("Exercise Parameters")]
    [SerializeField] private int _shuttleCockExerciseNumber;
    [SerializeField] private float _waitTimeBetweenScore;

    [Header("UI")]
    [SerializeField] private Transform _canvasUITransform;
    [SerializeField] private TMP_Text _nextShotTimeText;

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
    }

    private void Update()
    {
        if (!_canShoot && _timerWaitNextShot > 0f)
        {
            _timerWaitNextShot -= Time.deltaTime;
            _nextShotTimeText.text = ((int)(_timerWaitNextShot + 1)).ToString();
        }
        else if (!_canShoot)
        {
            _canShoot = true;
            _nextShotTimeText.text = "Shot!";
        }

        //_canvasUITransform.LookAt(_playerTransform);
    }

    [ContextMenu("Launch Exercise")]
    public void LaunchExercise()
    {
        StartCoroutine(SCExercise());
    }

    private IEnumerator SCExercise()
    {
        //var waitTime = new WaitForSeconds(_waitTimeBetweenScore);
        
        int scCount = 0;

        while (scCount < _shuttleCockExerciseNumber)
        {
            _canShoot = false;
            _timerWaitNextShot = _waitTimeBetweenScore;

            CalculateNextShot();

            // Then wait before sending another one
            yield return new WaitUntil(() => _canShoot);

            LaunchShuttleCock();

            scCount++;

            // Wait for sc to hit something and callback
            yield return new WaitUntil(() => _currSC == null);
        }

        _nextShotTimeText.text = "Finished!";
        _scoreManager.ExerciseFinished();
    }

    private void LaunchShuttleCock()
    {
        var nSC = Instantiate(_shuttleCockPrefab, transform.position, Quaternion.identity);
        nSC.left_controller = lController;
        nSC.right_controller = rController;
        
        var rot = -transform.rotation.eulerAngles.y + 90f;
        
        nSC.rb.velocity = (transform.forward + new Vector3(Mathf.Cos(Mathf.Deg2Rad * (_nextShotArc + rot)), 0f, Mathf.Sin(Mathf.Deg2Rad * (_nextShotArc + rot))).normalized).normalized * _nextShotStrength;
        _currSC = nSC.gameObject;
    }

    private void CalculateNextShot()
    {
        _nextShotStrength = Random.Range(_minStrength, _maxStrength);
        _nextShotArc = Random.Range(-_xArc, _xArc);

        _model.rotation = Quaternion.Euler(0f, -_nextShotArc, 0f);
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
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.25f);

        var rot = -transform.rotation.eulerAngles.y + 90f;
        
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(Mathf.Cos(Mathf.Deg2Rad * (_xArc + rot)), 0, Mathf.Sin(Mathf.Deg2Rad * (_xArc + rot))).normalized * _maxStrength);
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(Mathf.Cos(Mathf.Deg2Rad * (-_xArc + rot)), 0, Mathf.Sin(Mathf.Deg2Rad * (-_xArc + rot))).normalized * _maxStrength);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(Mathf.Cos(Mathf.Deg2Rad * (_xArc + rot)), 0, Mathf.Sin(Mathf.Deg2Rad * (_xArc + rot))).normalized * _minStrength);
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(Mathf.Cos(Mathf.Deg2Rad * (-_xArc + rot)), 0, Mathf.Sin(Mathf.Deg2Rad * (-_xArc + rot))).normalized * _minStrength);

        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
    }
}
