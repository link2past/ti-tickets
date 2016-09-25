using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TIEntidades;
using TIBDUtil;
using TIInterfaces;

namespace TIAccesoDatos
{
    public class Ubigeo : IUbigeo
    {
        #region Miembros de IUbigeo

        public IList<UbigeoInfo> Listar(UbigeoInfo oUbigeo)
        {
            var sqlParm = new SqlParameter[3];
            IList<UbigeoInfo> oListaUbigeo = new List<UbigeoInfo>();

            sqlParm[0] = new SqlParameter("@Id_Departamento", SqlDbType.VarChar);
            if (oUbigeo.IdDepartamento != null) { sqlParm[0].Value = oUbigeo.IdDepartamento; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Id_Provincia", SqlDbType.VarChar);
            if (oUbigeo.IdProvincia != null) { sqlParm[1].Value = oUbigeo.IdProvincia; } else { sqlParm[1].Value = DBNull.Value; }

            sqlParm[2] = new SqlParameter("@Id_Distrito", SqlDbType.VarChar);
            if (oUbigeo.IdDistrito != null) { sqlParm[2].Value = oUbigeo.IdDistrito; } else { sqlParm[2].Value = DBNull.Value; }

            using (
                var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction,
                                                  CommandType.StoredProcedure, "TI_SP_CONSULTAR_UBIGEO", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        while (drd.Read())
                        {
                            oListaUbigeo.Add(new UbigeoInfo(drd.GetString(0).Trim(), drd.GetString(1).Trim(), drd.GetString(2).Trim(), drd.GetString(3).Trim()));
                        }
                    }
                }
            }
            return oListaUbigeo;
        }

        public UbigeoInfo Consultar(UbigeoInfo oUbigeo)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
