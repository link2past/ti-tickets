using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TIEntidades;
using TIBDUtil;
using TIInterfaces;

namespace TIAccesoDatos
{
    public class Moneda : IMoneda
    {
        #region Miembros de IMoneda

        public IList<MonedaInfo> Listar(MonedaInfo oMoneda)
        {
            var sqlParm = new SqlParameter[2];
            var oListaMoneda = new List<MonedaInfo>();

            sqlParm[0] = new SqlParameter("@Id_Moneda", SqlDbType.VarChar);
            if (oMoneda.IdMoneda != null) { sqlParm[0].Value = oMoneda.IdMoneda; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Descripcion", SqlDbType.VarChar);
            if (oMoneda.Descripcion != null) { sqlParm[1].Value = oMoneda.Descripcion; } else { sqlParm[1].Value = DBNull.Value; }

            using (var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction, CommandType.StoredProcedure, "TI_SP_CONSULTAR_MONEDA", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        while (drd.Read())
                        {
                            oListaMoneda.Add(new MonedaInfo(drd.GetString(0).Trim(), drd.GetString(1).Trim()));
                        }
                    }
                }
            }
            return oListaMoneda;
        }

        public MonedaInfo Consultar(MonedaInfo oMoneda)
        {
            var sqlParm = new SqlParameter[2];
            var oEntMoneda = new MonedaInfo();

            sqlParm[0] = new SqlParameter("@Id_Moneda", SqlDbType.VarChar);
            if (oMoneda.IdMoneda != null) { sqlParm[0].Value = oMoneda.IdMoneda; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Descripcion", SqlDbType.VarChar);
            if (oMoneda.Descripcion != null) { sqlParm[1].Value = oMoneda.Descripcion; } else { sqlParm[1].Value = DBNull.Value; }

            using (var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction, CommandType.StoredProcedure, "TI_SP_CONSULTAR_MONEDA", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        drd.Read();
                        oEntMoneda = new MonedaInfo(drd.GetString(0).Trim(), drd.GetString(1).Trim());
                    }
                }
            }
            return oEntMoneda;
        }

        #endregion
    }
}
