using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Interfaces.Services;

public interface ISharedPointService
{
    public string GetListByName(string url, string token);
}
