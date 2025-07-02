using UnityEngine;

public class RotationCube : MonoBehaviour
{
    private Quaternion targetRotation;

    [SerializeField] private float rotationSpeed = 5f;

    void Start()
    {
        targetRotation = transform.rotation;
    }

    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

public void RotateToPosition1() => SetRotationWithSide(new Vector3(0, 0, 0), 1);
    public void RotateToPosition2() => SetRotationWithSide(new Vector3(0, 90, 0), 2);
    public void RotateToPosition3() => SetRotationWithSide(new Vector3(0, 180, 0), 3);
    public void RotateToPosition4() => SetRotationWithSide(new Vector3(0, 270, 0), 4);
    public void RotateToPosition5() => SetRotationWithSide(new Vector3(90, 0, 0), 5);
    public void RotateToPosition6() => SetRotationWithSide(new Vector3(90, 90, -90), 6);

    private void SetRotationWithSide(Vector3 eulerAngles, int sideNumber)
    {
        My_Text.side = sideNumber;
        targetRotation = Quaternion.Euler(eulerAngles);
    }
}
