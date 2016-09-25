using System;

namespace TIEntidades
{
    [Serializable]
    public class MonedaInfo
    {
        private String _idMoneda;
        private String _descripcion;

        public MonedaInfo()
        {
        }

        public MonedaInfo(String sIdMoneda, String sDescripcion)
        {
            _idMoneda = sIdMoneda;
            _descripcion = sDescripcion;
        }

        public string IdMoneda
        {
            get { return _idMoneda; }
            set { _idMoneda = value; }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
    }
}
