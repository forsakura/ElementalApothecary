using System.Collections.Generic;
using System.Threading.Tasks;
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
    int MaxMaterialEnum { get; set; }

    void addMaterial(int order, IDataItem Item);
    EElement[] CalculateBaseElement(Vector2 ElementCount);
    Vector2 CalculateElement();
    void checkSucceed();
    int GachaATTR(List<int> pool);
    void init();
    void initATTRpool(EElement[] baseElement, Attribute.AttributeType attributeType);
    IDataItem output();
    Task<IDataItem> OutputAsync();
    void removeMaterial(int order);
}