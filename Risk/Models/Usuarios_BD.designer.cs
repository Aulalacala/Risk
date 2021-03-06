﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Risk.Models
{
    using System.Data.Linq;
    using System.Data.Linq.Mapping;
    using System.Data;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Linq;
    using System.Linq.Expressions;
    using System.ComponentModel;
    using System;
    using System.ComponentModel.DataAnnotations;

    [global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Risk")]
	public partial class Usuarios_BDDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InserttUsuario(tUsuario instance);
    partial void UpdatetUsuario(tUsuario instance);
    partial void DeletetUsuario(tUsuario instance);
        #endregion

        public Usuarios_BDDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["RiskMVCConnectionString1"].ConnectionString, mappingSource)
		{
            OnCreated();
        }

        public Usuarios_BDDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public Usuarios_BDDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public Usuarios_BDDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public Usuarios_BDDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<tUsuario> tUsuarios
		{
			get
			{
				return this.GetTable<tUsuario>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.tUsuarios")]
	public partial class tUsuario : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _IdUsuario;
		
		private string _Nombre;
		
		private string _Usuario;
		
		private string _Clave;
		
		private bool _Activo;
		
		private bool _Administrador;
		
		private bool _Bloqueado;
		
		private string _Email;
		
		private string _Tlf1;
		
		private string _Tlf2;
		
		private string _Observaciones;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdUsuarioChanging(int value);
    partial void OnIdUsuarioChanged();
    partial void OnNombreChanging(string value);
    partial void OnNombreChanged();
    partial void OnUsuarioChanging(string value);
    partial void OnUsuarioChanged();
    partial void OnClaveChanging(string value);
    partial void OnClaveChanged();
    partial void OnActivoChanging(bool value);
    partial void OnActivoChanged();
    partial void OnAdministradorChanging(bool value);
    partial void OnAdministradorChanged();
    partial void OnBloqueadoChanging(bool value);
    partial void OnBloqueadoChanged();
    partial void OnEmailChanging(string value);
    partial void OnEmailChanged();
    partial void OnTlf1Changing(string value);
    partial void OnTlf1Changed();
    partial void OnTlf2Changing(string value);
    partial void OnTlf2Changed();
    partial void OnObservacionesChanging(string value);
    partial void OnObservacionesChanged();
    #endregion
		
		public tUsuario()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdUsuario", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int IdUsuario
		{
			get
			{
				return this._IdUsuario;
			}
			set
			{
				if ((this._IdUsuario != value))
				{
					this.OnIdUsuarioChanging(value);
					this.SendPropertyChanging();
					this._IdUsuario = value;
					this.SendPropertyChanged("IdUsuario");
					this.OnIdUsuarioChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Nombre", DbType="NVarChar(150)")]
		public string Nombre
		{
			get
			{
				return this._Nombre;
			}
			set
			{
				if ((this._Nombre != value))
				{
					this.OnNombreChanging(value);
					this.SendPropertyChanging();
					this._Nombre = value;
					this.SendPropertyChanged("Nombre");
					this.OnNombreChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Usuario", DbType="NVarChar(15)")]
        [Required(ErrorMessage ="User required")]
		public string Usuario
		{
			get
			{
				return this._Usuario;
			}
			set
			{
				if ((this._Usuario != value))
				{
					this.OnUsuarioChanging(value);
					this.SendPropertyChanging();
					this._Usuario = value;
					this.SendPropertyChanged("Usuario");
					this.OnUsuarioChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Clave", DbType="NVarChar(100)")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password required")]
        public string Clave
		{
			get
			{
				return this._Clave;
			}
			set
			{
				if ((this._Clave != value))
				{
					this.OnClaveChanging(value);
					this.SendPropertyChanging();
					this._Clave = value;
					this.SendPropertyChanged("Clave");
					this.OnClaveChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Activo", DbType="Bit NOT NULL")]
		public bool Activo
		{
			get
			{
				return this._Activo;
			}
			set
			{
				if ((this._Activo != value))
				{
					this.OnActivoChanging(value);
					this.SendPropertyChanging();
					this._Activo = value;
					this.SendPropertyChanged("Activo");
					this.OnActivoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Administrador", DbType="Bit NOT NULL")]
		public bool Administrador
		{
			get
			{
				return this._Administrador;
			}
			set
			{
				if ((this._Administrador != value))
				{
					this.OnAdministradorChanging(value);
					this.SendPropertyChanging();
					this._Administrador = value;
					this.SendPropertyChanged("Administrador");
					this.OnAdministradorChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Bloqueado", DbType="Bit NOT NULL")]
		public bool Bloqueado
		{
			get
			{
				return this._Bloqueado;
			}
			set
			{
				if ((this._Bloqueado != value))
				{
					this.OnBloqueadoChanging(value);
					this.SendPropertyChanging();
					this._Bloqueado = value;
					this.SendPropertyChanged("Bloqueado");
					this.OnBloqueadoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Email", DbType="NVarChar(255)")]
		public string Email
		{
			get
			{
				return this._Email;
			}
			set
			{
				if ((this._Email != value))
				{
					this.OnEmailChanging(value);
					this.SendPropertyChanging();
					this._Email = value;
					this.SendPropertyChanged("Email");
					this.OnEmailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Tlf1", DbType="NVarChar(255)")]
		public string Tlf1
		{
			get
			{
				return this._Tlf1;
			}
			set
			{
				if ((this._Tlf1 != value))
				{
					this.OnTlf1Changing(value);
					this.SendPropertyChanging();
					this._Tlf1 = value;
					this.SendPropertyChanged("Tlf1");
					this.OnTlf1Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Tlf2", DbType="NVarChar(255)")]
		public string Tlf2
		{
			get
			{
				return this._Tlf2;
			}
			set
			{
				if ((this._Tlf2 != value))
				{
					this.OnTlf2Changing(value);
					this.SendPropertyChanging();
					this._Tlf2 = value;
					this.SendPropertyChanged("Tlf2");
					this.OnTlf2Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Observaciones", DbType="NVarChar(MAX)")]
		public string Observaciones
		{
			get
			{
				return this._Observaciones;
			}
			set
			{
				if ((this._Observaciones != value))
				{
					this.OnObservacionesChanging(value);
					this.SendPropertyChanging();
					this._Observaciones = value;
					this.SendPropertyChanged("Observaciones");
					this.OnObservacionesChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
