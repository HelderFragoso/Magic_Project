using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWand : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Sprite _fireWandFirst;
    [SerializeField] private Sprite _waterWandFirst;
    [SerializeField] private Sprite _rockWand;
    [SerializeField] private GameObject _offSet;
    [SerializeField] private ParticleSystem _flamePrefab;
    [SerializeField] private Auto _fireAutoPrefab;
    [SerializeField] private WaterBubble _waterAutoPrefab;
    [SerializeField] private RockAuto _rockAutoPrefab;
    [SerializeField] private Boulder _boulderPrefab;


    [Header("Bools")]
    [SerializeField] private bool _canShoot = true;
    [SerializeField] private bool _canShootFlame = true;
    [SerializeField] private bool _canShootWater = true;
    [SerializeField] private bool _canShootWaterJet = true;
    [SerializeField] private bool _canShootPebble = true;
    [SerializeField] private bool _canShootBoulder = true;
    [SerializeField] private bool _isFire = true;
    [SerializeField] private bool _isWater = false;
    [SerializeField] private bool _isRock = false;


    private bool _isItDual = true;
    private ParticleSystem _flame;
    private Auto _fireAutoInstance;
    private WaterBubble _waterAutoInstance;
    private RockAuto _rockAutoInstance;
    private Boulder _boulderInstance;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        ChangeSprite();
        ChangeWand();
    }

    private void ChangeSprite()
    {
        if (Input.GetKeyDown(KeyCode.C) && _isFire == true && _isItDual == true)
        {
            GetComponent<SpriteRenderer>().sprite = _waterWandFirst;
            _isWater = true;
            _isFire= false;
        }
        else if(Input.GetKeyDown(KeyCode.C) && _isWater == true && _isItDual == true)
        {
            GetComponent<SpriteRenderer>().sprite = _fireWandFirst;
            _isFire = true;
            _isWater = false;
        }
    }

    private void ChangeWand()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GetComponent<SpriteRenderer>().sprite = _fireWandFirst;
            _isFire = true;
            _isItDual = true;
            _isWater = false;
            _isRock = false;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GetComponent<SpriteRenderer>().sprite = _rockWand;
            _isWater = false;
            _isFire = false;
            _isItDual= false;
            _isRock= true;
        }
    }


    protected virtual void Attack()
    {
        if(_isFire == true)
        {
            if (Input.GetKey(KeyCode.Mouse1) && _canShootFlame == true)
            {
                StartCoroutine(FlameAttack());
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && _canShoot == true)
            {
                StartCoroutine(FireAutoAttack());
            }
        }
        else if(_isWater == true) 
        {
            if (Input.GetKey(KeyCode.Mouse1) && _canShootFlame == true)
            {
                StartCoroutine(FlameAttack());
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && _canShootWater == true)
            {
                StartCoroutine(WaterAutoAttack());
            }

        }
        else if(_isRock == true) 
        { 
            if(Input.GetKey(KeyCode.Mouse0) && _canShootPebble == true)
            {
                StartCoroutine(RockAutoAttack());
            }

            if(Input.GetKeyDown(KeyCode.Mouse1) && _canShootBoulder == true)
            {
                StartCoroutine(BoulderAttack());
            }
        }
       
    }

    #region Coroutine Attacks
    IEnumerator FireAutoAttack() 
    {
        _canShoot = false;
        _fireAutoInstance = Instantiate(_fireAutoPrefab, _offSet.transform.position, _fireAutoPrefab.transform.rotation);
        yield return new WaitForSeconds(0.5f);

        _canShoot = true;

    }

    IEnumerator FlameAttack()
    {
        _canShootFlame = false;
        _flame = Instantiate(_flamePrefab, _offSet.transform.position, _flamePrefab.transform.rotation);
        yield return new WaitForSeconds(0.1f);

        _canShootFlame = true;

    }

    IEnumerator WaterAutoAttack()
    {
        _canShootWater = false;
        _waterAutoInstance = Instantiate(_waterAutoPrefab, _offSet.transform.position, _waterAutoPrefab.transform.rotation);
        yield return new WaitForSeconds(1f);

        _canShootWater = true;
    }

    IEnumerator RockAutoAttack() 
    {
        _canShootPebble = false;
        _rockAutoInstance = Instantiate(_rockAutoPrefab, _offSet.transform.position, _rockAutoPrefab.transform.rotation);
        yield return new WaitForSeconds(0.2f);
        _canShootPebble = true;
    }

    IEnumerator BoulderAttack() 
    {
        _canShootBoulder = false;
        _boulderInstance = Instantiate(_boulderPrefab, _offSet.transform.position, _boulderPrefab.transform.rotation);

        yield return new WaitForSeconds(2);
        _canShootBoulder = true;
    }

    #endregion
}
