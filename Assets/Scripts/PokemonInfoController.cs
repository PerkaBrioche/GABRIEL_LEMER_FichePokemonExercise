using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PokemonInfoController : MonoBehaviour
{
    public GameObject GO_FightCanvas;
    public Button BUTTON_StartFight;
    public Button BUTTON_ChoosePokemon;
    public int INT_PokemonChoosed1;
    public int INT_PokemonChoosed2;
    public bool BOOL_FirstPokemonChoose;
    public List<Slider> SliderStats;
    public Button BUTTON_ChangeRight;
    public Button BUTTON_ChangeLeft;
    public int IndexChanger;
    public Animator CanvasAnimator;
    public int PokemonIndex;
    public DataBaseManager PokemonDataBaseManager;
    [SerializeField] private RawImage _ImgIcon;
    [SerializeField] private RawImage _ImgIcon2;
    [SerializeField] private RawImage _ImgType;
    [SerializeField] private RawImage _IMGSexe;
    [SerializeField] private TextMeshProUGUI _txtName;
    [SerializeField] private TextMeshProUGUI _txtsize;
    [SerializeField] private TextMeshProUGUI _txtweight;
    [SerializeField] private TextMeshProUGUI _txtcaption;
    void Awake()
    {

        PokemonDataBaseManager = FindObjectOfType<DataBaseManager>();

    }

    public void Start()
    {
        AttributStats(PokemonDataBaseManager.PokemonDataBase.datas.Count);
    }


    void Update()
    {
        if (PokemonIndex == INT_PokemonChoosed1 || PokemonIndex == INT_PokemonChoosed2)
        {
            _ImgIcon.color = Color.gray;
        }
        else
        {
            _ImgIcon.color = Color.white;

        }
        PokemonData data = PokemonDataBaseManager.GetData(PokemonIndex);
        _ImgIcon.texture = data.Icon;
        _ImgIcon2.texture = data.Icon;
        _ImgType.texture = data.type;
        _IMGSexe.texture = data.Sexe;
        _txtName.text = data.Name;
        _txtsize.text = data.size.ToString() + "M";
        _txtweight.text = data.weight.ToString() + "KG";
        _txtcaption.text = data.caption;
        SliderStats[0].value = PokemonDataBaseManager.PokemonDataBase.datas[PokemonIndex].stats.pv;
        SliderStats[1].value = PokemonDataBaseManager.PokemonDataBase.datas[PokemonIndex].stats.atk;
        SliderStats[2].value = PokemonDataBaseManager.PokemonDataBase.datas[PokemonIndex].stats.def;
        SliderStats[3].value = PokemonDataBaseManager.PokemonDataBase.datas[PokemonIndex].stats.atkSpe;
        SliderStats[4].value = PokemonDataBaseManager.PokemonDataBase.datas[PokemonIndex].stats.defSpe;
        SliderStats[5].value = PokemonDataBaseManager.PokemonDataBase.datas[PokemonIndex].stats.speed;
        // for (int i = 0; i < SliderStats.Count)
        // {
        //     SliderStats[i].
        // }
        print(PokemonDataBaseManager.PokemonDataBase.datas.Count -1);
        if (PokemonIndex > 0)
        {
            BUTTON_ChangeLeft.interactable = true;
        }
        else
        {
            BUTTON_ChangeLeft.interactable = false;
        }
        if (PokemonIndex < PokemonDataBaseManager.PokemonDataBase.datas.Count -1)
        {
            BUTTON_ChangeRight.interactable = true;
        }
        else
        {
            BUTTON_ChangeRight.interactable = false;
        }
    }

    public void NextPokemon()
    {
        if (PokemonIndex < PokemonDataBaseManager.PokemonDataBase.datas.Count -1)
        {
            IndexChanger = 1;
            StartCoroutine(ChangePokemon());
        }
    }
    public void PreviousPokemon()
    {
        if (PokemonIndex > 0)
        {
            IndexChanger = -1;
            StartCoroutine(ChangePokemon());
        }
    }

    private IEnumerator ChangePokemon()
    {
        CanvasAnimator.Play("ChangePokemon");
        yield return new WaitForSeconds(0.2f);
        PokemonIndex += IndexChanger;
    }
    
    public void AttributStats(int RangeMax)
    {

        
        for (int i = 0; i < RangeMax; i++)
        {
            PokemonData.Stats stats = new();

            stats = stats.InitRandomStat();
            PokemonDataBaseManager.PokemonDataBase.datas[i].stats = stats;
        }
    }

    public void Choose()
    {
        CanvasAnimator.Play("PokemonChoosed");
        if (!BOOL_FirstPokemonChoose)
        {
            INT_PokemonChoosed1 = PokemonIndex;
            BOOL_FirstPokemonChoose = true;
        }
        else
        {
            INT_PokemonChoosed2 = PokemonIndex;
            _ImgIcon.color = Color.gray;
            
            BUTTON_StartFight.interactable = true;
            BUTTON_StartFight.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0,0,0,1);
            
            BUTTON_ChoosePokemon.interactable = false;
            BUTTON_ChoosePokemon.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0,0,0,0.5f);
        }
    }

    public void Fight()
    {
        GO_FightCanvas.SetActive(true);
        gameObject.SetActive(false);
    }
}
