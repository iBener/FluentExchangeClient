using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient;

public interface IDerivativeExchange : IExchange, IDerivativeExchangeRaw
{

}
