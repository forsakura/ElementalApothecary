using System.Collections.Generic;

public interface ITrItem
{
    void AddATTRID(int singleATTR);
    List<int> GetATTRID();
    int GetBaseID();
    void SetBaseID(int id);
}