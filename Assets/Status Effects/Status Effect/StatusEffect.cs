using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;

public class StatusEffect : MonoBehaviour
{
    public float Duration;
    public float EffectPercent;
    public MonoBehaviour ObjectToAffect;
    public string Id;

    [HideInInspector]
    public string TypeToAffect;
    [HideInInspector]
    public int TypeChoiceIndex;
    [HideInInspector]
    public string FieldToAffect;
    [HideInInspector]
    public int FieldChoiceIndex;

    private float _effectValue;
    private DateTime _expireTime;

    // Use this for initialization
    void Start()
    {
        var myEffects = this.gameObject.transform.parent.GetComponentsInChildren<StatusEffect>().Where(e => e.Id == Id);

        //I'm the only affect
        if (myEffects.Count() == 1)
        {
            ApplyEffect();
            _expireTime = DateTime.Now.AddSeconds(Duration);
        }
        else
        {
            myEffects.Where(e => e != this).FirstOrDefault()?.Refresh();
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if it's expired
        if(_expireTime <= DateTime.Now)
        {
            //remove from parent, delete myself
            RemoveEffect();
            Destroy(this.gameObject);
        }
    }

    protected void RemoveEffect()
    {
        FieldInfo field = Type.GetType(TypeToAffect).GetField(FieldToAffect);
        var scriptToAffect = this.gameObject.transform.parent.GetComponent(TypeToAffect);
        if (scriptToAffect != null)
        {
            float fieldValue = Convert.ToSingle(field.GetValue(scriptToAffect));
            field.SetValue(scriptToAffect, Convert.ChangeType(fieldValue - _effectValue, field.FieldType));
        }
    }

    protected void ApplyEffect()
    {
        FieldInfo field = Type.GetType(TypeToAffect).GetField(FieldToAffect);
        var scriptToAffect = this.gameObject.transform.parent.GetComponent(TypeToAffect);

        if (scriptToAffect != null)
        {
            float fieldValue = Convert.ToSingle(field.GetValue(scriptToAffect));
            _effectValue = fieldValue * (EffectPercent/100);
            field.SetValue(scriptToAffect, Convert.ChangeType(fieldValue + _effectValue, field.FieldType));
        }
    }

    public void Refresh()
    {
        _expireTime = DateTime.Now.AddSeconds(Duration);
    }
}
