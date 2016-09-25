using System;
using System.Collections.Generic;
using TIEntidades;

namespace TIInterfaces
{
    public interface IUnidadNegocio
    {
        IList<UnidadNegocioInfo> Listar(UnidadNegocioInfo oUnidadNegocio);
        IList<UnidadNegocioInfo> ListarPorCliente(UnidadNegocioInfo oUnidadNegocio, int? nIdCliente);
        UnidadNegocioInfo Consultar(UnidadNegocioInfo oUnidadNegocio);
        Boolean Registrar(UnidadNegocioInfo oUnidadNegocio, ref int? nId);
        Boolean Actualizar(UnidadNegocioInfo oUnidadNegocio);
    }
}
