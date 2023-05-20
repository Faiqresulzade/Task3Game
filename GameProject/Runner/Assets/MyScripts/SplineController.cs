using UnityEngine;

public class SplineController : MonoBehaviour
{
    [SerializeField] private float splineSpeed;

    private Vector3 _movePlayer;
    void Start()
    {
        
    }

    void Update()
    {
        _movePlayer = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        transform.localPosition+= _movePlayer*splineSpeed*Time.deltaTime;
        transform.localPosition=new Vector3(Mathf.Clamp(transform.localPosition.x, -4, 4),transform.localPosition.y,transform.position.z);
    }
}
