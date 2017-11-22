﻿using DALC.Portafolio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Negocio.Portafolio
{
    public class FamiliaAnfitriona : IPersistente
    {
        private int _idFamilia;
        private string _nombres;
        private string _apePaterno;
        private string _apeMaterno;
        private string _identificador;
        private string _correo;
        private int _telefono;
        private string _rutaArchivo; 
        private string _direccion;
        private int _idCiudad;
        private int _idPais;
        private string _estado;


        public bool LeerPorIdentificador()
        {
            try
            {
                EntitiesCEM ctx = new DALC.Portafolio.EntitiesCEM();
                //Busca si existe el FamiliaAnfitriona segun su id y asigna los valores a un obj FAMILIASANFITRIONA ENTITY
                FAMILIASANFITRIONA _familia = ctx.FAMILIASANFITRIONA.First(f => f.IDENTIFICACION == Identificador);
                //Asigna los valores de obj del FAMILIASANFITRIONA Entity al obj FamiliaAnfitriona de la Clase
                this.IdFamilia = _familia.ID_FAMILIA;
                this.Nombres = _familia.NOMBRE;
                this.ApePaterno = _familia.APELL_PATERNO;
                this.ApeMaterno = _familia.APELL_MATERNO;
                this.Identificador = _familia.IDENTIFICACION;
                this.Correo = _familia.CORREO;
                this.Telefono = (int)_familia.TELEFONO_CONTACTO;
                this.Direccion = _familia.DIRECCION;
                this.RutaArchivo = _familia.RUTA_ARCHIVO;
                this.IdPais = _familia.ID_PAIS;
                this.IdCiudad = _familia.ID_CIUDAD.GetValueOrDefault();
                this.Estado = _familia.ESTADO;

                ctx = null;

                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool Read()
        {
            try
            {
                EntitiesCEM ctx = new DALC.Portafolio.EntitiesCEM();
                //Busca si existe el FamiliaAnfitriona segun su id y asigna los valores a un obj FAMILIASANFITRIONA ENTITY
                FAMILIASANFITRIONA _familia = ctx.FAMILIASANFITRIONA.First(f => f.ID_FAMILIA == IdFamilia);
                //Asigna los valores de obj del FAMILIASANFITRIONA Entity al obj FamiliaAnfitriona de la Clase
                this.IdFamilia= _familia.ID_FAMILIA;
                this.Nombres = _familia.NOMBRE;
                this.ApePaterno = _familia.APELL_PATERNO;
                this.ApeMaterno = _familia.APELL_MATERNO;
                this.Identificador = _familia.IDENTIFICACION;
                this.Correo = _familia.CORREO;
                this.Telefono = (int)_familia.TELEFONO_CONTACTO;
                this.Direccion = _familia.DIRECCION;
                this.RutaArchivo = _familia.RUTA_ARCHIVO;
                this.IdPais = _familia.ID_PAIS;
                this.IdCiudad = _familia.ID_CIUDAD.GetValueOrDefault();
                this.Estado = _familia.ESTADO;

                ctx = null;

                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public bool Delete()
        {
            try
            {
                EntitiesCEM ctx = new EntitiesCEM();
                //Busca si existe el FAMILIASANFITRIONA segun su id 
                if (ctx.FAMILIASANFITRIONA.Any(f => f.ID_FAMILIA == IdFamilia))
                {
                    //Llama al procedimiento DELETE en la tabla FAMILIASANFITRIONA
                    ctx.DEL_FAMILIASANFITRIONA(IdFamilia);
                    ctx.SaveChanges();
                    ctx = null;

                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Update()
        {
            try
            {
                EntitiesCEM ctx = new EntitiesCEM();
                //Busca si existe el FAMILIASANFITRIONA segun su id
                if (ctx.FAMILIASANFITRIONA.Any(f => f.ID_FAMILIA == IdFamilia))
                {
                    //Llama al procedimiento UPDATE en la tabla FAMILIASANFITRIONA
                    ctx.UPD_FAMILIASANFITRIONA(ApePaterno, ApeMaterno, Direccion, Estado, Correo, IdFamilia, Identificador, Telefono, IdCiudad, Nombres, RutaArchivo, IdPais);
                    ctx.SaveChanges();
                    ctx = null;

                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Create()
        {
            try
            {
                EntitiesCEM ctx = new EntitiesCEM();
                //Llama al procedimiento INSERT en la tabla FAMILIAANFITRIONA
                ctx.INS_FAMILIASANFITRIONA(ApePaterno, ApeMaterno, Direccion, Estado, Correo, IdFamilia, Identificador, Telefono, IdCiudad, Nombres, RutaArchivo, IdPais);
                ctx.SaveChanges();
                ctx = null;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public string Serializar()
        {
            XmlSerializer serializador = new XmlSerializer(typeof(FamiliaAnfitriona));
            StringWriter writer = new StringWriter();

            serializador.Serialize(writer, this);
            writer.Close();

            return writer.ToString();
        }
        public FamiliaAnfitriona(string xml)
        {
            XmlSerializer serializiador = new XmlSerializer(typeof(FamiliaAnfitriona));
            StringReader reader = new StringReader(xml);

            FamiliaAnfitriona familiaAnfitriona = (FamiliaAnfitriona)serializiador.Deserialize(reader);

            this.IdFamilia = familiaAnfitriona.IdFamilia;
            this.Nombres = familiaAnfitriona.Nombres;
            this.ApePaterno = familiaAnfitriona.ApePaterno;
            this.ApeMaterno = familiaAnfitriona.ApeMaterno;
            this.Identificador = familiaAnfitriona.Identificador;
            this.Correo = familiaAnfitriona.Correo;
            this.Telefono = familiaAnfitriona.Telefono;
            this.RutaArchivo = familiaAnfitriona.RutaArchivo;
            this.Direccion = familiaAnfitriona.Direccion;
            this.IdCiudad = familiaAnfitriona.IdCiudad;
            this.IdPais = familiaAnfitriona.IdPais;
            this.Estado = familiaAnfitriona.Estado;
        }

        public FamiliaAnfitriona()
        {
            this.Init();
        }

        private void Init()
        {
            this.IdFamilia = 0;
            this.Nombres = string.Empty;
            this.ApePaterno = string.Empty;
            this.ApeMaterno = string.Empty;
            this.Identificador = string.Empty;
            this.Correo = string.Empty;
            this.Telefono = 0;
            this.RutaArchivo = string.Empty;
            this.Direccion = string.Empty;
            this.IdCiudad = 0;
            this.IdPais = 0;
            this.Estado = string.Empty;
        }

        public int IdFamilia
        {
            get { return _idFamilia; }
            set { _idFamilia = value; }
        }
        public string Nombres
        {
            get { return _nombres; }
            set { _nombres = value; }
        }
        public string ApePaterno
        {
            get { return _apePaterno; }
            set { _apePaterno = value; }
        }
        public string ApeMaterno
        {
            get { return _apeMaterno; }
            set { _apeMaterno = value; }
        }
        public string Identificador
        {
            get { return _identificador; }
            set { _identificador = value; }
        } 
        public string Correo
        {
            get { return _correo; }
            set { _correo = value; }
        } 
        public int Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        } 
        
        public string RutaArchivo
        {
            get { return _rutaArchivo; }
            set { _rutaArchivo = value; }
        } 
        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }
        public int IdCiudad
        {
            get { return _idCiudad; }
            set { _idCiudad = value; }
        }
        public int IdPais
        {
            get { return _idPais; }
            set { _idPais = value; }
        }

        public string Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }
    }
}
