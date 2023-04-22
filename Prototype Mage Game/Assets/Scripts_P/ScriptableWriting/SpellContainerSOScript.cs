using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell Container", menuName = "Create Spell Container")]
public class SpellContainerSOScript : ScriptableObject
{
    public List<SpellSOScript> Spells;
}
