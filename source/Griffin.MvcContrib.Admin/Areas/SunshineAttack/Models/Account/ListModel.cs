using System.Collections.Generic;
using SunshineAttack.Localization.Providers.Membership;

namespace SunshineAttack.Localization.Areas.SunshineAttack.Models.Account
{
    public class ListModel
    {
        public IEnumerable<IMembershipAccount> Accounts { get; set; }
        public int TotalCount { get; set; }
    }
}