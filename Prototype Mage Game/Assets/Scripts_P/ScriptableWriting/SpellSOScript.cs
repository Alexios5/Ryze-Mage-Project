using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[CreateAssetMenu(fileName = "Spell", menuName = "Create Spell")]
public class SpellSOScript : ScriptableObject
{

    
    [Tooltip("Spell Type")]
    public TypeOfSpellSOScript CurrentType;

    [Tooltip("Spell Type")]
    public string NameId;


    [Tooltip("Pool Amount")]
    public int PoolAmount;

    public List<GameObject> SpellsPrefabsLevels;

    public List<InputSOScript> ComboSpell;




    

   




}
