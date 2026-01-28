using System;
using UnityEngine;

public static class EventManager
{
    public static event Action<Vector3> OnGridParametersChanged;

    public static void EmitGridParameters(Vector3 upperRightCellPosition)
    {
        OnGridParametersChanged?.Invoke(upperRightCellPosition);
    }

}
