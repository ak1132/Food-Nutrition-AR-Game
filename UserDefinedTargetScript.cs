using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class UserDefinedTargetScript : MonoBehaviour,IUserDefinedTargetEventHandler {

     UserDefinedTargetBuildingBehaviour userTargetBuildingBehaviour;
     ObjectTracker objectTracker;
     DataSet dataSet;

     ImageTargetBuilder.FrameQuality globalFrameQuality;
     int track = 0;

    public ImageTargetBehaviour imageTargetBehaviour;

    void Start()
    {
        userTargetBuildingBehaviour = GetComponent<UserDefinedTargetBuildingBehaviour>();
        if (userTargetBuildingBehaviour != null)
        {
            userTargetBuildingBehaviour.RegisterEventHandler(this);
        }
    }

    public void OnFrameQualityChanged(ImageTargetBuilder.FrameQuality frameQuality)
    {
        globalFrameQuality = frameQuality;
    }

    public void OnInitialized()
    {
        objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        if (objectTracker != null)
        {
            dataSet = objectTracker.CreateDataSet();
            objectTracker.ActivateDataSet(dataSet);
        }
    }

    public void OnNewTrackableSource(TrackableSource trackableSource)
    {
        track++;
        objectTracker.DeactivateDataSet(dataSet);

        dataSet.CreateTrackable(trackableSource, imageTargetBehaviour.gameObject);

        objectTracker.ActivateDataSet(dataSet);

        userTargetBuildingBehaviour.StartScanning();
    }

    public void BuildTarget()
    {
        if(globalFrameQuality == ImageTargetBuilder.FrameQuality.FRAME_QUALITY_HIGH)
        {
            userTargetBuildingBehaviour.BuildNewTarget(track.ToString(),imageTargetBehaviour.GetSize().x);
            GetComponent<NNConnector>().SendDatatoModel();
        }
    }
    
}
