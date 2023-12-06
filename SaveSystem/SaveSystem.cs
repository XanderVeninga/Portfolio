using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Data;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using System;

public class SaveSystem : MonoBehaviour
{
    public enum SystemState
    {
        Waiting,
        Saving,
        Loading
    }
    public SystemState Datastate;
    public static SaveSystem instance;
    public GameManager gameManager;
    public DataSlots dataSlots;
    private string path;
    public int slotToLoad;

    // Start is called before the first frame update

    public void Start()
    {
        path = Application.dataPath + "/DataXml.data";

        //if (instance)
        //{
        //    Destroy(instance.gameObject);
        //}
        //instance = this;

        if (File.Exists(path))
        {
            dataSlots = Load();
        }

        //DontDestroyOnLoad(gameObject);

    }
    public void Save(int _saveIndex)
    {
        dataSlots.savedData[_saveIndex] = SetDataToSave(_saveIndex);
        var serializer = new XmlSerializer(typeof(DataSlots));
        var stream = new FileStream(path, FileMode.Create);
        serializer.Serialize(stream, dataSlots);
        stream.Close();
    }

    public SavedData SetDataToSave(int _saveInSlot)
    {
        Datastate = SystemState.Saving;

        dataSlots.savedData[_saveInSlot] = new SavedData();
        var dataSlot = dataSlots.savedData[_saveInSlot];

        //saving what is in each inventory slot
        for (int sIndex = 0; sIndex < gameManager.inventoryManager.slots.Count; sIndex++)
        {
            if (gameManager.inventoryManager.slots[sIndex].GetComponent<Slot>().amount <= 0)
            {
                dataSlot.slotItemType.Add(-1);
                dataSlot.slotItemCount.Add(0);
            }
            else
            {
                dataSlot.slotItemType.Add(gameManager.inventoryManager.slots[sIndex].GetComponent<Slot>().item.itemID);
                dataSlot.slotItemCount.Add(gameManager.inventoryManager.slots[sIndex].GetComponent<Slot>().amount);
            }
        }

        //saving player transform and base transform
        dataSlot.playerPosition = gameManager.playerController.transform.localPosition;
        dataSlot.playerRotation = gameManager.playerController.gameObject.transform.eulerAngles;
        dataSlot.basePostion = gameManager.baseController.gameObject.transform.position;
        dataSlot.baseRotation = gameManager.baseController.transform.rotation.eulerAngles;

        //getting the buildingID of each placed building
        Transform baseParent = gameManager.buildManager.baseParent;
        for (int bIndex = 0; bIndex < baseParent.childCount; bIndex++)
        {

            //if the building is a farm save what crop is inside the farm and how much the crop has grown
            if (baseParent.GetChild(bIndex).TryGetComponent(out FarmController farmController))
            {
                //saving the crop data
                dataSlot.cropIndex.Add(farmController.currentCrop.GetComponent<Crop>().cropData.CropID);
                dataSlot.cropProgess.Add(farmController.cropProgress);
                dataSlot.cropStage.Add(farmController.growthStage);
            }
            //saving the ID of each buidling and their transforms
            dataSlot.buildingIndexes.Add(baseParent.GetChild(bIndex).GetComponent<CheckPlacement>().buildingID);
            dataSlot.buildingPosistions.Add(baseParent.GetChild(bIndex).transform.localPosition);
            dataSlot.buildingRotations.Add(baseParent.GetChild(bIndex).transform.localEulerAngles);
        }
        Datastate = SystemState.Waiting;
        return dataSlots.savedData[_saveInSlot];
    }

    public void LoadButton(int _sceneToLoad)
    {
        Datastate = SystemState.Loading;
        SceneManager.LoadScene("Main Scene");

        slotToLoad = _sceneToLoad;
    }

    //load all save files
    public DataSlots Load()
    {
        var serializer = new XmlSerializer(typeof(DataSlots));
        var stream = new FileStream(path, FileMode.Open);
        DataSlots container = serializer.Deserialize(stream) as DataSlots;
        stream.Close();
        return container;
    }

