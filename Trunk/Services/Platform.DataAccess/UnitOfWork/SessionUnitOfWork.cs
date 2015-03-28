using System;
using System.Collections.Generic;
using System.Linq;
using PayPal.Api;
using PayPal.Sample;
using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess.Repositories;

namespace SportsWebPt.Platform.DataAccess
{
    public class SessionUnitOfWork : BaseUnitOfWork, ISessionUnitOfWork
    {
        #region Properties

        public ISessionRepo SessionRepo { get { return GetRepo<ISessionRepo>(); } }
        public IRepository<Case> CaseRepo { get { return GetStandardRepo<Case>(); } }
        public IRepository<SessionPlanMatrixItem> SessionPlanMatrixRepo { get { return GetStandardRepo<SessionPlanMatrixItem>();} }
        public IRepository<TherapistPlanMatrixItem> TherapistPlanMatrixRepo { get {  return GetStandardRepo<TherapistPlanMatrixItem>();} } 

        #endregion

        #region Construction

        public SessionUnitOfWork(IRepositoryProvider repositoryProvider)
            :base(repositoryProvider, new PlatformDbContext())
        {}

        #endregion

        #region Methods
        
        public Session AddSession(Session session)
        {
            Check.Argument.IsNotNull(session, "Session Cannot Be Null");
            Check.Argument.IsNotNegativeOrZero(session.CaseId, "CaseId");
            Check.Argument.IsNotEmpty(session.ScheduledWithId, "Scheduled With Id");

            session.Created = DateTime.Now;
            session.Executed = null;
            //TODO: hack
            session.DifferentialDiagnosisId = null;

            SessionRepo.Add(session);
            Commit();

            return session;
        }

        public Session GetSession(Int64 id)
        {
            var session =  SessionRepo.GetSessionDetails().SingleOrDefault(p => p.Id == id);

            if (session != null)
            {
                session.SessionPlans.ForEach(f =>
                {
                    f.Plan.TherapistPlanMatrixItems =
                        TherapistPlanMatrixRepo.GetAll().Where(p => p.PlanId == f.PlanId).ToList();
                });
            }

            return session;
        }

        public Session GetSessionWithPlanOwner(Int64 id, Guid therapistId)
        {
            var session = GetSession(id);

            if (session != null)
            {
                session.SessionPlans.ForEach(f =>
                {
                    f.Plan.TherapistPlanMatrixItems =
                        TherapistPlanMatrixRepo.GetAll().Where(p => p.PlanId == f.PlanId && p.TherapistId == therapistId).ToList();
                });
            }

            return session;
        }


        public Session UpdateSession(Session session)
        {
            Check.Argument.IsNotNull(session, "Session Cannot Be Null");
            Check.Argument.IsNotNegativeOrZero(session.Id, "SessionId");

            var sessionInDb = SessionRepo.GetSessionDetails()
                .SingleOrDefault(p => p.Id == session.Id);

            if (sessionInDb == null)
                throw new ArgumentNullException("session id", "session does not exist");

            if (session.DifferentialDiagnosisId == 0)
                session.DifferentialDiagnosisId = null;

            var sessionEntry = _context.Entry(sessionInDb);
            sessionEntry.CurrentValues.SetValues(session);

            Commit();

            return session;
        }

        public void SetSessionPlans(Int64 sessionId, int[] planIds)
        {
            Check.Argument.IsNotNegativeOrZero(sessionId, "SessionId");

            var sessionPlans = SessionPlanMatrixRepo.GetAll().Where(p => p.SessionId == sessionId);

            if (sessionPlans.Any())
            {
                foreach (var sessionPlan in sessionPlans)
                    SessionPlanMatrixRepo.Delete(sessionPlan);

                Commit();
            }

            foreach (int planId in planIds)
                SessionPlanMatrixRepo.Add(new SessionPlanMatrixItem() { SessionId = sessionId, PlanId = planId, Name = String.Empty} );    

            Commit();
        }

        public SessionPayDetail ExecuteTransaction(Int64 sessionId, String payerId, String paymentId)
        {
            var apiContext = Configuration.GetAPIContext();
            var paymentExecution = new PaymentExecution() { payer_id = payerId};
            var payment = new Payment() { id = paymentId };

            var session = SessionRepo.GetAll().FirstOrDefault(f => f.Id == sessionId);

            payment.Execute(apiContext, paymentExecution);

            return new SessionPayDetail() { SessionId = sessionId, CaseId = session.CaseId} ;
        }

