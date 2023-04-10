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


    public List<InputsCombo> ComboSpell;




    [System.Serializable]
    public struct InputsCombo
    {
        public string NameInput;
        public InputActionReference currentActionReference;
        
        public Animation ComboAnimation;
    }




}
