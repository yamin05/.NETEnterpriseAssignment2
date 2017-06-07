using Assignment2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment2.Helpers.Helper_Interfaces
{
    public interface IClientHelper
    {
        IList<ListClientsViewModel> ListOfClients();
    }
}