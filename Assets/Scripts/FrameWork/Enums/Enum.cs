/// <summary>
/// 默认 行走 浮空 投掷 射击 喝药
/// </summary>
public enum EPlayerBaseState
{
    Default,Walking,Floating
}

public enum EPlayerAttackState
{
    Throwing,Shooting,Drinking
}

public enum ECharacterType
{
    Enemy,Player
}

/// <summary>
/// 无，风，火，土，水
/// </summary>
public enum EElement
{
    None,Aer,Ignis,Terra,Aqua
}
public enum ElementalSickness
{
    Persist,Concentrate,Abundant,Impulsive,Fatal
}

public enum ItemType
{
    Potion, Material, Special
}

public enum ContainerType
{
    Bag,Box,Falsk,Furnace,Pot,Distiller
}
public enum InventoryLocation
{
    Bag,Flask,Box,Furnace,Pot, Distiller
}



