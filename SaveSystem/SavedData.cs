using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavedData
{
    [Header("Inventory and base resources")]
    public List<int> slotItemCount = new List<int>();
    public List<int> slotItemType = new List<int>();
    public int OilAmount;

    [Header("Player and Base Transforms")]
    public Vector3 playerPosition;
    public Vector3 playerRotation;
    public Vector3 basePostion;
    public Vector3 baseRotation;

    [Header("Building info")]
    public List<int> buildingIndexes = new List<int>();
    public List<Vector3> buildingPosistions = new List<Vector3>();
    public List<Vector3> buildingRotations = new List<Vector3>();

    [Header("Farming info")]
    public List<int> cropIndex = new List<int>();
    public List<float> cropProgess = new List<float>();
    public List<int> cropStage = new List<int>();
}
