// CoSpousalRelations.CoSpousalRelationsDefOf
using RimWorld;
using Verse;
using System.Reflection;
using System.Collections.Generic;

namespace CoLovePartnerRelations
{
    [DefOf]
    public static class CoLovePartnerRelationsDefOf {
        public static PawnRelationDef CoLovePartner;
        static CoLovePartnerRelationsDefOf() {
		    DefOfHelper.EnsureInitializedInCtor(typeof(CoLovePartnerRelationsDefOf));
	    }
    }

}

            