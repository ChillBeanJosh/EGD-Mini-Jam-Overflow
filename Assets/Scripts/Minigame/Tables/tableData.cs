using UnityEngine;

public class tableData : MonoBehaviour
{
    public int SeatedClients = 0;
    public float ChanceWeight = 1.00f;

    [HideInInspector]
    public tableSpawner ownerSpawner;
}
