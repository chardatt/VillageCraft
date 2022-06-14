using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class PlayerCamera : MonoBehaviour
{
    public CinemachineFreeLook cinemachineFreeLook;

    public float zoomSpeed;
    public float zoomValue;
    private float zoomLevelTop,zoomLevelMid,zoomLevelBot;


    // Start is called before the first frame update
    void Start()
    {
        zoomLevelBot = cinemachineFreeLook.m_Orbits[0].m_Radius;
        zoomLevelMid = cinemachineFreeLook.m_Orbits[1].m_Radius;
        zoomLevelTop = cinemachineFreeLook.m_Orbits[2].m_Radius;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            zoomLevelBot = cinemachineFreeLook.m_Orbits[0].m_Radius - zoomValue;
            zoomLevelMid = cinemachineFreeLook.m_Orbits[1].m_Radius - zoomValue;
            zoomLevelTop = cinemachineFreeLook.m_Orbits[2].m_Radius - zoomValue;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            zoomLevelBot = cinemachineFreeLook.m_Orbits[0].m_Radius + zoomValue;
            zoomLevelMid = cinemachineFreeLook.m_Orbits[1].m_Radius + zoomValue;
            zoomLevelTop = cinemachineFreeLook.m_Orbits[2].m_Radius + zoomValue;
        }

        cinemachineFreeLook.m_Orbits[1].m_Radius = Mathf.Lerp(cinemachineFreeLook.m_Orbits[1].m_Radius, zoomLevelMid, zoomSpeed * Time.deltaTime);
        cinemachineFreeLook.m_Orbits[1].m_Radius = Mathf.Clamp(cinemachineFreeLook.m_Orbits[1].m_Radius, 5, 15);

        cinemachineFreeLook.m_Orbits[2].m_Radius = Mathf.Lerp(cinemachineFreeLook.m_Orbits[2].m_Radius, zoomLevelTop, zoomSpeed * Time.deltaTime);
        cinemachineFreeLook.m_Orbits[2].m_Radius = Mathf.Clamp(cinemachineFreeLook.m_Orbits[2].m_Radius, 2, 12);

        cinemachineFreeLook.m_Orbits[0].m_Radius = Mathf.Lerp(cinemachineFreeLook.m_Orbits[0].m_Radius, zoomLevelBot, zoomSpeed * Time.deltaTime);
        cinemachineFreeLook.m_Orbits[0].m_Radius = Mathf.Clamp(cinemachineFreeLook.m_Orbits[0].m_Radius, 3, 13);

    }    
}
