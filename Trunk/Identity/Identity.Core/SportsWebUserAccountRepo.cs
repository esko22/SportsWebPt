using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrockAllen.MembershipReboot;
using BrockAllen.MembershipReboot.Ef;
using BrockAllen.MembershipReboot.Relational;

namespace SportsWebPt.Identity.Core
{
    public class SportsWebUserAccountRepo : DbContextUserAccountRepository<SportsWebMembershipRebootDatabase, SportsWebUser>, 
             IUserAccountRepository
    {
        public SportsWebUserAccountRepo(SportsWebMembershipRebootDatabase ctx)
            : base(ctx)
        {
        }

        IUserAccountRepository<SportsWebUser> This { get { return (IUserAccountRepository<SportsWebUser>)this; } }

        public new UserAccount Create()
        {
            return This.Create();
        }

        public void Add(UserAccount item)
        {
            This.Add((SportsWebUser)item);
        }

        public void Remove(UserAccount item)
        {
            This.Remove((SportsWebUser)item);
        }

        public void Update(UserAccount item)
        {
            This.Update((SportsWebUser)item);
        }

        public new UserAccount GetByID(System.Guid id)
        {
            return This.GetByID(id);
        }

        public new UserAccount GetByUsername(string username)
        {
            return This.GetByUsername(username);
        }

        UserAccount IUserAccountRepository<UserAccount>.GetByUsername(string tenant, string username)
        {
            return This.GetByUsername(tenant, username);
        }

        public new UserAccount GetByEmail(string tenant, string email)
        {
            return This.GetByEmail(tenant, email);
        }

        public new UserAccount GetByMobilePhone(string tenant, string phone)
        {
            return This.GetByMobilePhone(tenant, phone);
        }

        public new UserAccount GetByVerificationKey(string key)
        {
            return This.GetByVerificationKey(key);
        }

        public new UserAccount GetByLinkedAccount(string tenant, string provider, string id)
        {
            return This.GetByLinkedAccount(tenant, provider, id);
        }

        public new UserAccount GetByCertificate(string tenant, string thumbprint)
        {
            return This.GetByCertificate(tenant, thumbprint);
        }
    }
}
