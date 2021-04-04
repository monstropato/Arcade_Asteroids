using UnityEngine;

[RequireComponent(typeof(Obstacle))]
public class ObstacleRandomness : MonoBehaviour
{
    //CONFIG PARAMS
    [Header("Rotation")]
    [SerializeField] [Tooltip("How much the object will rotate per second")]
    float rotationFactor = 5f;
    [SerializeField] [Tooltip("How much the object can be rotated at the start")] 
    float initialRotation = 15f;

    [Header("Scale")]
    [SerializeField] float minSize = 1;
    [SerializeField] float maxSize = 5f;

    //STATS
    float rndmPitch, rndmYaw, rndmRoll;
    internal float rndmSize;


    //CACHED CLASSES REFERENCES
    Obstacle obstacle;

    //CACHED REFERENCES
    Transform obstacleTransform;


    internal void CustomStart()
    {
        obstacle = GetComponent<Obstacle>();

        SetObstacleModel();
        SetRandomRotationFactor();
        SetInitialRotation();
        SetInitialSize();
    }

    private void Update()
    {
        Rotate();
    }


    //MODEL
    private void SetObstacleModel()
    {
        if (obstacle.obstacleOptions.Length > 0)
        {
            var randomObstacle = obstacle.obstacleOptions[Random.Range(0, obstacle.obstacleOptions.Length)];
            InstantiateModel(randomObstacle);
        }
        else
        {
            InstantiateModel(obstacle.defaultObstacle);
        }
    }

    private void InstantiateModel(GameObject obstacleModel)
    {
        obstacle.obstacleModel = Instantiate(obstacleModel, obstacle.transform); ;
        obstacle.obstacleModel.transform.parent = transform;
    }


    //ROTATION
    //this sets the rotation to be used when summoned
    private void SetInitialRotation()
    {
        float rndmX = Random.Range(-initialRotation, initialRotation);
        float rndmY = Random.Range(-initialRotation, initialRotation);
        float rndmZ = Random.Range(-initialRotation, initialRotation);

        obstacle.obstacleModel.transform.rotation =
            Quaternion.Euler(rndmX, rndmY, rndmZ);
    }

    //this sets the rotation to be used in the rotation update
    private void SetRandomRotationFactor()
    {
        rndmPitch = Random.Range(-rotationFactor, rotationFactor);
        rndmYaw = Random.Range(-rotationFactor, rotationFactor);
        rndmRoll = Random.Range(-rotationFactor, rotationFactor);
    }

    private void Rotate()
    {
        obstacle.obstacleModel.transform.Rotate(
            new Vector3(rndmPitch, rndmYaw, rndmRoll), Space.Self);
    }


    //SCALE
    private void SetInitialSize()
    {
        rndmSize = Random.Range(minSize, maxSize);
        Vector3 rndmV3Size = new Vector3(rndmSize, rndmSize, rndmSize);

        obstacle.obstacleModel.transform.localScale = rndmV3Size;
        obstacle.boxCollider.size = rndmV3Size;
    }
}