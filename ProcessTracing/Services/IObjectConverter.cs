using ProcessTracing.Models;
using ProcessTracing.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessTracing.Services
{
    interface IListObjectConverter
    {
        List<ListViewModel> ConvertList(string response);
    }
}
