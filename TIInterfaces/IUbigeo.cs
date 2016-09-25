using System;
using System.Collections.Generic;
using TIEntidades;

namespace TIInterfaces
{
    public interface IUbigeo
    {
        IList<UbigeoInfo> Listar(UbigeoInfo oUbigeo);
        UbigeoInfo Consultar(UbigeoInfo oUbigeo);
    }
}
