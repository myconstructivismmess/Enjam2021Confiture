using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public abstract class Mob : MonoBehaviour
{
    // Stats du mob.
    [SerializeField] protected int hp;
    protected int dmg;
    protected float speed;
    // Stats changeantes.
    protected bool attacking;
    protected float reach;
    
    #region Variables for death
    protected bool _isAlive = true;
    protected UnityEvent _onDeath;
    protected Collider2D _collider;
    protected SpriteRenderer _renderer;
    [SerializeField] protected int _numberOfBlink;
    [SerializeField] protected float _blinkDuration;
    [SerializeField] protected GameObject HealthBar;
    [SerializeField] protected List<Transform> HeartList;

    #endregion

    // Attributs de mouvements.
    public Vector2 direction;
    protected Rigidbody2D mobRb;
    // Timer WIP.
    protected float tmpTimer = 0f;
    private bool _isFirstHit;

    // Initialisation des mouvements aléatoirement.
    void Awake()
    {
        mobRb = GetComponent<Rigidbody2D>();
        direction = Random.Range(0, 2) == 0 ? Vector2.right : Vector2.left;
        ChangeDirection();
    }

    // Méthode appelée pour changer de direction en cas d'obstacle ou de fin de plateforme.
    public void ChangeDirection()
    {
        direction.x *= -1;
        mobRb.AddForce(direction * speed * 2);
        if (direction == Vector2.left) transform.eulerAngles = new Vector3(0, 180, 0);
        else transform.eulerAngles = new Vector3(0, 0, 0);
    }

    // Méthode appelée pour détecter un joueur en face du mob.
    protected bool DetectPlayer()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast((Vector2)transform.position, direction, reach, LayerMask.NameToLayer("MobLayer"));
        if (hitInfo.collider == null) return false;
        else return hitInfo.collider.transform.CompareTag("Player");
    }

    [Button]
    public void Hit(int damage)
    {
        if (_isFirstHit)
        {
            HealthBar.gameObject.SetActive(true);
            _isFirstHit = false;
        }

        hp -= damage;
        if (HeartList.Count > 0)
        {
            HeartList.Remove(HeartList.First());
            HeartList[0].gameObject.SetActive(false);
        }
    } 
    
    /// <summary>
    /// Death animation using DOTWeen
    /// </summary>
    protected void Blink(int blinkNumber, float blinkDuration)
    { 
        if (blinkNumber > 0)
        {
            blinkNumber -= 1;
            _renderer.DOFade(0, blinkDuration / 2).OnComplete(() =>
            {
                if(blinkNumber <=0 && !_isAlive)
                    return;
                
                _renderer.DOFade(1, blinkDuration / 2).OnComplete(() =>
                {
                    Blink(blinkNumber, blinkDuration);
                });
            });
        }
    }

    protected void DeathAnimation()
    {
        GetComponent<Animator>().Play("Death");
    }
}
