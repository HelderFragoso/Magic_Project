using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWand : MonoBehaviour
{
    [SerializeField] private Sprite _fireWandFirst;
    [SerializeField] private Sprite _waterWandFirst;
    [SerializeField] private GameObject _offSet;
    [SerializeField] private ParticleSystem _flamePrefab;
    [SerializeField] private Auto _fireAutoPrefab;
    [SerializeField] private WaterBubble _waterAutoPrefab;
    //[SerializeField] private float _launchVelocity;

    private ParticleSystem _flame;
    private Auto _fireAutoInstance;
    private WaterBubble _waterAutoInstance;
    [SerializeField] private bool _canShoot = true;
    [SerializeField] private bool _canShootFlame = true;
    [SerializeField] private bool _canShootWater = true;
    [SerializeField] private bool _canShootWaterJet = true;
    [SerializeField] private bool _isFire = true;
    [SerializeField] private bool _isWater = false;
    private bool _isItDual = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        ChangeSprite();
    }

    protected virtual void ChangeSprite()
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
       
    }

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
}
