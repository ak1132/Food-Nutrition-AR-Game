using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class SpawnScript : MonoBehaviour {

    public GameObject mCubeObjt;
    public int mTotalCubes = 1000;
    public float mTimeToSpawn = 1f;

    private GameObject[] mCubes;

    private bool mPositionSet;

    private void Start()
    {
        StartCoroutine(SpawnLoop());

        mCubes = new GameObject[mTotalCubes];
    }

    private bool SetPosition()
    {

        Transform cameraTransform = Camera.main.transform;

        transform.position = cameraTransform.position + cameraTransform.forward * 10f;
        transform.rotation = cameraTransform.rotation;
        transform.localScale = Vector3.one;
        //Transform cam = Camera.main.transform;

        //transform.position = cam.position * 10;
        return true;
    }

    private IEnumerator SpawnLoop()
    {
        StartCoroutine(ChangePosition());

        yield return new WaitForSeconds(0.2f);

        int i = 0;
        while (i <= mTotalCubes - 1)
        {
            mCubes[i] = SpawnElement();
            i++;
            yield return new WaitForSeconds(Random.Range(mTimeToSpawn, mTimeToSpawn * 3));

        }
    }

    private GameObject SpawnElement()
    {
        GameObject cube = Instantiate(mCubeObjt, (Random.insideUnitSphere * 3) + transform.position, transform.rotation) as GameObject;
        float scale = Random.Range(0.5f, 2f);
        cube.transform.localScale = new Vector3(scale, scale, scale);
        return cube;
    }

    private IEnumerator ChangePosition()
    {
        yield return new WaitForSeconds(0.2f);

        if (!mPositionSet)
        {
            if (VuforiaBehaviour.Instance.enabled)
                SetPosition();
        }
    }
}