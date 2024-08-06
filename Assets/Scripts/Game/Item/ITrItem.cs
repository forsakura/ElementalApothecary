using System.Collections.Generic;

public interface ITrItem
{
    ItemID ID { get; set; }

    void AddATTRID(int singleATTR);
    List<int> GetATTRID();
    int GetBaseID();
    void SetBaseID(int id);
}