using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class MaterialSheet : ScriptableObject
{
	public List<DrugMaterial> material; // Replace 'EntityType' to an actual type that is serializable.
}
