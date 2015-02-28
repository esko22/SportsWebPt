using System;

using BrockAllen.MembershipReboot;
using BrockAllen.MembershipReboot.Relational;

namespace SportsWebPt.Identity.Core
{
    public class SportsWebMembershipRebootConfig : MembershipRebootConfiguration<SportsWebUser>
    {

        #region Construction
        
        public SportsWebMembershipRebootConfig()
            : this(String.Empty, String.Empty)
        { }

        public SportsWebMembershipRebootConfig(String loginUrl, String identityUrl)
        {
            PasswordHashingIterationCount = 50000;
            AllowLoginAfterAccountCreation = true;
            RequireAccountVerification = false;
            //config.EmailIsUsername = true;
            var appinfo = new ApplicationInformation
            {
                ApplicationName = "SportsWebPT",
                EmailSignature = "SportsWebPT Staff",
                LoginUrl = loginUrl,
                CancelVerificationUrl = String.Format("{0}/core/cancelVerification/", identityUrl),
                ConfirmChangeEmailUrl = String.Format("{0}/core/verifyEmail/", identityUrl),
                ConfirmPasswordResetUrl = String.Format("{0}/core/passwordReset/", identityUrl)
            };

            AddEventHandler(new EmailAccountEventsHandler<SportsWebUser>(new EmailMessageFormatter<SportsWebUser>(appinfo)));
        } 

        #endregion
    }

}
