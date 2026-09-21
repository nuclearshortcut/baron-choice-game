using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AttributeManager : MonoBehaviour
{
    private static AttributeManager instance;

    // Attribute Text
    [SerializeField] private TextMeshProUGUI _loveText;
    [SerializeField] private TextMeshProUGUI _approText;
    [SerializeField] private TextMeshProUGUI _presText;
    [SerializeField] private TextMeshProUGUI _infText;
    [SerializeField] private TextMeshProUGUI _armyText;
    [SerializeField] private TextMeshProUGUI _churText;
    [SerializeField] private TextMeshProUGUI _goldText;

    // Attribute Nums
    [SerializeField] private int _loveStat;
    [SerializeField] private int _approStat;
    [SerializeField] private int _presStat;
    [SerializeField] private int _infStat;
    [SerializeField] private int _armyStat;
    [SerializeField] private int _churStat;
    [SerializeField] private int _goldStat;


    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance);
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void AlterAttributes(Card card, int dir)
    {
        _loveStat += card.dirSelecs[dir].loveEffect;
        if (_loveStat > 20) {_loveStat = 20;} else if (_loveStat < 0) {_loveStat = 0;};
        _loveText.text = (int.Parse(_loveText.text) + card.dirSelecs[dir].loveEffect).ToString();

        _approStat += card.dirSelecs[dir].approEffect;
        if (_approStat > 20) {_approStat = 20;} else if (_approStat < 0) {_approStat = 0;};
        _approText.text = (int.Parse(_approText.text) + card.dirSelecs[dir].approEffect).ToString();

        _presStat += card.dirSelecs[dir].presEffect;
        if (_presStat > 20) {_presStat = 20;} else if (_presStat < 0) {_presStat = 0;};
        _presText.text = (int.Parse(_presText.text) + card.dirSelecs[dir].presEffect).ToString();

        _infStat += card.dirSelecs[dir].infEffect;
        if (_infStat > 20) {_infStat = 20;} else if (_infStat < 0) {_infStat = 0;};
        _infText.text = (int.Parse(_infText.text) + card.dirSelecs[dir].infEffect).ToString();

        _armyStat += card.dirSelecs[dir].armyEffect;
        if (_armyStat > 20) {_armyStat = 20;} else if (_armyStat < 0) {_armyStat = 0;};
        _armyText.text = (int.Parse(_armyText.text) + card.dirSelecs[dir].armyEffect).ToString();

        _churStat += card.dirSelecs[dir].churEffect;
        if (_churStat > 20) {_churStat = 20;} else if (_churStat < 0) {_churStat = 0;};
        _churText.text = (int.Parse(_churText.text) + card.dirSelecs[dir].churEffect).ToString();

        _goldStat += card.dirSelecs[dir].goldEffect;
        if (_goldStat > 20) {_goldStat = 20;} else if (_goldStat < 0) {_goldStat = 0;};
        _goldText.text = (int.Parse(_goldText.text) + card.dirSelecs[dir].goldEffect).ToString();
    }
}
