using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;
using UnityEngine.Serialization;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace MoreMountains.CorgiEngine
{
    public class CharacterHandleSpells : CharacterAbility
    {

        [SerializeField] private float ComboTimeMax = 1.0f;
        [SerializeField] private List<string> InputComboSequence;
        [SerializeField] private int SpellLevelIndicator = 0;


        public List<SpellSOScript> CurrentSpells;


        private SpellSOScript SpellToCast;
        private float Timer;
        private bool InitiateSequence = false;
        private List<CombosBasicInfo> CombosCounter;
        private List<SpellPool> AllPools;

        private class CombosBasicInfo
        {
            public SpellSOScript SpellToCast;
            public int Value;

            public CombosBasicInfo(SpellSOScript spellToCast, int value)
            {
                SpellToCast = spellToCast;
                Value = value;
            }
        }


        private struct SpellPool
        {
            public string PoolSpellNameId;
            public List<GameObject> SpellPooledObjects;

        }


        protected override void Initialization()
        {
            base.Initialization();
            InputComboSequence = new List<string>();
            CombosCounter = new List<CombosBasicInfo>();
            for (int i=0;i< CurrentSpells.Count;i++)
            {
                CombosBasicInfo currentComboInfo = new CombosBasicInfo(CurrentSpells[i],0);
                
                CombosCounter.Add(currentComboInfo);
            }

            //Setup Pools

            AllPools = new List<SpellPool>();
            for (int j=0;j<CurrentSpells.Count;j++)
            {
                SpellPool newPool = new SpellPool();
                newPool.PoolSpellNameId = CurrentSpells[j].NameId;
                newPool.SpellPooledObjects = new List<GameObject>();
                GameObject tmp;
                for (int i=0;i< CurrentSpells[j].PoolAmount;i++)
                {
                    
                    tmp = Instantiate(CurrentSpells[j].SpellsPrefabsLevels[SpellLevelIndicator]);
                    tmp.SetActive(false);
                    newPool.SpellPooledObjects.Add(tmp);


                }
                AllPools.Add(newPool);
            }

            


        }

        public override void ProcessAbility()
        {
            base.ProcessAbility();



            if (InitiateSequence)
            {
                Timer += Time.deltaTime;
                Debug.Log("Timer : " + Timer);
                if (Timer>= ComboTimeMax)
                {
                    Timer = 0;
                    InitiateSequence = false;
                    CheckComboMatch();
                }                
            }

           

        }
		protected override void HandleInput()
		{

            if (CheckComboButtonsPressed())
            {
                Timer = 0;
                InitiateSequence = true;

            }





        }
        public bool CheckComboButtonsPressed()
        {


            /*
             * 
             * if (CurrentSpell.ComboSpell[0].currentActionReference.action.name == InputManagerExt.Instance.InputActions.PlayerControls.LeftB.name)
                {
                    Debug.Log("Same First INput");
                }
             */
            if (InputManagerExt.Instance.NorthButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
            {
                InputComboSequence.Add(InputManagerExt.Instance.InputActions.PlayerControls.NorthB.name);               
                
                return true;
            }
            else if (InputManagerExt.Instance.LeftButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
            {
                InputComboSequence.Add(InputManagerExt.Instance.InputActions.PlayerControls.LeftB.name);
                
                return true;
            }
            else if (InputManagerExt.Instance.RightButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
            {
                InputComboSequence.Add(InputManagerExt.Instance.InputActions.PlayerControls.RightB.name);

                return true;
            }
            else if (InputManagerExt.Instance.SouthButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
            {
                InputComboSequence.Add(InputManagerExt.Instance.InputActions.PlayerControls.SouthB.name);

                return true;
            }

            return false;
        }

        public void CheckComboMatch()
        {



            for(int i=0;i<InputComboSequence.Count;i++)
            {
                for (int j = 0; j < CurrentSpells.Count; j++)
                {
                    if (i < CurrentSpells[j].ComboSpell.Count)
                    {
                        if (CurrentSpells[j].ComboSpell[i].currentActionReference.action.name == InputComboSequence[i])
                        {

                            //Debug.Log(currentIndividualSpell.NameId);
                            CombosCounter[j].Value++;
                            

                        }
                    }
                }
                
               



            }

            for(int i=0;i< CombosCounter.Count;i++)
            {
                
                if (CombosCounter[i].Value == CombosCounter[i].SpellToCast.ComboSpell.Count && InputComboSequence.Count == CombosCounter[i].SpellToCast.ComboSpell.Count)
                {
                    Debug.Log("It is a match!!!!!!!!!!!");
                    Debug.Log("The spell is : " + CombosCounter[i].SpellToCast.NameId);


                    this.gameObject.MMGetComponentNoAlloc<CharacterHandleWeapon>().CurrentWeapon.GetComponent<SpellCasterWeapon>().SpellToCast = GetPooledObject(CombosCounter[i].SpellToCast);//CombosCounter[i].SpellToCast.SpellsPrefabsLevels[SpellLevelIndicator];
                    if (this.gameObject.MMGetComponentNoAlloc<CharacterHandleWeapon>().CurrentWeapon.GetComponent<SpellCasterWeapon>().SpellToCast != null)
                    {
                        this.gameObject.MMGetComponentNoAlloc<CharacterHandleWeapon>().ShootStart();
                    }
                    
                    
                }
                CombosCounter[i].Value = 0;
            }
            
            
            
            InputComboSequence.Clear();


        }
        public GameObject GetPooledObject(SpellSOScript currentSpellToPool)
        {

            for (int i=0; i<AllPools.Count;i++)
            {
                if (AllPools[i].PoolSpellNameId == currentSpellToPool.NameId)
                {
                    for (int j = 0; j < currentSpellToPool.PoolAmount; j++)
                    {
                        if (!AllPools[i].SpellPooledObjects[j].activeInHierarchy)
                        {
                            return AllPools[i].SpellPooledObjects[j];
                        }
                    }
                }
            }
            return null;

            
        }
    }
}
