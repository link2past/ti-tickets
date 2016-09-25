using System;
using System.Collections.Generic;
using TIEntidades;

namespace TILogicaNegocio
{
    public class CategoriaProblema
    {
        private static readonly TIInterfaces.ICategoriaProblema DalCategoriaProblema = new TIAccesoDatos.CategoriaProblema();

        public IList<CategoriaProblemaInfo> Listar(CategoriaProblemaInfo oCategoriaProblema)
        {
            return DalCategoriaProblema.Listar(oCategoriaProblema);
        }

        public CategoriaProblemaInfo Consultar(CategoriaProblemaInfo oCategoriaProblema)
        {
            return DalCategoriaProblema.Consultar(oCategoriaProblema);
        }

        public bool Registrar(CategoriaProblemaInfo oCategoriaProblema, ref int? nId)
        {
            return DalCategoriaProblema.Registrar(oCategoriaProblema, ref nId);
        }

        public bool Actualizar(CategoriaProblemaInfo oCategoriaProblema)
        {
            return DalCategoriaProblema.Actualizar(oCategoriaProblema);
        }
    }
}
