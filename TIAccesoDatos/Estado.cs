using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TIEntidades;
using TIBDUtil;
using TIInterfaces;

namespace TIAccesoDatos
{
    public class Estado : IEstado
    {
        #region Miembros de IEstado

        public IList<EstadoInfo> Listar(EstadoInfo oEstado)
        {
            var sqlParm = new SqlParameter[2];
            var oListaEstados = new List<EstadoInfo>();

            sqlParm[0] = new SqlParameter("@Id_Estado", SqlDbType.Int);
            if (oEstado.IdEstado.HasValue) { sqlParm[0].Value = oEstado.IdEstado; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Descripcion", SqlDbType.VarChar);
            if (oEstado.Descripcion != null) { sqlParm[1].Value = oEstado.Descripcion; } else { sqlParm[1].Value = DBNull.Value; }

            using (
                var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction,
                                                  CommandType.StoredProcedure, "TI_SP_CONSULTAR_ESTADO", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        while (drd.Read())
                        {
                            oListaEstados.Add(new EstadoInfo(Int32.Parse(drd.GetValue(0).ToString()), drd.GetString(1).Trim()));
                        }
                    }
                }
            }

            return oListaEstados;
        }

        public EstadoInfo Consultar(EstadoInfo oEstado)
        {
            var sqlParm = new SqlParameter[2];
            var oEntEstado = new EstadoInfo();

            sqlParm[0] = new SqlParameter("@Id_Estado", SqlDbType.Int);
            if (oEstado.IdEstado.HasValue) { sqlParm[0].Value = oEstado.IdEstado; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Descripcion", SqlDbType.VarChar);
            if (oEstado.Descripcion != null) { sqlParm[1].Value = oEstado.Descripcion; } else { sqlParm[1].Value = DBNull.Value; }

            using (
                var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction,
                                                  CommandType.StoredProcedure, "TI_SP_CONSULTAR_ESTADO", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        drd.Read();
                        oEntEstado = new EstadoInfo(Int32.Parse(drd.GetValue(0).ToString()), drd.GetString(1).Trim());

                    }
                }
            }

            return oEntEstado;
        }

        public bool Registrar(EstadoInfo oEstado, ref int? nId)
        {
            throw new NotImplementedException();
        }

        public bool Actualizar(EstadoInfo oEstado)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