    public IEnumerator LoadData()
    {
        yield return new WaitForSeconds(1);
        //loading every inventory slot and what was inside of them
        Debug.Log("Refilling Inventory");
        var _dataSlot = dataSlots.savedData[slotToLoad];
        for (int index = 0; index < gameManager.inventoryManager.slots.Count; index++)
        {
            Debug.Log(index);
            if (_dataSlot.slotItemType[index] == -1)
            {
                continue;
            }
            else if (_dataSlot.slotItemType[index] >= 0)
            {
                Debug.Log("adding: " + gameManager.inventoryManager.itemHolders[_dataSlot.slotItemType[index]]);
                gameManager.inventoryManager.AddItem(gameManager.inventoryManager.itemHolders[_dataSlot.slotItemType[index]], _dataSlot.slotItemCount[index], index);
            }
        }
        //loading the player transform
        Debug.Log("Adjusting Player Transform");
        gameManager.playerController.transform.localPosition = new Vector3(_dataSlot.playerPosition.x, _dataSlot.playerPosition.y, _dataSlot.playerPosition.z);
        gameManager.playerController.gameObject.transform.eulerAngles = new Vector3(_dataSlot.playerRotation.x, _dataSlot.playerRotation.y, _dataSlot.playerRotation.z);

        //loading the base transofrm
        Debug.Log("Adjusting Base Transform");
        gameManager.baseController.gameObject.transform.position = new Vector3(_dataSlot.basePostion.x, _dataSlot.basePostion.y, _dataSlot.basePostion.z);
        gameManager.baseController.transform.eulerAngles = new Vector3(_dataSlot.baseRotation.x, _dataSlot.baseRotation.y, _dataSlot.baseRotation.z);

        //loading all buildings that were placed with correct position and rotation
        Transform baseParent = gameManager.buildManager.baseParent;
        Transform buildParent = gameManager.buildManager.buildParent;

        var _fIndex = 0;
        for (int bIndex = 0; bIndex < _dataSlot.buildingIndexes.Count; bIndex++)
        {
            Debug.Log("Rebuilding Base");
            GameObject newBuilding = Instantiate(buildParent.GetComponent<BuildManager>().objects[_dataSlot.buildingIndexes[bIndex]], baseParent);
            newBuilding.transform.localPosition = _dataSlot.buildingPosistions[bIndex];
            newBuilding.transform.localEulerAngles = new Vector3(_dataSlot.buildingRotations[bIndex].x, _dataSlot.buildingRotations[bIndex].y, _dataSlot.buildingRotations[bIndex].z);

            Debug.Log("Rebuilding Build Blueprint");
            GameObject bpBuilding = Instantiate(buildParent.GetComponent<BuildManager>().objects[_dataSlot.buildingIndexes[bIndex]], buildParent);
            bpBuilding.transform.localPosition = _dataSlot.buildingPosistions[bIndex];
            bpBuilding.transform.localEulerAngles = new Vector3(_dataSlot.buildingRotations[bIndex].x, _dataSlot.buildingRotations[bIndex].y, _dataSlot.buildingRotations[bIndex].z);

            //if the building was a farm load all crop data it contained
            if (newBuilding.TryGetComponent(out FarmController farm))
            {
                farm.currentCrop = Instantiate(farm.crops[_dataSlot.cropIndex[_fIndex]], farm.transform);
                farm.currentCrop.transform.localPosition = new Vector3(0, 0.24f, 0);
                farm.currentCrop = farm.crops[_dataSlot.cropIndex[_fIndex]];
                farm.cropProgress = _dataSlot.cropProgess[_fIndex];
                if (farm.cropProgress == 100)
                {
                    farm.fullyGrown = true;
                }
                farm.growthStage = _dataSlot.cropStage[_fIndex];
                if (!farm.fullyGrown)
                {
                    farm.StartCoroutine(farm.GrowCrop(farm.crops[_dataSlot.cropIndex[_fIndex]].GetComponent<Crop>()));
                }
                _fIndex++;
            }
        }
        Datastate = SystemState.Waiting;
        yield return null;
    }
}