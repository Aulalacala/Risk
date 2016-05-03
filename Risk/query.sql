select * from dbo.tEstructura where idPadre =0;
select * from dbo.tEstructura where idPadre =209 order by Orden;

select * from dbo.tEstructura where idPadre= 213 order by Orden;
select * from dbo.tEstructura where idPadre= 246 order by Orden;

--si no sale nada es que no tiene mas hijos de cara a riesgos
--si tiene, dara una lista con los riesgos, que hay que pintar en la derecha
select * from dbo.tRelEstructuraRiesgos where IdEstructura =246; 
select * from dbo.tRiesgos where IdRiesgo = 1526;

select COUNT(Orden)from dbo.tEstructura;

select * from qEstructura_Contenidos_Def where IdEstructura = 213;