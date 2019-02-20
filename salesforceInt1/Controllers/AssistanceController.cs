using Salesforce.Common.Models;
using salesforceInt1.Models.Salesforce1;
using salesforceInt1.Salesforce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace salesforceInt1.Controllers
{
    public class AssistanceController : Controller
    {
        // GET: Assistance
        // GET: Contacts
        // Note: the SOQL Field list, and Binding Property list have subtle differences as custom properties may be mapped with the JsonProperty attribute to remove __c
        //const string _ContactsPostBinding = "Id,Salutation,FirstName,LastName,MailingStreet,MailingCity,MailingState,MailingPostalCode,MailingCountry,Phone,Email";
        // GET: Contacts
        public async Task<ActionResult> Index()
        {
            IEnumerable<Assistance__c> selectedContacts = Enumerable.Empty<Assistance__c>();
            try
            {
                selectedContacts = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        QueryResult<Assistance__c> contacts =
                            await client.QueryAsync<Assistance__c>("SELECT Owner ID, Assistance Name, Contact, Date, Description, Provider, Status From Assistance__c");
                        return contacts.Records;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "query Salesforce Contacts";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            return View(selectedContacts);
        }

        
    

    }
}
