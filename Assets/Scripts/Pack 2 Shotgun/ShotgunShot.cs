using UnityEngine;

public class ShotgunShot : MonoBehaviour
{
    [SerializeField] ClickChecking Checker;
    [SerializeField] GameObject Cam;
    [SerializeField] Vector3 area;
    [SerializeField] float distance;
    [SerializeField] byte shots;
    [SerializeField] GameObject flash;

    [SerializeField] GameObject Cube;

    byte Shot;
    bool prevShot = false;
    float timr = 0;

    private void Update()
    {

    }
}
