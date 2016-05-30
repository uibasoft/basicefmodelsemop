#Basic EF Model Semop

Proyecto Semop usando la tecnología ADO.NET Entity Framework Model First  y ADO.NET MVC 4. Este proyecto es usado en el curso de capacitación e introducción Entity Framework Basic

##Introducción

Este proyecto es usado en el curso de capacitación e introducción Entity Framework Basic. Se muestra el uso de la tecnología ADO.NET Entity Framework, explicando el ADO.NET Entity Data Model Designer, bajo el concepto Model First.

ADO.NET Entity Framework es un conjunto de API de acceso a datos para el Microsoft .NET Framework, apuntando a la versión de ADO.NET que se incluye con el .NET Framework 3.5.
Una entidad del Entity Framework es un objeto que tiene una clave representando la clave primaria de una entidad lógica de datastore. Un modelo conceptual Entity Data Model (modelo Entidad-Relación) es mapeado a un modelo de esquema de datastore. Usando el Entity Data Model, el Framework permite que los datos sean tratados como entidades independientemente de sus representaciones del datastore subyacente.

El Entity SQL es un lenguaje similar al SQL para consultar el Entity Data Model (en vez del datastore subyacente). Similarmente, las extensiones del Linq, Linq-to-Entities, proporcionan consultas tipeadas en el Entity Data Model. Las consultas Entity SQL y Linq-to-Entities son convertidas internamente en un Canonical Query Tree que entonces es convertido en una consulta comprensible al datastore subyacente (ej. en SQL en el caso de una base de datos relacional). Las entidades pueden utilizar sus relaciones, y sus cambios enviados de regreso al datastore.

##Instalación

Se requeriere las siguientes herramientas:

- Visual Studio Community 2015
- Sql Server Express 2014
