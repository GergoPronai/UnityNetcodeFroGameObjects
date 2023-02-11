using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum blessingType
{
    position1_Blesssing,
    position2_Blesssing,
    position3_Blesssing,
    position4_Blesssing
}
public class setPositionScript : MonoBehaviour
{
    public blessingType blessing;
    public PlayergameObjScript player_interact;
    public void enable()
    {
        switch (blessing)
        {
            case blessingType.position1_Blesssing:
                //Melee boost
                break;
            case blessingType.position2_Blesssing:
                //Range and melee
                break;
            case blessingType.position3_Blesssing:
                //Magic And range
                break;
            case blessingType.position4_Blesssing:
                //Magic and Healing
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
