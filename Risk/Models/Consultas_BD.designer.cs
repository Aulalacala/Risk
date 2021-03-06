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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="RiskMVC")]
	public partial class Consultas_BDDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InserttPlanesAccion_Estados(tPlanesAccion_Estados instance);
    partial void UpdatetPlanesAccion_Estados(tPlanesAccion_Estados instance);
    partial void DeletetPlanesAccion_Estados(tPlanesAccion_Estados instance);
    partial void InserttPlanesAccion_Mitigacion(tPlanesAccion_Mitigacion instance);
    partial void UpdatetPlanesAccion_Mitigacion(tPlanesAccion_Mitigacion instance);
    partial void DeletetPlanesAccion_Mitigacion(tPlanesAccion_Mitigacion instance);
    partial void InserttPlanesAccion_Prioridades(tPlanesAccion_Prioridades instance);
    partial void UpdatetPlanesAccion_Prioridades(tPlanesAccion_Prioridades instance);
    partial void DeletetPlanesAccion_Prioridades(tPlanesAccion_Prioridades instance);
    #endregion
		
		public Consultas_BDDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["RiskMVCConnectionString1"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public Consultas_BDDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public Consultas_BDDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public Consultas_BDDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public Consultas_BDDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<qIndicadores> qIndicadores
		{
			get
			{
				return this.GetTable<qIndicadores>();
			}
		}
		
		public System.Data.Linq.Table<qPlanes> qPlanes
		{
			get
			{
				return this.GetTable<qPlanes>();
			}
		}
		
		public System.Data.Linq.Table<tPlanesAccion_Estados> tPlanesAccion_Estados
		{
			get
			{
				return this.GetTable<tPlanesAccion_Estados>();
			}
		}
		
		public System.Data.Linq.Table<tPlanesAccion_Mitigacion> tPlanesAccion_Mitigacion
		{
			get
			{
				return this.GetTable<tPlanesAccion_Mitigacion>();
			}
		}
		
		public System.Data.Linq.Table<tPlanesAccion_Prioridades> tPlanesAccion_Prioridades
		{
			get
			{
				return this.GetTable<tPlanesAccion_Prioridades>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.qIndicadores")]
	public partial class qIndicadores
	{
		
		private string _Estado;
		
		private string _Color;
		
		private string _CodEstado;
		
		private int _IdIndicador;
		
		private string _CodIndicador;
		
		private string _Indicador;
		
		private string _FormulaCalculo;
		
		private System.Nullable<int> _UltimoValor;
		
		private System.Nullable<int> _NivelAlarma;
		
		private System.Nullable<int> _NivelPrecaucion;
		
		private System.Nullable<System.DateTime> _UltimaFecha;
		
		private string _NivelAlarmaAccion;
		
		private System.Nullable<int> _IdEstado;
		
		private string _NivelAlarmaEscalamiento;
		
		private string _NivelPrecaucionAccion;
		
		private string _NivelPrecaucionEscalamiento;
		
		private System.Nullable<int> _IdNivelAlarmaAccion;
		
		private System.Nullable<int> _idResponsable;
		
		private string _Nombre;
		
		private System.Nullable<int> _IdNivelAlarmaEscalamiento;
		
		private System.Nullable<int> _IndiiEvolucion;
		
		private System.Nullable<int> _IdSelec1;
		
		private System.Nullable<int> _IdSelec2;
		
		private System.Nullable<int> _IdPeriodicidad;
		
		private string _EstadoPeriodicidad;
		
		public qIndicadores()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Estado", DbType="NVarChar(50)")]
		public string Estado
		{
			get
			{
				return this._Estado;
			}
			set
			{
				if ((this._Estado != value))
				{
					this._Estado = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Color", DbType="NVarChar(50)")]
		public string Color
		{
			get
			{
				return this._Color;
			}
			set
			{
				if ((this._Color != value))
				{
					this._Color = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CodEstado", DbType="NVarChar(10)")]
		public string CodEstado
		{
			get
			{
				return this._CodEstado;
			}
			set
			{
				if ((this._CodEstado != value))
				{
					this._CodEstado = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdIndicador", DbType="Int NOT NULL")]
		public int IdIndicador
		{
			get
			{
				return this._IdIndicador;
			}
			set
			{
				if ((this._IdIndicador != value))
				{
					this._IdIndicador = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CodIndicador", DbType="NVarChar(20)")]
		public string CodIndicador
		{
			get
			{
				return this._CodIndicador;
			}
			set
			{
				if ((this._CodIndicador != value))
				{
					this._CodIndicador = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Indicador", DbType="NVarChar(50)")]
		public string Indicador
		{
			get
			{
				return this._Indicador;
			}
			set
			{
				if ((this._Indicador != value))
				{
					this._Indicador = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FormulaCalculo", DbType="NVarChar(250)")]
		public string FormulaCalculo
		{
			get
			{
				return this._FormulaCalculo;
			}
			set
			{
				if ((this._FormulaCalculo != value))
				{
					this._FormulaCalculo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UltimoValor", DbType="Int")]
		public System.Nullable<int> UltimoValor
		{
			get
			{
				return this._UltimoValor;
			}
			set
			{
				if ((this._UltimoValor != value))
				{
					this._UltimoValor = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NivelAlarma", DbType="Int")]
		public System.Nullable<int> NivelAlarma
		{
			get
			{
				return this._NivelAlarma;
			}
			set
			{
				if ((this._NivelAlarma != value))
				{
					this._NivelAlarma = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NivelPrecaucion", DbType="Int")]
		public System.Nullable<int> NivelPrecaucion
		{
			get
			{
				return this._NivelPrecaucion;
			}
			set
			{
				if ((this._NivelPrecaucion != value))
				{
					this._NivelPrecaucion = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UltimaFecha", DbType="DateTime")]
		public System.Nullable<System.DateTime> UltimaFecha
		{
			get
			{
				return this._UltimaFecha;
			}
			set
			{
				if ((this._UltimaFecha != value))
				{
					this._UltimaFecha = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NivelAlarmaAccion", DbType="NVarChar(250)")]
		public string NivelAlarmaAccion
		{
			get
			{
				return this._NivelAlarmaAccion;
			}
			set
			{
				if ((this._NivelAlarmaAccion != value))
				{
					this._NivelAlarmaAccion = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdEstado", DbType="Int")]
		public System.Nullable<int> IdEstado
		{
			get
			{
				return this._IdEstado;
			}
			set
			{
				if ((this._IdEstado != value))
				{
					this._IdEstado = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NivelAlarmaEscalamiento", DbType="NVarChar(250)")]
		public string NivelAlarmaEscalamiento
		{
			get
			{
				return this._NivelAlarmaEscalamiento;
			}
			set
			{
				if ((this._NivelAlarmaEscalamiento != value))
				{
					this._NivelAlarmaEscalamiento = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NivelPrecaucionAccion", DbType="NVarChar(250)")]
		public string NivelPrecaucionAccion
		{
			get
			{
				return this._NivelPrecaucionAccion;
			}
			set
			{
				if ((this._NivelPrecaucionAccion != value))
				{
					this._NivelPrecaucionAccion = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NivelPrecaucionEscalamiento", DbType="NVarChar(250)")]
		public string NivelPrecaucionEscalamiento
		{
			get
			{
				return this._NivelPrecaucionEscalamiento;
			}
			set
			{
				if ((this._NivelPrecaucionEscalamiento != value))
				{
					this._NivelPrecaucionEscalamiento = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdNivelAlarmaAccion", DbType="Int")]
		public System.Nullable<int> IdNivelAlarmaAccion
		{
			get
			{
				return this._IdNivelAlarmaAccion;
			}
			set
			{
				if ((this._IdNivelAlarmaAccion != value))
				{
					this._IdNivelAlarmaAccion = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_idResponsable", DbType="Int")]
		public System.Nullable<int> idResponsable
		{
			get
			{
				return this._idResponsable;
			}
			set
			{
				if ((this._idResponsable != value))
				{
					this._idResponsable = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Nombre", DbType="NVarChar(100)")]
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
					this._Nombre = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdNivelAlarmaEscalamiento", DbType="Int")]
		public System.Nullable<int> IdNivelAlarmaEscalamiento
		{
			get
			{
				return this._IdNivelAlarmaEscalamiento;
			}
			set
			{
				if ((this._IdNivelAlarmaEscalamiento != value))
				{
					this._IdNivelAlarmaEscalamiento = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IndiiEvolucion", DbType="Int")]
		public System.Nullable<int> IndiiEvolucion
		{
			get
			{
				return this._IndiiEvolucion;
			}
			set
			{
				if ((this._IndiiEvolucion != value))
				{
					this._IndiiEvolucion = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdSelec1", DbType="Int")]
		public System.Nullable<int> IdSelec1
		{
			get
			{
				return this._IdSelec1;
			}
			set
			{
				if ((this._IdSelec1 != value))
				{
					this._IdSelec1 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdSelec2", DbType="Int")]
		public System.Nullable<int> IdSelec2
		{
			get
			{
				return this._IdSelec2;
			}
			set
			{
				if ((this._IdSelec2 != value))
				{
					this._IdSelec2 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdPeriodicidad", DbType="Int")]
		public System.Nullable<int> IdPeriodicidad
		{
			get
			{
				return this._IdPeriodicidad;
			}
			set
			{
				if ((this._IdPeriodicidad != value))
				{
					this._IdPeriodicidad = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EstadoPeriodicidad", DbType="NVarChar(255)")]
		public string EstadoPeriodicidad
		{
			get
			{
				return this._EstadoPeriodicidad;
			}
			set
			{
				if ((this._EstadoPeriodicidad != value))
				{
					this._EstadoPeriodicidad = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.qPlanes")]
	public partial class qPlanes
	{
		
		private int _IdPlanAccion;
		
		private string _CodPlanAccion;
		
		private string _Nombre;
		
		private string _IdEsructura;
		
		private string _Medidas;
		
		private System.Nullable<double> _PrevistoCosteTiempo;
		
		private System.Nullable<double> _PrevistoCosteEuros;
		
		private System.Nullable<double> _RealCosteTiempo;
		
		private System.Nullable<double> _RealCosteEuros;
		
		private System.Nullable<int> _IdPrioridad;
		
		private System.Nullable<int> _IdMitigacionPotencial;
		
		private string _Accion;
		
		private bool _Activa;
		
		private System.Nullable<int> _IdResponsable;
		
		private System.Nullable<System.DateTime> _FechaFinTeorica;
		
		private System.Nullable<System.DateTime> _FechaFinReal;
		
		private System.Nullable<double> _DismiSev;
		
		private System.Nullable<double> _DismiFrec;
		
		private string _Responsable;
		
		private System.Nullable<int> _Expr1;
		
		private string _Prioridad;
		
		private string _ColorP;
		
		private string _Mitigacion;
		
		private string _ColorM;
		
		private System.Nullable<int> _IdEstado;
		
		public qPlanes()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdPlanAccion", DbType="Int NOT NULL")]
		public int IdPlanAccion
		{
			get
			{
				return this._IdPlanAccion;
			}
			set
			{
				if ((this._IdPlanAccion != value))
				{
					this._IdPlanAccion = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CodPlanAccion", DbType="NVarChar(50)")]
		public string CodPlanAccion
		{
			get
			{
				return this._CodPlanAccion;
			}
			set
			{
				if ((this._CodPlanAccion != value))
				{
					this._CodPlanAccion = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Nombre", DbType="NVarChar(50)")]
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
					this._Nombre = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdEsructura", DbType="NVarChar(50)")]
		public string IdEsructura
		{
			get
			{
				return this._IdEsructura;
			}
			set
			{
				if ((this._IdEsructura != value))
				{
					this._IdEsructura = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Medidas", DbType="NVarChar(MAX)")]
		public string Medidas
		{
			get
			{
				return this._Medidas;
			}
			set
			{
				if ((this._Medidas != value))
				{
					this._Medidas = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PrevistoCosteTiempo", DbType="Float")]
		public System.Nullable<double> PrevistoCosteTiempo
		{
			get
			{
				return this._PrevistoCosteTiempo;
			}
			set
			{
				if ((this._PrevistoCosteTiempo != value))
				{
					this._PrevistoCosteTiempo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PrevistoCosteEuros", DbType="Float")]
		public System.Nullable<double> PrevistoCosteEuros
		{
			get
			{
				return this._PrevistoCosteEuros;
			}
			set
			{
				if ((this._PrevistoCosteEuros != value))
				{
					this._PrevistoCosteEuros = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RealCosteTiempo", DbType="Float")]
		public System.Nullable<double> RealCosteTiempo
		{
			get
			{
				return this._RealCosteTiempo;
			}
			set
			{
				if ((this._RealCosteTiempo != value))
				{
					this._RealCosteTiempo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RealCosteEuros", DbType="Float")]
		public System.Nullable<double> RealCosteEuros
		{
			get
			{
				return this._RealCosteEuros;
			}
			set
			{
				if ((this._RealCosteEuros != value))
				{
					this._RealCosteEuros = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdPrioridad", DbType="Int")]
		public System.Nullable<int> IdPrioridad
		{
			get
			{
				return this._IdPrioridad;
			}
			set
			{
				if ((this._IdPrioridad != value))
				{
					this._IdPrioridad = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdMitigacionPotencial", DbType="Int")]
		public System.Nullable<int> IdMitigacionPotencial
		{
			get
			{
				return this._IdMitigacionPotencial;
			}
			set
			{
				if ((this._IdMitigacionPotencial != value))
				{
					this._IdMitigacionPotencial = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Accion", DbType="NVarChar(MAX)")]
		public string Accion
		{
			get
			{
				return this._Accion;
			}
			set
			{
				if ((this._Accion != value))
				{
					this._Accion = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Activa", DbType="Bit NOT NULL")]
		public bool Activa
		{
			get
			{
				return this._Activa;
			}
			set
			{
				if ((this._Activa != value))
				{
					this._Activa = value;
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
					this._IdResponsable = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FechaFinTeorica", DbType="DateTime")]
		public System.Nullable<System.DateTime> FechaFinTeorica
		{
			get
			{
				return this._FechaFinTeorica;
			}
			set
			{
				if ((this._FechaFinTeorica != value))
				{
					this._FechaFinTeorica = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FechaFinReal", DbType="DateTime")]
		public System.Nullable<System.DateTime> FechaFinReal
		{
			get
			{
				return this._FechaFinReal;
			}
			set
			{
				if ((this._FechaFinReal != value))
				{
					this._FechaFinReal = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DismiSev", DbType="Float")]
		public System.Nullable<double> DismiSev
		{
			get
			{
				return this._DismiSev;
			}
			set
			{
				if ((this._DismiSev != value))
				{
					this._DismiSev = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DismiFrec", DbType="Float")]
		public System.Nullable<double> DismiFrec
		{
			get
			{
				return this._DismiFrec;
			}
			set
			{
				if ((this._DismiFrec != value))
				{
					this._DismiFrec = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Responsable", DbType="NVarChar(100)")]
		public string Responsable
		{
			get
			{
				return this._Responsable;
			}
			set
			{
				if ((this._Responsable != value))
				{
					this._Responsable = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Expr1", DbType="Int")]
		public System.Nullable<int> Expr1
		{
			get
			{
				return this._Expr1;
			}
			set
			{
				if ((this._Expr1 != value))
				{
					this._Expr1 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Prioridad", DbType="NVarChar(50)")]
		public string Prioridad
		{
			get
			{
				return this._Prioridad;
			}
			set
			{
				if ((this._Prioridad != value))
				{
					this._Prioridad = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ColorP", DbType="NVarChar(50)")]
		public string ColorP
		{
			get
			{
				return this._ColorP;
			}
			set
			{
				if ((this._ColorP != value))
				{
					this._ColorP = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Mitigacion", DbType="NVarChar(50)")]
		public string Mitigacion
		{
			get
			{
				return this._Mitigacion;
			}
			set
			{
				if ((this._Mitigacion != value))
				{
					this._Mitigacion = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ColorM", DbType="NVarChar(50)")]
		public string ColorM
		{
			get
			{
				return this._ColorM;
			}
			set
			{
				if ((this._ColorM != value))
				{
					this._ColorM = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdEstado", DbType="Int")]
		public System.Nullable<int> IdEstado
		{
			get
			{
				return this._IdEstado;
			}
			set
			{
				if ((this._IdEstado != value))
				{
					this._IdEstado = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.tPlanesAccion_Estados")]
	public partial class tPlanesAccion_Estados : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _IdEstado;
		
		private string _CodEstado;
		
		private string _Estado;
		
		private System.Nullable<int> _Orden;
		
		private string _Color;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdEstadoChanging(int value);
    partial void OnIdEstadoChanged();
    partial void OnCodEstadoChanging(string value);
    partial void OnCodEstadoChanged();
    partial void OnEstadoChanging(string value);
    partial void OnEstadoChanged();
    partial void OnOrdenChanging(System.Nullable<int> value);
    partial void OnOrdenChanged();
    partial void OnColorChanging(string value);
    partial void OnColorChanged();
    #endregion
		
		public tPlanesAccion_Estados()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdEstado", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int IdEstado
		{
			get
			{
				return this._IdEstado;
			}
			set
			{
				if ((this._IdEstado != value))
				{
					this.OnIdEstadoChanging(value);
					this.SendPropertyChanging();
					this._IdEstado = value;
					this.SendPropertyChanged("IdEstado");
					this.OnIdEstadoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CodEstado", DbType="NVarChar(10)")]
		public string CodEstado
		{
			get
			{
				return this._CodEstado;
			}
			set
			{
				if ((this._CodEstado != value))
				{
					this.OnCodEstadoChanging(value);
					this.SendPropertyChanging();
					this._CodEstado = value;
					this.SendPropertyChanged("CodEstado");
					this.OnCodEstadoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Estado", DbType="NVarChar(50)")]
		public string Estado
		{
			get
			{
				return this._Estado;
			}
			set
			{
				if ((this._Estado != value))
				{
					this.OnEstadoChanging(value);
					this.SendPropertyChanging();
					this._Estado = value;
					this.SendPropertyChanged("Estado");
					this.OnEstadoChanged();
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Color", DbType="NVarChar(50)")]
		public string Color
		{
			get
			{
				return this._Color;
			}
			set
			{
				if ((this._Color != value))
				{
					this.OnColorChanging(value);
					this.SendPropertyChanging();
					this._Color = value;
					this.SendPropertyChanged("Color");
					this.OnColorChanged();
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.tPlanesAccion_Mitigacion")]
	public partial class tPlanesAccion_Mitigacion : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _IdMitigacion;
		
		private string _Mitigacion;
		
		private string _Color;
		
		private System.Nullable<int> _Valor;
		
		private System.Nullable<int> _Orden;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdMitigacionChanging(int value);
    partial void OnIdMitigacionChanged();
    partial void OnMitigacionChanging(string value);
    partial void OnMitigacionChanged();
    partial void OnColorChanging(string value);
    partial void OnColorChanged();
    partial void OnValorChanging(System.Nullable<int> value);
    partial void OnValorChanged();
    partial void OnOrdenChanging(System.Nullable<int> value);
    partial void OnOrdenChanged();
    #endregion
		
		public tPlanesAccion_Mitigacion()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdMitigacion", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int IdMitigacion
		{
			get
			{
				return this._IdMitigacion;
			}
			set
			{
				if ((this._IdMitigacion != value))
				{
					this.OnIdMitigacionChanging(value);
					this.SendPropertyChanging();
					this._IdMitigacion = value;
					this.SendPropertyChanged("IdMitigacion");
					this.OnIdMitigacionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Mitigacion", DbType="NVarChar(50)")]
		public string Mitigacion
		{
			get
			{
				return this._Mitigacion;
			}
			set
			{
				if ((this._Mitigacion != value))
				{
					this.OnMitigacionChanging(value);
					this.SendPropertyChanging();
					this._Mitigacion = value;
					this.SendPropertyChanged("Mitigacion");
					this.OnMitigacionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Color", DbType="NVarChar(50)")]
		public string Color
		{
			get
			{
				return this._Color;
			}
			set
			{
				if ((this._Color != value))
				{
					this.OnColorChanging(value);
					this.SendPropertyChanging();
					this._Color = value;
					this.SendPropertyChanged("Color");
					this.OnColorChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Valor", DbType="Int")]
		public System.Nullable<int> Valor
		{
			get
			{
				return this._Valor;
			}
			set
			{
				if ((this._Valor != value))
				{
					this.OnValorChanging(value);
					this.SendPropertyChanging();
					this._Valor = value;
					this.SendPropertyChanged("Valor");
					this.OnValorChanged();
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.tPlanesAccion_Prioridades")]
	public partial class tPlanesAccion_Prioridades : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _IdPrioridad;
		
		private string _Prioridad;
		
		private string _Color;
		
		private System.Nullable<int> _Valor;
		
		private System.Nullable<int> _Orden;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdPrioridadChanging(int value);
    partial void OnIdPrioridadChanged();
    partial void OnPrioridadChanging(string value);
    partial void OnPrioridadChanged();
    partial void OnColorChanging(string value);
    partial void OnColorChanged();
    partial void OnValorChanging(System.Nullable<int> value);
    partial void OnValorChanged();
    partial void OnOrdenChanging(System.Nullable<int> value);
    partial void OnOrdenChanged();
    #endregion
		
		public tPlanesAccion_Prioridades()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdPrioridad", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int IdPrioridad
		{
			get
			{
				return this._IdPrioridad;
			}
			set
			{
				if ((this._IdPrioridad != value))
				{
					this.OnIdPrioridadChanging(value);
					this.SendPropertyChanging();
					this._IdPrioridad = value;
					this.SendPropertyChanged("IdPrioridad");
					this.OnIdPrioridadChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Prioridad", DbType="NVarChar(50)")]
		public string Prioridad
		{
			get
			{
				return this._Prioridad;
			}
			set
			{
				if ((this._Prioridad != value))
				{
					this.OnPrioridadChanging(value);
					this.SendPropertyChanging();
					this._Prioridad = value;
					this.SendPropertyChanged("Prioridad");
					this.OnPrioridadChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Color", DbType="NVarChar(50)")]
		public string Color
		{
			get
			{
				return this._Color;
			}
			set
			{
				if ((this._Color != value))
				{
					this.OnColorChanging(value);
					this.SendPropertyChanging();
					this._Color = value;
					this.SendPropertyChanged("Color");
					this.OnColorChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Valor", DbType="Int")]
		public System.Nullable<int> Valor
		{
			get
			{
				return this._Valor;
			}
			set
			{
				if ((this._Valor != value))
				{
					this.OnValorChanging(value);
					this.SendPropertyChanging();
					this._Valor = value;
					this.SendPropertyChanged("Valor");
					this.OnValorChanged();
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
