using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PokemonFightMangaer : MonoBehaviour
{

    public TextMeshProUGUI DamageText1;
    public TextMeshProUGUI DamageText2;

    public Animator FightAnim;
    public bool SpecialUsed;
    public bool ShieldUsed;
    public int PokemonAttack;
    public int AttackAmount;
    public int OldLife;
    public int PokemonVictim;
    public bool FightStart;
    public bool FirstStart;
    public DataBaseManager PokemonDataBaseManager;
    public PokemonInfoController PokemonInfoController;
    public int IndexPokemon1;
    public int IndexPokemon2;

    public GameObject FightText;
    public Transform TEXTparent;
    void Start()
    {
        StartCoroutine(StartFight());
    }

    void Update()
    {
        if (FightStart)
        {
            if (PokemonDataBaseManager.PokemonDataBase.datas[IndexPokemon1].stats.pv <= 0 ||
                PokemonDataBaseManager.PokemonDataBase.datas[IndexPokemon2].stats.pv <= 0)
            {
                StopCoroutine("Attack");
                FightStart = false;
                StartCoroutine("EndGame");
            }
            
            IndexPokemon1 = PokemonInfoController.INT_PokemonChoosed1;
            IndexPokemon2 = PokemonInfoController.INT_PokemonChoosed2;


            if (!FirstStart)
            {
                FirstStart = true;
                
                
                if (PokemonDataBaseManager.PokemonDataBase.datas[IndexPokemon1].stats.speed >
                    PokemonDataBaseManager.PokemonDataBase.datas[IndexPokemon2].stats.speed)
                {
                    SpawnText(PokemonDataBaseManager.PokemonDataBase.datas[IndexPokemon1].Name +
                              " Commence le combat !");
                    PokemonAttack = IndexPokemon1;
                    PokemonVictim = IndexPokemon2;
                }
                else
                {
                    SpawnText(PokemonDataBaseManager.PokemonDataBase.datas[IndexPokemon2].Name +
                              " Commence le combat !");
                    PokemonAttack = IndexPokemon2;
                    PokemonVictim = IndexPokemon1;
                }
                StartCoroutine("Attack");
            }
            


        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(1.4f);

        if (Random.Range(0, 3) == 1)
        {
            SpawnText(PokemonDataBaseManager.PokemonDataBase.datas[PokemonAttack].Name + " Utilise son attaque spéciale");
            AttackAmount = PokemonDataBaseManager.PokemonDataBase.datas[PokemonAttack].stats.atkSpe;
            SpecialUsed = true;
        }
        else
        { 
            SpawnText(PokemonDataBaseManager.PokemonDataBase.datas[PokemonAttack].Name +" Utilise une attacke physique ");
            AttackAmount = PokemonDataBaseManager.PokemonDataBase.datas[PokemonAttack].stats.atk;
        }

        yield return new WaitForSeconds(2.2f);
        if (Random.Range(0, 4) == 1)
        {
            ShieldUsed = true;
            if (SpecialUsed)
            {
                if (AttackAmount > PokemonDataBaseManager.PokemonDataBase.datas[PokemonVictim].stats.defSpe)
                {
                    SpawnText(PokemonDataBaseManager.PokemonDataBase.datas[PokemonVictim].Name + " Essaye de se défendre de l'attaque spéciale et ne prend que " + (AttackAmount - PokemonDataBaseManager.PokemonDataBase.datas[PokemonVictim].stats.defSpe) + " dégats");
                    StartCoroutine (Damage(AttackAmount - AttackAmount - PokemonDataBaseManager.PokemonDataBase.datas[PokemonVictim].stats.def, PokemonVictim));
                }
                else
                {
                    SpawnText(PokemonDataBaseManager.PokemonDataBase.datas[PokemonVictim].Name + " Pare l'attaque spéciale entièrement");
                    StartCoroutine (Damage(0, PokemonVictim));
                }
            }
            else
            {
                if (AttackAmount > PokemonDataBaseManager.PokemonDataBase.datas[PokemonVictim].stats.def)
                {
                    SpawnText(PokemonDataBaseManager.PokemonDataBase.datas[PokemonVictim].Name + " Essaye de se défendre et ne prend que " + (AttackAmount - PokemonDataBaseManager.PokemonDataBase.datas[PokemonVictim].stats.def) + " dégats");
                    StartCoroutine (Damage(AttackAmount - AttackAmount - PokemonDataBaseManager.PokemonDataBase.datas[PokemonVictim].stats.def, PokemonVictim));
                }
                else
                {
                    StartCoroutine (Damage(0, PokemonVictim));

                    SpawnText(PokemonDataBaseManager.PokemonDataBase.datas[PokemonVictim].Name + " Pare l'attaque entièrement");
                }
            }
        }
        else
        {
            SpawnText(PokemonDataBaseManager.PokemonDataBase.datas[PokemonAttack].Name + " Inflige " + AttackAmount + " dégats");
            StartCoroutine (Damage(AttackAmount , PokemonVictim));        
        }
        yield return new WaitForSeconds(2.2f);
        int NewVictim = PokemonVictim;
        PokemonVictim = PokemonAttack;
        PokemonAttack = NewVictim;
        SpawnText("C'est autour de " + PokemonDataBaseManager.PokemonDataBase.datas[PokemonAttack].Name + " d'attaquer !");
        StartCoroutine("Attack");
    }

    public void SpawnText(string Text)
    {
        var Clone = Instantiate(FightText, TEXTparent);
        Clone.GetComponent<TextMeshProUGUI>().text = Text;
    }

    IEnumerator Damage(int AttackAmount, int IDPokemonVictim)
    {
        FightAnim.SetBool("Attack", true);
        FightAnim.SetBool("ShieldUsed", ShieldUsed);
        if (PokemonAttack == IndexPokemon1)
        {
            DamageText2.text = "-" + AttackAmount;
            FightAnim.SetInteger("PokemonAttack", 1);
            FightAnim.SetInteger("PokemonVictim", 2);
        }
        else
        {
            DamageText1.text = "-" + AttackAmount;

            FightAnim.SetInteger("PokemonAttack", 2);
            FightAnim.SetInteger("PokemonVictim", 1);
        }

        print("Dégat");
        PokemonDataBaseManager.PokemonDataBase.datas[IDPokemonVictim].stats.pv -= AttackAmount;
        yield return new WaitForSeconds(0.2f);
        SpecialUsed = false;
        FightAnim.SetBool("Attack", false);
        ShieldUsed = false;
        FightAnim.SetBool("ShieldUsed", ShieldUsed);
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(0);
    }    
    IEnumerator StartFight()
    {
        yield return new WaitForSeconds(1.45f);
        FightStart = true;
    }
    
}
