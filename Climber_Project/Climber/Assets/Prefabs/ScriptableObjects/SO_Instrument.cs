using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New insrtument", menuName ="Insrument")]
public class SO_Instrument : ScriptableObject
{
    [Header("The numbers :D")]
    public float max_Angle_Range;
    public float min_Angle_Range;
    public int shoot_Count;
    public float aim_Distance;
    public float rangeMovingspeed;

    [Header("The textures ;P")]
    public Sprite instrument_Body;
    public Sprite insturment_Rope;
    public Sprite instrument_Hock;

}
