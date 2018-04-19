using System.Web.Mvc;

namespace VaucherSystem.Web.Areas.Merchant
{
    public class MerchantAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Merchant";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.Routes.MapMvcAttributeRoutes();

            context.MapRoute(
                "Merchant_default",
                "Merchant/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}