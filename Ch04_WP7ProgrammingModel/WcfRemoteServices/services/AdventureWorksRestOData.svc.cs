using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using WcfRemoteServices.model;

namespace WcfRemoteServices.services
{
  public class AdventureWorksRestOData : DataService<AdventureWorksEntities>
  {
    public static void InitializeService(DataServiceConfiguration config)
    {
      config.SetEntitySetAccessRule("*", EntitySetRights.All);
      config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
      config.SetEntitySetPageSize("*", 20);
    }
  }
}
