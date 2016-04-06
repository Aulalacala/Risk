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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Risk")]
	public partial class DataClasses1DataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InserttRiesgo(tRiesgo instance);
    partial void UpdatetRiesgo(tRiesgo instance);
    partial void DeletetRiesgo(tRiesgo instance);
    partial void InserttRiesgos_ControlOportunidad(tRiesgos_ControlOportunidad instance);
    partial void UpdatetRiesgos_ControlOportunidad(tRiesgos_ControlOportunidad instance);
    partial void DeletetRiesgos_ControlOportunidad(tRiesgos_ControlOportunidad instance);
    partial void InserttRiesgos_Clasificaciones1(tRiesgos_Clasificaciones1 instance);
    partial void UpdatetRiesgos_Clasificaciones1(tRiesgos_Clasificaciones1 instance);
    partial void DeletetRiesgos_Clasificaciones1(tRiesgos_Clasificaciones1 instance);
    partial void InserttRiesgos_Clasificaciones2(tRiesgos_Clasificaciones2 instance);
    partial void UpdatetRiesgos_Clasificaciones2(tRiesgos_Clasificaciones2 instance);
    partial void DeletetRiesgos_Clasificaciones2(tRiesgos_Clasificaciones2 instance);
    partial void InserttRiesgos_Clasificaciones3(tRiesgos_Clasificaciones3 instance);
    partial void UpdatetRiesgos_Clasificaciones3(tRiesgos_Clasificaciones3 instance);
    partial void DeletetRiesgos_Clasificaciones3(tRiesgos_Clasificaciones3 instance);
    #endregion
		
		public DataClasses1DataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["RiskConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<tRiesgo> tRiesgos
		{
			get
			{
				return this.GetTable<tRiesgo>();
			}
		}
		
		public System.Data.Linq.Table<tRiesgos_ControlOportunidad> tRiesgos_ControlOportunidads
		{
			get
			{
				return this.GetTable<tRiesgos_ControlOportunidad>();
			}
		}
		
		public System.Data.Linq.Table<tRiesgos_Clasificaciones1> tRiesgos_Clasificaciones1s
		{
			get
			{
				return this.GetTable<tRiesgos_Clasificaciones1>();
			}
		}
		
		public System.Data.Linq.Table<tRiesgos_Clasificaciones2> tRiesgos_Clasificaciones2s
		{
			get
			{
				return this.GetTable<tRiesgos_Clasificaciones2>();
			}
		}
		
		public System.Data.Linq.Table<tRiesgos_Clasificaciones3> tRiesgos_Clasificaciones3s
		{
			get
			{
				return this.GetTable<tRiesgos_Clasificaciones3>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.tRiesgos")]
	public partial class tRiesgo : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _IdRiesgo;
		
		private string _CodRiesgoGenerico;
		
		private string _CodRiesgo;
		
		private string _CodRiesgoLocalizado;
		
		private string _Nombre;
		
		private System.Nullable<int> _IdCategoria;
		
		private int _IdClasificacion1;
		
		private int _IdClasificacion2;
		
		private int _IdClasificacion3;
		
		private string _Descripcion;
		
		private string _Ejemplo;
		
		private string _Justificacion;
		
		private System.Nullable<int> _IdSegmentacion1;
		
		private System.Nullable<int> _IdResponsable;
		
		private System.Nullable<int> _IdSupervisor;
		
		private int _IdControlesOportunidad;
		
		private System.Nullable<int> _IdControlesEfectividad;
		
		private string _Vigencia;
		
		private EntitySet<tRiesgos_ControlOportunidad> _tRiesgos_ControlOportunidads;
		
		private EntitySet<tRiesgos_Clasificaciones1> _tRiesgos_Clasificaciones1s;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdRiesgoChanging(int value);
    partial void OnIdRiesgoChanged();
    partial void OnCodRiesgoGenericoChanging(string value);
    partial void OnCodRiesgoGenericoChanged();
    partial void OnCodRiesgoChanging(string value);
    partial void OnCodRiesgoChanged();
    partial void OnCodRiesgoLocalizadoChanging(string value);
    partial void OnCodRiesgoLocalizadoChanged();
    partial void OnNombreChanging(string value);
    partial void OnNombreChanged();
    partial void OnIdCategoriaChanging(System.Nullable<int> value);
    partial void OnIdCategoriaChanged();
    partial void OnIdClasificacion1Changing(int value);
    partial void OnIdClasificacion1Changed();
    partial void OnIdClasificacion2Changing(int value);
    partial void OnIdClasificacion2Changed();
    partial void OnIdClasificacion3Changing(int value);
    partial void OnIdClasificacion3Changed();
    partial void OnDescripcionChanging(string value);
    partial void OnDescripcionChanged();
    partial void OnEjemploChanging(string value);
    partial void OnEjemploChanged();
    partial void OnJustificacionChanging(string value);
    partial void OnJustificacionChanged();
    partial void OnIdSegmentacion1Changing(System.Nullable<int> value);
    partial void OnIdSegmentacion1Changed();
    partial void OnIdResponsableChanging(System.Nullable<int> value);
    partial void OnIdResponsableChanged();
    partial void OnIdSupervisorChanging(System.Nullable<int> value);
    partial void OnIdSupervisorChanged();
    partial void OnIdControlesOportunidadChanging(int value);
    partial void OnIdControlesOportunidadChanged();
    partial void OnIdControlesEfectividadChanging(System.Nullable<int> value);
    partial void OnIdControlesEfectividadChanged();
    partial void OnVigenciaChanging(string value);
    partial void OnVigenciaChanged();
    #endregion
		
		public tRiesgo()
		{
			this._tRiesgos_ControlOportunidads = new EntitySet<tRiesgos_ControlOportunidad>(new Action<tRiesgos_ControlOportunidad>(this.attach_tRiesgos_ControlOportunidads), new Action<tRiesgos_ControlOportunidad>(this.detach_tRiesgos_ControlOportunidads));
			this._tRiesgos_Clasificaciones1s = new EntitySet<tRiesgos_Clasificaciones1>(new Action<tRiesgos_Clasificaciones1>(this.attach_tRiesgos_Clasificaciones1s), new Action<tRiesgos_Clasificaciones1>(this.detach_tRiesgos_Clasificaciones1s));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdRiesgo", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int IdRiesgo
		{
			get
			{
				return this._IdRiesgo;
			}
			set
			{
				if ((this._IdRiesgo != value))
				{
					this.OnIdRiesgoChanging(value);
					this.SendPropertyChanging();
					this._IdRiesgo = value;
					this.SendPropertyChanged("IdRiesgo");
					this.OnIdRiesgoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CodRiesgoGenerico", DbType="NVarChar(50)")]
		public string CodRiesgoGenerico
		{
			get
			{
				return this._CodRiesgoGenerico;
			}
			set
			{
				if ((this._CodRiesgoGenerico != value))
				{
					this.OnCodRiesgoGenericoChanging(value);
					this.SendPropertyChanging();
					this._CodRiesgoGenerico = value;
					this.SendPropertyChanged("CodRiesgoGenerico");
					this.OnCodRiesgoGenericoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CodRiesgo", DbType="NVarChar(50)")]
		public string CodRiesgo
		{
			get
			{
				return this._CodRiesgo;
			}
			set
			{
				if ((this._CodRiesgo != value))
				{
					this.OnCodRiesgoChanging(value);
					this.SendPropertyChanging();
					this._CodRiesgo = value;
					this.SendPropertyChanged("CodRiesgo");
					this.OnCodRiesgoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CodRiesgoLocalizado", DbType="NVarChar(250)")]
		public string CodRiesgoLocalizado
		{
			get
			{
				return this._CodRiesgoLocalizado;
			}
			set
			{
				if ((this._CodRiesgoLocalizado != value))
				{
					this.OnCodRiesgoLocalizadoChanging(value);
					this.SendPropertyChanging();
					this._CodRiesgoLocalizado = value;
					this.SendPropertyChanged("CodRiesgoLocalizado");
					this.OnCodRiesgoLocalizadoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Nombre", DbType="NVarChar(250)")]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdCategoria", DbType="Int")]
		public System.Nullable<int> IdCategoria
		{
			get
			{
				return this._IdCategoria;
			}
			set
			{
				if ((this._IdCategoria != value))
				{
					this.OnIdCategoriaChanging(value);
					this.SendPropertyChanging();
					this._IdCategoria = value;
					this.SendPropertyChanged("IdCategoria");
					this.OnIdCategoriaChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdClasificacion1", DbType="Int")]
		public int IdClasificacion1
		{
			get
			{
				return this._IdClasificacion1;
			}
			set
			{
				if ((this._IdClasificacion1 != value))
				{
					this.OnIdClasificacion1Changing(value);
					this.SendPropertyChanging();
					this._IdClasificacion1 = value;
					this.SendPropertyChanged("IdClasificacion1");
					this.OnIdClasificacion1Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdClasificacion2", DbType="Int")]
		public int IdClasificacion2
		{
			get
			{
				return this._IdClasificacion2;
			}
			set
			{
				if ((this._IdClasificacion2 != value))
				{
					this.OnIdClasificacion2Changing(value);
					this.SendPropertyChanging();
					this._IdClasificacion2 = value;
					this.SendPropertyChanged("IdClasificacion2");
					this.OnIdClasificacion2Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdClasificacion3", DbType="Int")]
		public int IdClasificacion3
		{
			get
			{
				return this._IdClasificacion3;
			}
			set
			{
				if ((this._IdClasificacion3 != value))
				{
					this.OnIdClasificacion3Changing(value);
					this.SendPropertyChanging();
					this._IdClasificacion3 = value;
					this.SendPropertyChanged("IdClasificacion3");
					this.OnIdClasificacion3Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Descripcion", DbType="NVarChar(250)")]
		public string Descripcion
		{
			get
			{
				return this._Descripcion;
			}
			set
			{
				if ((this._Descripcion != value))
				{
					this.OnDescripcionChanging(value);
					this.SendPropertyChanging();
					this._Descripcion = value;
					this.SendPropertyChanged("Descripcion");
					this.OnDescripcionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Ejemplo", DbType="NVarChar(250)")]
		public string Ejemplo
		{
			get
			{
				return this._Ejemplo;
			}
			set
			{
				if ((this._Ejemplo != value))
				{
					this.OnEjemploChanging(value);
					this.SendPropertyChanging();
					this._Ejemplo = value;
					this.SendPropertyChanged("Ejemplo");
					this.OnEjemploChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Justificacion", DbType="NVarChar(MAX)")]
		public string Justificacion
		{
			get
			{
				return this._Justificacion;
			}
			set
			{
				if ((this._Justificacion != value))
				{
					this.OnJustificacionChanging(value);
					this.SendPropertyChanging();
					this._Justificacion = value;
					this.SendPropertyChanged("Justificacion");
					this.OnJustificacionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdSegmentacion1", DbType="Int")]
		public System.Nullable<int> IdSegmentacion1
		{
			get
			{
				return this._IdSegmentacion1;
			}
			set
			{
				if ((this._IdSegmentacion1 != value))
				{
					this.OnIdSegmentacion1Changing(value);
					this.SendPropertyChanging();
					this._IdSegmentacion1 = value;
					this.SendPropertyChanged("IdSegmentacion1");
					this.OnIdSegmentacion1Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdResponsable", DbType="Int")]
		public System.Nullable<int> IdResponsable
		{
			get
			{
				return this._IdResponsable;
			}
			set
			{
				if ((this._IdResponsable != value))
				{
					this.OnIdResponsableChanging(value);
					this.SendPropertyChanging();
					this._IdResponsable = value;
					this.SendPropertyChanged("IdResponsable");
					this.OnIdResponsableChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdSupervisor", DbType="Int")]
		public System.Nullable<int> IdSupervisor
		{
			get
			{
				return this._IdSupervisor;
			}
			set
			{
				if ((this._IdSupervisor != value))
				{
					this.OnIdSupervisorChanging(value);
					this.SendPropertyChanging();
					this._IdSupervisor = value;
					this.SendPropertyChanged("IdSupervisor");
					this.OnIdSupervisorChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdControlesOportunidad", DbType="Int NOT NULL")]
		public int IdControlesOportunidad
		{
			get
			{
				return this._IdControlesOportunidad;
			}
			set
			{
				if ((this._IdControlesOportunidad != value))
				{
					this.OnIdControlesOportunidadChanging(value);
					this.SendPropertyChanging();
					this._IdControlesOportunidad = value;
					this.SendPropertyChanged("IdControlesOportunidad");
					this.OnIdControlesOportunidadChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdControlesEfectividad", DbType="Int")]
		public System.Nullable<int> IdControlesEfectividad
		{
			get
			{
				return this._IdControlesEfectividad;
			}
			set
			{
				if ((this._IdControlesEfectividad != value))
				{
					this.OnIdControlesEfectividadChanging(value);
					this.SendPropertyChanging();
					this._IdControlesEfectividad = value;
					this.SendPropertyChanged("IdControlesEfectividad");
					this.OnIdControlesEfectividadChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Vigencia", DbType="NVarChar(20)")]
		public string Vigencia
		{
			get
			{
				return this._Vigencia;
			}
			set
			{
				if ((this._Vigencia != value))
				{
					this.OnVigenciaChanging(value);
					this.SendPropertyChanging();
					this._Vigencia = value;
					this.SendPropertyChanged("Vigencia");
					this.OnVigenciaChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="tRiesgo_tRiesgos_ControlOportunidad", Storage="_tRiesgos_ControlOportunidads", ThisKey="IdControlesOportunidad", OtherKey="IdControlOportunidad")]
		public EntitySet<tRiesgos_ControlOportunidad> tRiesgos_ControlOportunidads
		{
			get
			{
				return this._tRiesgos_ControlOportunidads;
			}
			set
			{
				this._tRiesgos_ControlOportunidads.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="tRiesgo_tRiesgos_Clasificaciones1", Storage="_tRiesgos_Clasificaciones1s", ThisKey="IdClasificacion1", OtherKey="IdEstructura")]
		public EntitySet<tRiesgos_Clasificaciones1> tRiesgos_Clasificaciones1s
		{
			get
			{
				return this._tRiesgos_Clasificaciones1s;
			}
			set
			{
				this._tRiesgos_Clasificaciones1s.Assign(value);
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
		
		private void attach_tRiesgos_ControlOportunidads(tRiesgos_ControlOportunidad entity)
		{
			this.SendPropertyChanging();
			entity.tRiesgo = this;
		}
		
		private void detach_tRiesgos_ControlOportunidads(tRiesgos_ControlOportunidad entity)
		{
			this.SendPropertyChanging();
			entity.tRiesgo = null;
		}
		
		private void attach_tRiesgos_Clasificaciones1s(tRiesgos_Clasificaciones1 entity)
		{
			this.SendPropertyChanging();
			entity.tRiesgo = this;
		}
		
		private void detach_tRiesgos_Clasificaciones1s(tRiesgos_Clasificaciones1 entity)
		{
			this.SendPropertyChanging();
			entity.tRiesgo = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.tRiesgos_ControlOportunidad")]
	public partial class tRiesgos_ControlOportunidad : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _IdControlOportunidad;
		
		private string _Oportunidad;
		
		private System.Nullable<int> _Orden;
		
		private EntityRef<tRiesgo> _tRiesgo;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdControlOportunidadChanging(int value);
    partial void OnIdControlOportunidadChanged();
    partial void OnOportunidadChanging(string value);
    partial void OnOportunidadChanged();
    partial void OnOrdenChanging(System.Nullable<int> value);
    partial void OnOrdenChanged();
    #endregion
		
		public tRiesgos_ControlOportunidad()
		{
			this._tRiesgo = default(EntityRef<tRiesgo>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdControlOportunidad", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int IdControlOportunidad
		{
			get
			{
				return this._IdControlOportunidad;
			}
			set
			{
				if ((this._IdControlOportunidad != value))
				{
					if (this._tRiesgo.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnIdControlOportunidadChanging(value);
					this.SendPropertyChanging();
					this._IdControlOportunidad = value;
					this.SendPropertyChanged("IdControlOportunidad");
					this.OnIdControlOportunidadChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Oportunidad", DbType="NVarChar(50)")]
		public string Oportunidad
		{
			get
			{
				return this._Oportunidad;
			}
			set
			{
				if ((this._Oportunidad != value))
				{
					this.OnOportunidadChanging(value);
					this.SendPropertyChanging();
					this._Oportunidad = value;
					this.SendPropertyChanged("Oportunidad");
					this.OnOportunidadChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Orden", DbType="Int")]
		public System.Nullable<int> Orden
		{
			get
			{
				return this._Orden;
			}
			set
			{
				if ((this._Orden != value))
				{
					this.OnOrdenChanging(value);
					this.SendPropertyChanging();
					this._Orden = value;
					this.SendPropertyChanged("Orden");
					this.OnOrdenChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="tRiesgo_tRiesgos_ControlOportunidad", Storage="_tRiesgo", ThisKey="IdControlOportunidad", OtherKey="IdControlesOportunidad", IsForeignKey=true)]
		public tRiesgo tRiesgo
		{
			get
			{
				return this._tRiesgo.Entity;
			}
			set
			{
				tRiesgo previousValue = this._tRiesgo.Entity;
				if (((previousValue != value) 
							|| (this._tRiesgo.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._tRiesgo.Entity = null;
						previousValue.tRiesgos_ControlOportunidads.Remove(this);
					}
					this._tRiesgo.Entity = value;
					if ((value != null))
					{
						value.tRiesgos_ControlOportunidads.Add(this);
						this._IdControlOportunidad = value.IdControlesOportunidad;
					}
					else
					{
						this._IdControlOportunidad = default(int);
					}
					this.SendPropertyChanged("tRiesgo");
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.tRiesgos_Clasificaciones")]
	public partial class tRiesgos_Clasificaciones1 : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _IdEstructura;
		
		private string _CodNivel;
		
		private string _Nombre;
		
		private System.Nullable<int> _Nivel;
		
		private System.Nullable<int> _idPadre;
		
		private System.Nullable<int> _Orden;
		
		private string _CodCompleto;
		
		private EntityRef<tRiesgo> _tRiesgo;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdEstructuraChanging(int value);
    partial void OnIdEstructuraChanged();
    partial void OnCodNivelChanging(string value);
    partial void OnCodNivelChanged();
    partial void OnNombreChanging(string value);
    partial void OnNombreChanged();
    partial void OnNivelChanging(System.Nullable<int> value);
    partial void OnNivelChanged();
    partial void OnidPadreChanging(System.Nullable<int> value);
    partial void OnidPadreChanged();
    partial void OnOrdenChanging(System.Nullable<int> value);
    partial void OnOrdenChanged();
    partial void OnCodCompletoChanging(string value);
    partial void OnCodCompletoChanged();
    #endregion
		
		public tRiesgos_Clasificaciones1()
		{
			this._tRiesgo = default(EntityRef<tRiesgo>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdEstructura", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int IdEstructura
		{
			get
			{
				return this._IdEstructura;
			}
			set
			{
				if ((this._IdEstructura != value))
				{
					if (this._tRiesgo.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnIdEstructuraChanging(value);
					this.SendPropertyChanging();
					this._IdEstructura = value;
					this.SendPropertyChanged("IdEstructura");
					this.OnIdEstructuraChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CodNivel", DbType="NVarChar(10)")]
		public string CodNivel
		{
			get
			{
				return this._CodNivel;
			}
			set
			{
				if ((this._CodNivel != value))
				{
					this.OnCodNivelChanging(value);
					this.SendPropertyChanging();
					this._CodNivel = value;
					this.SendPropertyChanged("CodNivel");
					this.OnCodNivelChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Nombre", DbType="NVarChar(250)")]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Nivel", DbType="Int")]
		public System.Nullable<int> Nivel
		{
			get
			{
				return this._Nivel;
			}
			set
			{
				if ((this._Nivel != value))
				{
					this.OnNivelChanging(value);
					this.SendPropertyChanging();
					this._Nivel = value;
					this.SendPropertyChanged("Nivel");
					this.OnNivelChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_idPadre", DbType="Int")]
		public System.Nullable<int> idPadre
		{
			get
			{
				return this._idPadre;
			}
			set
			{
				if ((this._idPadre != value))
				{
					this.OnidPadreChanging(value);
					this.SendPropertyChanging();
					this._idPadre = value;
					this.SendPropertyChanged("idPadre");
					this.OnidPadreChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Orden", DbType="Int")]
		public System.Nullable<int> Orden
		{
			get
			{
				return this._Orden;
			}
			set
			{
				if ((this._Orden != value))
				{
					this.OnOrdenChanging(value);
					this.SendPropertyChanging();
					this._Orden = value;
					this.SendPropertyChanged("Orden");
					this.OnOrdenChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CodCompleto", DbType="NVarChar(250)")]
		public string CodCompleto
		{
			get
			{
				return this._CodCompleto;
			}
			set
			{
				if ((this._CodCompleto != value))
				{
					this.OnCodCompletoChanging(value);
					this.SendPropertyChanging();
					this._CodCompleto = value;
					this.SendPropertyChanged("CodCompleto");
					this.OnCodCompletoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="tRiesgo_tRiesgos_Clasificaciones1", Storage="_tRiesgo", ThisKey="IdEstructura", OtherKey="IdClasificacion1", IsForeignKey=true)]
		public tRiesgo tRiesgo
		{
			get
			{
				return this._tRiesgo.Entity;
			}
			set
			{
				tRiesgo previousValue = this._tRiesgo.Entity;
				if (((previousValue != value) 
							|| (this._tRiesgo.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._tRiesgo.Entity = null;
						previousValue.tRiesgos_Clasificaciones1s.Remove(this);
					}
					this._tRiesgo.Entity = value;
					if ((value != null))
					{
						value.tRiesgos_Clasificaciones1s.Add(this);
						this._IdEstructura = value.IdClasificacion1;
					}
					else
					{
						this._IdEstructura = default(int);
					}
					this.SendPropertyChanged("tRiesgo");
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.tRiesgos_Clasificaciones")]
	public partial class tRiesgos_Clasificaciones2 : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _IdEstructura;
		
		private string _CodNivel;
		
		private string _Nombre;
		
		private System.Nullable<int> _Nivel;
		
		private System.Nullable<int> _idPadre;
		
		private System.Nullable<int> _Orden;
		
		private string _CodCompleto;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdEstructuraChanging(int value);
    partial void OnIdEstructuraChanged();
    partial void OnCodNivelChanging(string value);
    partial void OnCodNivelChanged();
    partial void OnNombreChanging(string value);
    partial void OnNombreChanged();
    partial void OnNivelChanging(System.Nullable<int> value);
    partial void OnNivelChanged();
    partial void OnidPadreChanging(System.Nullable<int> value);
    partial void OnidPadreChanged();
    partial void OnOrdenChanging(System.Nullable<int> value);
    partial void OnOrdenChanged();
    partial void OnCodCompletoChanging(string value);
    partial void OnCodCompletoChanged();
    #endregion
		
		public tRiesgos_Clasificaciones2()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdEstructura", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int IdEstructura
		{
			get
			{
				return this._IdEstructura;
			}
			set
			{
				if ((this._IdEstructura != value))
				{
					this.OnIdEstructuraChanging(value);
					this.SendPropertyChanging();
					this._IdEstructura = value;
					this.SendPropertyChanged("IdEstructura");
					this.OnIdEstructuraChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CodNivel", DbType="NVarChar(10)")]
		public string CodNivel
		{
			get
			{
				return this._CodNivel;
			}
			set
			{
				if ((this._CodNivel != value))
				{
					this.OnCodNivelChanging(value);
					this.SendPropertyChanging();
					this._CodNivel = value;
					this.SendPropertyChanged("CodNivel");
					this.OnCodNivelChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Nombre", DbType="NVarChar(250)")]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Nivel", DbType="Int")]
		public System.Nullable<int> Nivel
		{
			get
			{
				return this._Nivel;
			}
			set
			{
				if ((this._Nivel != value))
				{
					this.OnNivelChanging(value);
					this.SendPropertyChanging();
					this._Nivel = value;
					this.SendPropertyChanged("Nivel");
					this.OnNivelChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_idPadre", DbType="Int")]
		public System.Nullable<int> idPadre
		{
			get
			{
				return this._idPadre;
			}
			set
			{
				if ((this._idPadre != value))
				{
					this.OnidPadreChanging(value);
					this.SendPropertyChanging();
					this._idPadre = value;
					this.SendPropertyChanged("idPadre");
					this.OnidPadreChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Orden", DbType="Int")]
		public System.Nullable<int> Orden
		{
			get
			{
				return this._Orden;
			}
			set
			{
				if ((this._Orden != value))
				{
					this.OnOrdenChanging(value);
					this.SendPropertyChanging();
					this._Orden = value;
					this.SendPropertyChanged("Orden");
					this.OnOrdenChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CodCompleto", DbType="NVarChar(250)")]
		public string CodCompleto
		{
			get
			{
				return this._CodCompleto;
			}
			set
			{
				if ((this._CodCompleto != value))
				{
					this.OnCodCompletoChanging(value);
					this.SendPropertyChanging();
					this._CodCompleto = value;
					this.SendPropertyChanged("CodCompleto");
					this.OnCodCompletoChanged();
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.tRiesgos_Clasificaciones")]
	public partial class tRiesgos_Clasificaciones3 : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _IdEstructura;
		
		private string _CodNivel;
		
		private string _Nombre;
		
		private System.Nullable<int> _Nivel;
		
		private System.Nullable<int> _idPadre;
		
		private System.Nullable<int> _Orden;
		
		private string _CodCompleto;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdEstructuraChanging(int value);
    partial void OnIdEstructuraChanged();
    partial void OnCodNivelChanging(string value);
    partial void OnCodNivelChanged();
    partial void OnNombreChanging(string value);
    partial void OnNombreChanged();
    partial void OnNivelChanging(System.Nullable<int> value);
    partial void OnNivelChanged();
    partial void OnidPadreChanging(System.Nullable<int> value);
    partial void OnidPadreChanged();
    partial void OnOrdenChanging(System.Nullable<int> value);
    partial void OnOrdenChanged();
    partial void OnCodCompletoChanging(string value);
    partial void OnCodCompletoChanged();
    #endregion
		
		public tRiesgos_Clasificaciones3()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdEstructura", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int IdEstructura
		{
			get
			{
				return this._IdEstructura;
			}
			set
			{
				if ((this._IdEstructura != value))
				{
					this.OnIdEstructuraChanging(value);
					this.SendPropertyChanging();
					this._IdEstructura = value;
					this.SendPropertyChanged("IdEstructura");
					this.OnIdEstructuraChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CodNivel", DbType="NVarChar(10)")]
		public string CodNivel
		{
			get
			{
				return this._CodNivel;
			}
			set
			{
				if ((this._CodNivel != value))
				{
					this.OnCodNivelChanging(value);
					this.SendPropertyChanging();
					this._CodNivel = value;
					this.SendPropertyChanged("CodNivel");
					this.OnCodNivelChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Nombre", DbType="NVarChar(250)")]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Nivel", DbType="Int")]
		public System.Nullable<int> Nivel
		{
			get
			{
				return this._Nivel;
			}
			set
			{
				if ((this._Nivel != value))
				{
					this.OnNivelChanging(value);
					this.SendPropertyChanging();
					this._Nivel = value;
					this.SendPropertyChanged("Nivel");
					this.OnNivelChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_idPadre", DbType="Int")]
		public System.Nullable<int> idPadre
		{
			get
			{
				return this._idPadre;
			}
			set
			{
				if ((this._idPadre != value))
				{
					this.OnidPadreChanging(value);
					this.SendPropertyChanging();
					this._idPadre = value;
					this.SendPropertyChanged("idPadre");
					this.OnidPadreChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Orden", DbType="Int")]
		public System.Nullable<int> Orden
		{
			get
			{
				return this._Orden;
			}
			set
			{
				if ((this._Orden != value))
				{
					this.OnOrdenChanging(value);
					this.SendPropertyChanging();
					this._Orden = value;
					this.SendPropertyChanged("Orden");
					this.OnOrdenChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CodCompleto", DbType="NVarChar(250)")]
		public string CodCompleto
		{
			get
			{
				return this._CodCompleto;
			}
			set
			{
				if ((this._CodCompleto != value))
				{
					this.OnCodCompletoChanging(value);
					this.SendPropertyChanging();
					this._CodCompleto = value;
					this.SendPropertyChanged("CodCompleto");
					this.OnCodCompletoChanged();
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
