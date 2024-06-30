using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDiscribbe : MonoBehaviour
{
    [SerializeField] NDTDEvent ndtdEvent;

    void OnDisable()
    {
        if (ndtdEvent != null)
        {
            Invoke("SkillDiscribe", 0.5f);
        }
    }
    private void SkillDiscribe()
    {
        Debug.Log("SkillDiscribeフラグ");
        //スキル説明
        ndtdEvent.Skilldiscribe();
    }
}
