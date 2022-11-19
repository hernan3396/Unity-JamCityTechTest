using UnityEngine;

public class TileHighlight : MonoBehaviour
{
    public void TileSelected(Transform model)
    {
        model.position += new Vector3(0, 0.1f, 0);
        model.localScale += new Vector3(0.1f, 0.1f, 0.1f);
    }

    public void TileUnselected(Transform model)
    {
        model.position -= new Vector3(0, 0.1f, 0);
        model.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
    }
}
