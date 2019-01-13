﻿using System;
using System.Collections.Generic;
using Harmony;

namespace RanchingSensors
{
	public class RanchingSensorsPatches
	{
		[HarmonyPatch(typeof(GeneratedBuildings))]
		[HarmonyPatch("LoadGeneratedBuildings")]
		public class GeneratedBuildings_LoadGeneratedBuildings_Patch
		{
			public static void Prefix()
			{
				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{CrittersSensorConfig.Id.ToUpperInvariant()}.NAME", CrittersSensorConfig.DisplayName);
				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{CrittersSensorConfig.Id.ToUpperInvariant()}.DESC", CrittersSensorConfig.Description);
				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{CrittersSensorConfig.Id.ToUpperInvariant()}.EFFECT", CrittersSensorConfig.Effect);

				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{EggsSensorConfig.Id.ToUpperInvariant()}.NAME", EggsSensorConfig.DisplayName);
				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{EggsSensorConfig.Id.ToUpperInvariant()}.DESC", EggsSensorConfig.Description);
				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{EggsSensorConfig.Id.ToUpperInvariant()}.EFFECT", EggsSensorConfig.Effect);

				ModUtil.AddBuildingToPlanScreen("Automation", CrittersSensorConfig.Id);
				ModUtil.AddBuildingToPlanScreen("Automation", EggsSensorConfig.Id);
			}

			[HarmonyPatch(typeof(Db))]
			[HarmonyPatch("Initialize")]
			public static class Db_Initialize_Patch
			{
				public static void Prefix()
				{
					var animalTech = new List<string>(Database.Techs.TECH_GROUPING["AnimalControl"]) { CrittersSensorConfig.Id, EggsSensorConfig.Id };
					Database.Techs.TECH_GROUPING["AnimalControl"] = animalTech.ToArray();
				}
			}

			[HarmonyPatch(typeof(KSerialization.Manager))]
			[HarmonyPatch("GetType")]
			public static class KSerializationManager_GetType_Patch
			{
				[HarmonyPostfix]
				public static void GetType(string type_name, ref Type __result)
				{
					if (type_name == "RanchingSensors.CrittersSensor")
					{
						__result = typeof(CrittersSensor);
					}

					if (type_name == "RanchingSensors.EggsSensor")
					{
						__result = typeof(EggsSensor);
					}
				}
			}
		}
	}
}