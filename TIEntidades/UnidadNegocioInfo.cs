﻿using System;

namespace TIEntidades
{
    [Serializable]
    public class UnidadNegocioInfo
    {
        private int? _idUnidadNegocio;
        private String _descripcion;
        private int? _idEstado;
        private EstadoInfo _estado;
        private String _usuarioCreacion;
        private String _usuarioModificacion;

        public UnidadNegocioInfo()
        {
        }

        public UnidadNegocioInfo(int? nIdUnidadNegocio, String sDescripcion, int? nIdEstado, EstadoInfo oEstado,
                                 String sUsuarioCreacion, String sUsuarioModificacion)
        {
            _idUnidadNegocio = nIdUnidadNegocio;
            _descripcion = sDescripcion;
            _idEstado = nIdEstado;
            _estado = oEstado;
            _usuarioCreacion = sUsuarioCreacion;
            _usuarioModificacion = sUsuarioModificacion;
        }

        public int? IdUnidadNegocio
        {
            get { return _idUnidadNegocio; }
            set { _idUnidadNegocio = value; }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public int? IdEstado
        {
            get { return _idEstado; }
            set { _idEstado = value; }
        }

        public EstadoInfo Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        public string UsuarioCreacion
        {
            get { return _usuarioCreacion; }
            set { _usuarioCreacion = value; }
        }

        public string UsuarioModificacion
        {
            get { return _usuarioModificacion; }
            set { _usuarioModificacion = value; }
        }
    }
}
