using ProcessTracing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessTracing.Services
{
    interface IListObjectConverter
    {
        List<ListModel> ConvertList(string response);
    }
}
