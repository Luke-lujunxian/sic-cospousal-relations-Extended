// CoSpousalRelations.PawnRelationWorker_CoSpouse
using RimWorld;
using Verse;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;


namespace CoLovePartnerRelations
{
    public class PawnRelationWorker_CoLove : PawnRelationWorker
    {

        public static List<PawnRelationDef> AllLoveRelationships = getRelations();

        private static List<PawnRelationDef> getRelations()
        {
            List<PawnRelationDef> AllLoveRelationships = new List<PawnRelationDef>();
            PropertyInfo[] properties = typeof(PawnRelationDefOf).GetProperties();

            foreach (PropertyInfo prop in properties)
            {
                if (prop.PropertyType != typeof(PawnRelationDef)) continue;
                // Assume other mod that add new love relatinship will modify this?
                if (LovePartnerRelationUtility.IsLovePartnerRelation(prop.GetValue(null, null) as PawnRelationDef))
                {
                    AllLoveRelationships.Add(prop.GetValue(null, null) as PawnRelationDef);
                }
            }
            return AllLoveRelationships;
        }
        public override bool InRelation(Pawn me, Pawn other)
        {
            if (me == other)
            {
                return false;
            }
            if (LovePartnerRelationUtility.HasAnyLovePartner(other))
            {
                return false;
            }
            foreach (PawnRelationDef relationType in AllLoveRelationships)
            {
                PawnRelationWorker worker = relationType.Worker;
                // Not considered Co-Lover if you are direct Lover
                if (worker.InRelation(me, other))
                {
                    return false;
                }
                // Use GetLoveCluster?
                // Considered Co-Spouses if you share a living Spouse
                foreach (DirectPawnRelation loverRelation in other.GetLoveRelations(includeDead: false))
                {
                    if (worker.InRelation(me, loverRelation.otherPawn))
                    {
                        Log.Message(me.Name.ToStringFull + "is fine to sleep with " + other.Name.ToStringFull + " because " + loverRelation.otherPawn.Name.ToStringFull);
                        return true;
                    }
                }
            }


            return false;

        }
    }
}