        public SessionPayDetail CreateTransaction(Int64 sessionId)
        {
            var sessionPayDetail = new SessionPayDetail();
            var session = SessionRepo.GetAll().FirstOrDefault(f => f.Id == sessionId);

            Check.Argument.IsNotNull(session, "Session does not exist");
            var apiContext = Configuration.GetAPIContext();

            // ###Items
            // Items within a transaction.
            var itemList = new ItemList()
            {
                items = new List<Item>() 
                    {
                        new Item()
                        {
                            name = "Session: " + session.Id ,
                            currency = "USD",
                            price = session.Fee.ToString(),
                            quantity = "1",
                            sku = "sku"
                        }
                    }
            };

            // ###Payer
            // A resource representing a Payer that funds a payment
            // Payment Method
            // as `paypal`
            var payer = new Payer() { payment_method = "paypal" };

            // ###Redirect URLS
            // These URLs will determine how the user is redirected from PayPal once they have either approved or canceled the payment.
            var baseURI = "http://localhost:8022/data/sessions/" + session.Id; 
            var redirUrls = new RedirectUrls()
            {
                cancel_url = baseURI + "/pay/cancel",
                return_url = baseURI + "/pay/execute"
            };

            // ###Details
            // Let's you specify details of a payment amount.
            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = session.Fee.ToString()
            };

            // ###Amount
            // Let's you specify a payment amount.
            var amount = new Amount()
            {
                currency = "USD",
                total = session.Fee.ToString(), // Total must be equal to sum of shipping, tax and subtotal.
                details = details
            };

            // ###Transaction
            // A transaction defines the contract of a
            // payment - what is the payment for and who
            // is fulfilling it. 
            var transactionList = new List<Transaction>();

            // The Payment creation API requires a list of
            // Transaction; add the created `Transaction`
            // to a List
            transactionList.Add(new Transaction()
            {
                description = "Transaction description.",
                invoice_number = new Random().Next(999999).ToString(),
                amount = amount,
                item_list = itemList
            });

            // ###Payment
            // A Payment Resource; create one using
            // the above types and intent as `sale` or `authorize`
            var payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            //// ^ Ignore workflow code segment
            //#region Track Workflow
            //this.flow.AddNewRequest("Create PayPal payment", payment);
            //#endregion

            // Create a payment using a valid APIContext
            var createdPayment = payment.Create(apiContext);

            //// ^ Ignore workflow code segment
            //#region Track Workflow
            //this.flow.RecordResponse(createdPayment);
            //#endregion

            // Using the `links` provided by the `createdPayment` object, we can give the user the option to redirect to PayPal to approve the payment.
            var links = createdPayment.links.GetEnumerator();
            while (links.MoveNext())
            {
                var link = links.Current;
                if (link.rel.ToLower().Trim().Equals("approval_url"))
                {
                    sessionPayDetail.PayToUri = link.href;

                    //this.flow.RecordRedirectUrl("Redirect to PayPal to approve the payment...", link.href);
                }
            }

            return sessionPayDetail;
        }

        #endregion
    }

    public interface ISessionUnitOfWork : IBaseUnitOfWork
    {
        ISessionRepo SessionRepo { get; }

        Session GetSession(Int64 id);
        Session GetSessionWithPlanOwner(Int64 id, Guid therapistId);
        Session UpdateSession(Session session);
        Session AddSession(Session session);
        void SetSessionPlans(Int64 sessionId, int[] planIds);
        SessionPayDetail CreateTransaction(Int64 sessionId);
        SessionPayDetail ExecuteTransaction(Int64 sessionId, String payerId, String paymentId);
    }
}

namespace PayPal.Sample
{
    public static class Configuration
    {
        public readonly static string ClientId;
        public readonly static string ClientSecret;

        // Static constructor for setting the readonly static members.
        static Configuration()
        {
            var config = GetConfig();
            ClientId = config["clientId"];
            ClientSecret = config["clientSecret"];
        }

        // Create the configuration map that contains mode and other optional configuration details.
        public static Dictionary<string, string> GetConfig()
        {
            return ConfigManager.Instance.GetProperties();
        }

        // Create accessToken
        private static string GetAccessToken()
        {
            // ###AccessToken
            // Retrieve the access token from
            // OAuthTokenCredential by passing in
            // ClientID and ClientSecret
            // It is not mandatory to generate Access Token on a per call basis.
            // Typically the access token can be generated once and
            // reused within the expiry window                
            string accessToken = new OAuthTokenCredential(ClientId, ClientSecret, GetConfig()).GetAccessToken();
            return accessToken;
        }

        // Returns APIContext object
        public static APIContext GetAPIContext(string accessToken = "")
        {
            // ### Api Context
            // Pass in a `APIContext` object to authenticate 
            // the call and to send a unique request id 
            // (that ensures idempotency). The SDK generates
            // a request id if you do not pass one explicitly. 
            var apiContext = new APIContext(string.IsNullOrEmpty(accessToken) ? GetAccessToken() : accessToken);
            apiContext.Config = GetConfig();

            // Use this variant if you want to pass in a request id  
            // that is meaningful in your application, ideally 
            // a order id.
            // String requestId = Long.toString(System.nanoTime();
            // APIContext apiContext = new APIContext(GetAccessToken(), requestId ));

            return apiContext;
        }

    }
}
