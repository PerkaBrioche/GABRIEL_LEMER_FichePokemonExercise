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
    public bool Heal;
    public bool Hypnotised;
    public bool ShieldUsed;
    public bool AttackFailed;
    public int SkipTurn;
    public int PokemonAttack;
    public int AttackAmount;
    public int OldLife;
    public int PsychicUsed;
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
                SpawnText("Le combat est terminé !");

            }
            
            IndexPokemon1 = PokemonInfoController.INT_PokemonChoosed1;
            IndexPokemon2 = PokemonInfoController.INT_PokemonChoosed2;


            if (!FirstStart)
            {
                FirstStart = true;
                
                
                if (PokemonDataBaseManager.PokemonDataBase.datas[IndexPokemon1].stats.speed >
                    PokemonDataBaseManager.PokemonDataBase.datas[IndexPokemon2].stats.speed)
                {
                    SpawnText(PokemonDataBaseManager.PokemonDataBase.datas[IndexPokemon1].Name + " Commence le combat !");
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
            ShieldUsed = true;
            int PsychicUsed = Random.Range(0, PokemonDataBaseManager.PokemonDataBase.datas[PokemonAttack].PsychickAttacksList.Length);
            SpawnText(PokemonDataBaseManager.PokemonDataBase.datas[PokemonAttack].Name + " Utilise son attaque spéciale : " + 
                      PokemonDataBaseManager.PokemonDataBase.datas[PokemonAttack].PsychickAttacksList[PsychicUsed].AttackName);
            if (PokemonDataBaseManager.PokemonDataBase.datas[PokemonAttack].PsychickAttacksList[PsychicUsed]
                    .AttackID == 12)
            {
                Hypnotised = true;
                SkipTurn = Random.Range(1, 3);
                yield return new WaitForSeconds(1.2f);

                SpawnText(PokemonDataBaseManager.PokemonDataBase.datas[PokemonVictim].Name + " Est Hypnotisé !");
                yield return new WaitForSeconds(0.5f);

            }else if (PokemonDataBaseManager.PokemonDataBase.datas[PokemonAttack].PsychickAttacksList[PsychicUsed].AttackID == 19)
            {
                Heal = true;
                int HealInt = Random.Range(20, 50);

                PokemonDataBaseManager.PokemonDataBase.datas[PokemonAttack].stats.pv += HealInt;
                SpawnText(PokemonDataBaseManager.PokemonDataBase.datas[PokemonAttack].Name + " Se regénère de " + HealInt +" Hp");
            }
            
            AttackAmount = PokemonDataBaseManager.PokemonDataBase.datas[PokemonAttack].stats.atkSpe + PokemonDataBaseManager.PokemonDataBase.datas[PokemonAttack].PsychickAttacksList[PsychicUsed].AttackDamage ;
            SpecialUsed = true;
            
            if (Random.Range(0, 2) == 1)
            {
                print("Calcul Failed");
                int randomPrecision = Random.Range(0, 20);
                if (PokemonDataBaseManager.PokemonDataBase.datas[PokemonAttack].PsychickAttacksList[PsychicUsed]
                        .AttackPrecision < randomPrecision)
                {
                    AttackFailed = true;
                }
            }
        }
        else
        {
                       int RandomAttacke = Random.Range(0,
                            PokemonDataBaseManager.PokemonDataBase.datas[PokemonAttack].PhysicalAttacksList.Length);
                        SpawnText(PokemonDataBaseManager.PokemonDataBase.datas[PokemonAttack].Name + " Utilise une attacke physique : " + PokemonDataBaseManager.PokemonDataBase.datas[PokemonAttack].PhysicalAttacksList[RandomAttacke].AttackName);
            AttackAmount = PokemonDataBaseManager.PokemonDataBase.datas[PokemonAttack].stats.atk + PokemonDataBaseManager.PokemonDataBase.datas[PokemonAttack].PhysicalAttacksList[RandomAttacke].AttackDamage;
            
            if (Random.Range(0, 2) == 1)
            {
                print("Calcul Failed");
                int randomPrecision = Random.Range(0, 20);
                if (PokemonDataBaseManager.PokemonDataBase.datas[PokemonAttack].PhysicalAttacksList[RandomAttacke]
                        .AttackPrecision < randomPrecision)
                {
                    AttackFailed = true;
                }
            }
        }

        yield return new WaitForSeconds(2.2f);


        if (Random.Range(0, 3) == 1 && !AttackFailed && !Hypnotised && !Heal)
        {
            ShieldUsed = false;

            if (SpecialUsed)
            {
                if (AttackAmount > PokemonDataBaseManager.PokemonDataBase.datas[PokemonVictim].stats.defSpe)
                {
                    SpawnText(PokemonDataBaseManager.PokemonDataBase.datas[PokemonVictim].Name + " Essaye de se défendre de l'attaque spéciale et ne prend que " + (AttackAmount - PokemonDataBaseManager.PokemonDataBase.datas[PokemonVictim].stats.defSpe) + " dégats");
                    StartCoroutine (Damage( AttackAmount - PokemonDataBaseManager.PokemonDataBase.datas[PokemonVictim].stats.def, PokemonVictim));
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
                    SpecialUsed = false;

                    SpawnText(PokemonDataBaseManager.PokemonDataBase.datas[PokemonVictim].Name + " Essaye de se défendre et ne prend que " + (AttackAmount - PokemonDataBaseManager.PokemonDataBase.datas[PokemonVictim].stats.def) + " dégats");
                    StartCoroutine (Damage(AttackAmount - PokemonDataBaseManager.PokemonDataBase.datas[PokemonVictim].stats.def, PokemonVictim));

                }
                else
                {
                    StartCoroutine (Damage(0, PokemonVictim));
                    SpawnText(PokemonDataBaseManager.PokemonDataBase.datas[PokemonVictim].Name + " Pare l'attaque entièrement");
                }
            }
        }
        else if(!Hypnotised && !Heal)
        {
            if (AttackFailed)
            {
                AttackFailed = false;
                SpawnText("L'attaque de !" +PokemonDataBaseManager.PokemonDataBase.datas[PokemonAttack].Name + " a échoué !!" ); 
            }
            else
            {
                SpawnText(PokemonDataBaseManager.PokemonDataBase.datas[PokemonAttack].Name + " Inflige " + AttackAmount + " dégats");
                StartCoroutine (Damage(AttackAmount , PokemonVictim));
            }

        }else if (Hypnotised)
        {
            Hypnotised = false;
        }
        yield return new WaitForSeconds(2.2f);

        if (SkipTurn > 0)
        {
            SkipTurn--;

            SpawnText(PokemonDataBaseManager.PokemonDataBase.datas[PokemonVictim].Name + "est hypnotisé et jouera pas ! C'est autour de " + PokemonDataBaseManager.PokemonDataBase.datas[PokemonAttack].Name +
                      " d'attaquer !");
            StartCoroutine("Attack");
        }
        else
        {
            Heal = false;
            int NewVictim = PokemonVictim;
            PokemonVictim = PokemonAttack;
            PokemonAttack = NewVictim;
            SpawnText("C'est autour de " + PokemonDataBaseManager.PokemonDataBase.datas[PokemonAttack].Name +
                      " d'attaquer !");
            StartCoroutine("Attack");
        }
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

        if (PokemonDataBaseManager.PokemonDataBase.datas[PokemonVictim].MyWeakness ==
            PokemonDataBaseManager.PokemonDataBase.datas[PokemonAttack].MyType)
        {
            SpawnText("L'attaque est super efficace ! + " + AttackAmount + " dégats !!");
            AttackAmount *= 2;

        }
        PokemonDataBaseManager.PokemonDataBase.datas[IDPokemonVictim].stats.pv -= AttackAmount;

        yield return new WaitForSeconds(0.2f);
        FightAnim.SetBool("Attack", false);
        SpecialUsed = false;

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
