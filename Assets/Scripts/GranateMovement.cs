using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranateMovement : MonoBehaviour
{
    /*public AnimationCurve curve;

    [SerializeField]
    private float duration = 1.0f, heightY = 3.0f;*/

    [Range(0f, 10f)]
    public float interpolator;

    public AnimationCurve curve;
    private Vector3 centerPivot;
    Vector3 currentPos;
    public float radius;
    public Transform circleOrigin;
    public Vector3 granatePos;
    public Animator anim;
    private Coroutine _shockWaveCoroutine;
    public float _shockWaveTime = 10f;
    public GameObject _material;
    private Material materialWave;
    private static int _moveDistance = Shader.PropertyToID("_WaveDistance");
    public float explosionForceZombie;

    public void Setup(Vector3 granatePoint, Vector3 flyingGranate)
    {
        materialWave = _material.GetComponent<SpriteRenderer>().material;
        GameObject aimAngle = GameObject.FindGameObjectWithTag("Hero");
        transform.eulerAngles = new Vector3(0, 0, aimAngle.transform.eulerAngles.z);
        Vector3 currentPos = transform.position;
        granatePos = granatePoint;

        centerPivot = (currentPos + granatePoint) * 0.5f;
        centerPivot -= new Vector3(0, interpolator);

        Debug.Log(flyingGranate);
    }

    private void Update()
    {
        var startRelativeCenter = transform.position - centerPivot;
        var endRelativeCenter = granatePos - centerPivot;
        transform.position = Vector3.Slerp(startRelativeCenter, endRelativeCenter, curve.Evaluate(interpolator * Time.deltaTime)) + centerPivot;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.grey;
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    private void OnDrawGizmos() {
            foreach (var point in EvaluateSlerpPoints(currentPos, granatePos, centerPivot, 0)) {
                Gizmos.DrawSphere(point, 0.1f);
            }

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(centerPivot, 0.2f);
        }

        IEnumerable<Vector3> EvaluateSlerpPoints(Vector3 start, Vector3 end, Vector3 center, int count = 10) {
            var startRelativeCenter = start - center;
            var endRelativeCenter = end - center;

            var f = 1f / count;

            for (var i = 0f; i < 1 + f; i += f) {
                yield return Vector3.Slerp(startRelativeCenter, endRelativeCenter, i) + center;
            }
        }

    private void OnTriggerEnter2D(Collider2D col)
    {
        GranateTarget target = col.GetComponent<GranateTarget>();
        if (target != null)
        {
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            Destroy(target.gameObject);
            anim.SetTrigger("Explode");
            CallShockWave();
            Invoke("ExplodeZombie", 0.3f);
            Destroy(gameObject, 1.2f);
        }
    }

    public void ExplodeZombie()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position,radius))
        {
            Target health;
            ZombieMovement zombie;
            if(health = collider.GetComponent<Target>())
            {
                if (!health.GetComponent<ZombieMovement>())
                {
                    Rigidbody2D rb = collider.GetComponentInParent<Rigidbody2D>();
                    GetExplode(rb);
                    health.TakeDamage(3);
                }
            }

            if(zombie = collider.GetComponent<ZombieMovement>())
            {
                Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
                GetExplodeZombie(rb);
                zombie.TargetHurtGranate();
            }
        }
    }

    private void GetExplode(Rigidbody2D obj)
    {
        Vector2 direction = obj.position - (Vector2)circleOrigin.position;
        float distance = direction.magnitude;
        float force = 1 - (distance / explosionForceZombie);
        obj.AddForce(direction.normalized * explosionForceZombie * force, ForceMode2D.Impulse);
        StartCoroutine(Stop(obj));
    }

    IEnumerator Stop(Rigidbody2D obj) 
    {
        yield return new WaitForSeconds(0.1f);
        obj.velocity = Vector2.zero;
    }

    private void GetExplodeZombie(Rigidbody2D obj)
    {
        obj.gameObject.GetComponent<ZombieMovement>().enabled = false;
        Vector2 direction = obj.position - (Vector2)circleOrigin.position;
        float distance = direction.magnitude;
        float force = 1 - (distance / explosionForceZombie);
        obj.AddForce(direction.normalized * explosionForceZombie * force, ForceMode2D.Impulse);
        StartCoroutine(StopZombie(obj));
    }

    IEnumerator StopZombie(Rigidbody2D obj) 
    {
        yield return new WaitForSeconds(0.1f);
        obj.gameObject.GetComponent<ZombieMovement>().enabled = true;
        obj.velocity = Vector2.zero;
    }

    public void CallShockWave()
    {
        _shockWaveCoroutine = StartCoroutine(ShockWaveAction(-0.1f, 1f));
    }

    private IEnumerator ShockWaveAction(float startPos, float endPos)
    {
        materialWave.SetFloat(_moveDistance, startPos);

        float lerpedAmount;

        float elapsedTime = 0f;

        while(elapsedTime < _shockWaveTime)
        {
            elapsedTime += Time.deltaTime;

            lerpedAmount = Mathf.Lerp(startPos, endPos, elapsedTime / _shockWaveTime);
            materialWave.SetFloat(_moveDistance, lerpedAmount);

            yield return null;
        }
    }
}
