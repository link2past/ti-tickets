using System.Collections.Generic;
using TIEntidades;

namespace TIInterfaces
{
    public interface IMoneda
    {
        IList<MonedaInfo> Listar(MonedaInfo oMoneda);
        MonedaInfo Consultar(MonedaInfo oMoneda);
    }
}
