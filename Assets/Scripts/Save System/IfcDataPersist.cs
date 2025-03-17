using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Keep in mind, this script works if you implement it in scripts
that need to be saved, so when using the interface, remember to add it to implement
and then add the two methods here within that script.
LoadData {this.[thing] = data.[thing]}
SaveData {data.[thing] = this.[thing]} 
*/
public interface IfcDataPersist 
{
    void LoadData(GameData data); // loads data

    void SaveData(ref GameData data); // saves to a reference to gamedata to allow the implementing script to modify data.

}
