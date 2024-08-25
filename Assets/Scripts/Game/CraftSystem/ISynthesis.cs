using System.Collections.Generic;
using UnityEngine;

public interface ISynthesis
{
    bool isExploded { get; set; }
    float Explosion { get; set; }
    int MainGachaTimes { get; set; }
    int AuxGachaTimes { get; set; }
    float AuxGachaProbability { get; set; }
    bool AddStabilizers { get; set; }
    bool isSuccess { get; set; }
    bool isExplosive { get; set; }
 

    void addMaterial(int order, IDataItem Item);
    EElement[] CalculateBaseElement(Vector2 ElementCount);
    Vector2 CalculateElement();
    void checkSecceed();
    int GachaATTR(List<int> pool);
    void init();
    void initATTRpool(EElement[] baseElement, Attribute.AttributeType attributeType);
    IDataItem output();
    void removeMaterial(int order);
}