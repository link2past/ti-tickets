using System;
using System.Collections.Generic;
using TIEntidades;

namespace TIInterfaces
{
    public interface ICategoriaProblema
    {
        IList<CategoriaProblemaInfo> Listar(CategoriaProblemaInfo oCategoriaProblema);
        CategoriaProblemaInfo Consultar(CategoriaProblemaInfo oCategoriaProblema);
        Boolean Registrar(CategoriaProblemaInfo oCategoriaProblema, ref int? nId);
        Boolean Actualizar(CategoriaProblemaInfo oCategoriaProblema);
    }
}
