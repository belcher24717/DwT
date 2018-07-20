using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Tower : MonoBehaviour
{
    public AttackType AttackType;
    protected List<Enemy> _targetedEnemies;

    public ParticleSystem[] ShotEffects;
    public GameObject TurretSwivel;
    public Animator Animator;
    public int TurnSpeed = 200;
    public Material SelectedMaterial;
    private Dictionary<Renderer, Material> _baseMaterialDictionary;

    //upgradeable
    public int _fireRateUpgradeIndex { get; protected set; } = 0;
    public List<UpgradeStep> FireRateUpgrades;

    public int _rangeUpgradeIndex { get; protected set; } = 0;
    public List<UpgradeStep> RangeUpgrades;

    public int _damageUpgradeIndex { get; protected set; } = 0;
    public List<UpgradeStep> DamageUpgrades;

    public int _maxTargetEnemiesUpgradeIndex { get; protected set; } = 0;
    public List<UpgradeStep> MaxTargetEnemiesUpgrades;
    //upgradeable

    public int BaseCost;
    public string TowerName;

    public static Tower PlayerSelectedTower = null;

    private bool CanFace { get { return TurretSwivel != null; } }
    private bool CanAttack { get { return _isFacingTarget && _lastAttack.AddSeconds(FireRateUpgrades[_fireRateUpgradeIndex].Value) < DateTime.Now; } }
    private bool _isFacingTarget;
    private DateTime _lastAttack;
    private LineRenderer _radiusDisplay;
    private readonly int RADIUS_DISPLAY_SEGMENTS = 50;

    abstract public bool Attack();
    abstract public List<Enemy> PickEnemies();

    // can be overriden in multi-target situations 
    public void FaceEnemy()
    {
        if (!CanFace)
            return;

        if (_targetedEnemies.Count < 1 || _targetedEnemies[0] == null || !_targetedEnemies[0].isActiveAndEnabled)
        {
            _isFacingTarget = false;
            return;
        }

        var lookPos = transform.position - _targetedEnemies[0].transform.position;
        lookPos.y = 0;

        var rotation = Quaternion.LookRotation(lookPos) * Quaternion.Euler(-90, 0, 0);
        TurretSwivel.transform.rotation = Quaternion.Slerp(TurretSwivel.transform.rotation, rotation, Time.deltaTime * TurnSpeed);

        _isFacingTarget = Quaternion.Angle(TurretSwivel.transform.rotation, rotation) < 30;
    }

    private void TowerAttack()
    {
        if (!_isFacingTarget || !CanAttack)
            return;

        //try to attack
        if (Attack())
        {
            //we attacked, so play the effects
            if (ShotEffects != null)
                foreach (ParticleSystem p in ShotEffects)
                    p?.Play();

            _lastAttack = DateTime.Now;
        }
    }

    private bool TargetEnemyInRange(Enemy enemy)
    {
        return Vector3.Distance(enemy.gameObject.transform.position, transform.position) <= RangeUpgrades[_rangeUpgradeIndex].Value;
    }

    // Use this for initialization
    protected void Start()
    {
        _targetedEnemies = new List<Enemy>();
        _isFacingTarget = true;
        _lastAttack = DateTime.Now - TimeSpan.FromSeconds(FireRateUpgrades[_fireRateUpgradeIndex].Value);
        _radiusDisplay = this.gameObject.GetComponentInChildren<LineRenderer>();
        UpdateRadiusDisplay(RangeUpgrades[_rangeUpgradeIndex].Value);
        _baseMaterialDictionary = new Dictionary<Renderer, Material>();
        foreach (Renderer r in this.gameObject.GetComponentsInChildren<Renderer>())
            _baseMaterialDictionary.Add(r, r.material);
    }

    // Update is called once per frame
    protected void Update()
    {
        if (_targetedEnemies.Count < MaxTargetEnemiesUpgrades[_maxTargetEnemiesUpgradeIndex].Value)
            _targetedEnemies = PickEnemies();
        else
        {
            for (int i = 0; i < _targetedEnemies.Count; i++)
            {
                Enemy enemy = _targetedEnemies[i];

                if (enemy == null || !enemy.isActiveAndEnabled || !TargetEnemyInRange(enemy))
                {
                    _targetedEnemies = PickEnemies();
                    break;
                }
            }
        }

        if (CanFace)
            FaceEnemy();

        if (_targetedEnemies.Count > 0)
            TowerAttack();
    }

    public void UpgradeRange()
    {
        if (_rangeUpgradeIndex < RangeUpgrades.Count - 1)
            _rangeUpgradeIndex++;
        UpdateRadiusDisplay(RangeUpgrades[_rangeUpgradeIndex].Value);
    }

    public void UpgradeFireRate()
    {
        if (_fireRateUpgradeIndex < FireRateUpgrades.Count - 1)
            _fireRateUpgradeIndex++;
    }

    public void UpgradeDamage()
    {
        if (_damageUpgradeIndex < DamageUpgrades.Count - 1)
            _damageUpgradeIndex++;
    }

    public void UpgradeMaxTargetEnemies()
    {
        if (_maxTargetEnemiesUpgradeIndex < MaxTargetEnemiesUpgrades.Count - 1)
            _maxTargetEnemiesUpgradeIndex++;
    }


    public void ShowRadius()
    {
        _radiusDisplay.enabled = true;
    }
    public void HideRadius()
    {
        _radiusDisplay.enabled = false;
    }

    public void DeselectTower()
    {
        if (PlayerSelectedTower != null)
        {
            foreach (Renderer r in this.gameObject.GetComponentsInChildren<Renderer>())
                r.material = _baseMaterialDictionary[r];
            PlayerSelectedTower.HideRadius();
            UpgradeTower.Instance.HideUpgrades();
            PlayerSelectedTower = null;
            //set the towers "showing selected" properties back
        }
    }

    private void SelectTower()
    {
        PlayerSelectedTower = this;
        PlayerSelectedTower.GetComponentsInChildren<Renderer>().ToList().ForEach(child => child.material = SelectedMaterial);
        PlayerSelectedTower.ShowRadius();
        UpgradeTower.Instance.ShowUpgrades();
    }

    private void OnMouseDown()
    {
        // remove currently selected tower graphic
        Tower.PlayerSelectedTower?.DeselectTower();
        SelectTower();

        //update to show we're selected


        // add graphic for selection


        // alter UI (showing upgrades, etc)

    }

    void UpdateRadiusDisplay(float radius)
    {
        if (_radiusDisplay == null)
            return;

        _radiusDisplay.positionCount = RADIUS_DISPLAY_SEGMENTS + 1;

        float x;
        float y = 0.2f;
        float z;

        float angle = 20f;

        for (int i = 0; i < (RADIUS_DISPLAY_SEGMENTS + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            _radiusDisplay.SetPosition(i, new Vector3(x, y, z));

            angle += (360f / RADIUS_DISPLAY_SEGMENTS);
        }
    }

}

[System.Serializable]
public class UpgradeStep
{
    public int Cost;
    public float Value;
}

