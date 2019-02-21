using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NL_VAS.Entity;

namespace NL_VAS.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<LandSetting> LandSetting { get; set; }

        public System.Data.Entity.DbSet<NL_VAS.Entity.LandSettingType> LandSettingTypes { get; set; }

        public System.Data.Entity.DbSet<NL_VAS.Entity.LandSettingTypeValue> LandSettingTypeValues { get; set; }

        public System.Data.Entity.DbSet<NL_VAS.Entity.GeneralInfoParcel> GeneralInfoParcels { get; set; }
        public System.Data.Entity.DbSet<NL_VAS.Entity.LandValuationScore> LandValuationScore { get; set; }

        public System.Data.Entity.DbSet<NL_VAS.Entity.LandType> LandTypes { get; set; }

        public System.Data.Entity.DbSet<NL_VAS.Entity.LandTypeSetting> LandTypeSettings { get; set; }
        public System.Data.Entity.DbSet<NL_VAS.Entity.LandUse> LandUse { get; set; }
        public System.Data.Entity.DbSet<NL_VAS.Entity.LandUseValue> LandUseValue { get; set; }

        public System.Data.Entity.DbSet<NL_VAS.Entity.Factor> Factors { get; set; }

        public System.Data.Entity.DbSet<NL_VAS.Entity.FactorValue> FactorValues { get; set; }

        public System.Data.Entity.DbSet<NL_VAS.Entity.Consideration> Considerations { get; set; }
        public System.Data.Entity.DbSet<NL_VAS.Entity.PriceSetting> PriceSetting { get; set; }
        

    }
}