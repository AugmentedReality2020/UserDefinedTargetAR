using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
public class UDTManager : MonoBehaviour,IUserDefinedTargetEventHandler
{
    UserDefinedTargetBuildingBehaviour udt_targetBuildingBehavior;
    ObjectTracker objectTracker;
    DataSet dataSet;
    ImageTargetBuilder.FrameQuality udt_FrameQuality;  
    public ImageTargetBehaviour targetBehaviour;
    int targetCounter;
    void Start()
    {
        udt_targetBuildingBehavior = GetComponent<UserDefinedTargetBuildingBehaviour>();
        if(udt_targetBuildingBehavior)
        {
            udt_targetBuildingBehavior.RegisterEventHandler(this);
        }
    }
    //This method update the frameQuality
    public void OnFrameQualityChanged(ImageTargetBuilder.FrameQuality frameQuality)
    {
        udt_FrameQuality = frameQuality;
        //throw new System.NotImplementedException();
    }

    public void OnInitialized()
    {
        objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        if(objectTracker!=null)
        {
            dataSet = objectTracker.CreateDataSet();
            objectTracker.ActivateDataSet(dataSet);
        }
    }

    public void OnNewTrackableSource(TrackableSource trackableSource)
    {
        targetCounter++;
        objectTracker.DeactivateDataSet(dataSet);
        dataSet.CreateTrackable(trackableSource, targetBehaviour.gameObject);
        objectTracker.ActivateDataSet(dataSet);
        udt_targetBuildingBehavior.StartScanning();

        //  throw new System.NotImplementedException();
    }
    public void BuildTarget()
    {
        if(udt_FrameQuality==ImageTargetBuilder.FrameQuality.FRAME_QUALITY_HIGH)
        {
            udt_targetBuildingBehavior.BuildNewTarget(targetCounter.ToString(), targetBehaviour.GetSize().x);
        }
    }
}
