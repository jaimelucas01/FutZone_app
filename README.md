⚽ FutZone - Sistema de Gestión para Complejos de Fútbol
📌 Descripción General

FutZone es un sistema de gestión desarrollado en C# (WinForms) con SQL Server, diseñado para administrar la operación diaria de un complejo de fútbol.

El sistema permite gestionar clientes, reservas de canchas, control de pagos y cierre de caja, integrando mecanismos de seguridad, control de acceso y auditoría lógica de datos.

Este proyecto fue desarrollado en el marco de las materias Ingeniería de Software y Trabajo de Diploma, aplicando buenas prácticas de diseño, seguridad y gestión de datos.

🏗️ Arquitectura del Sistema

El sistema sigue una arquitectura en capas simple, orientada a aplicaciones de escritorio:

Capa de Presentación (UI):
WinForms
Navegación mediante MenuStrip
Formularios modales (ShowDialog())
Capa de Lógica de Negocio:
Validaciones
Reglas de negocio (reservas, pagos, caja)
Capa de Acceso a Datos:
Conexión a SQL Server
Operaciones CRUD
Implementación de baja lógica
🗄️ Base de Datos

Motor: SQL Server
Base de datos: FutZone_DB

📊 Tablas principales:
Usuarios
Grupos
Clientes
Reservas
🔁 Estrategia de Eliminación

Se implementa baja lógica, utilizando un campo:

Estado = 'ACTIVO' / 'INACTIVO'

Esto permite:

Auditoría de datos
Recuperación de información
Evitar pérdida de integridad
🔐 Seguridad
✅ Autenticación
Login contra base de datos
Validación de credenciales en tiempo real
🔒 Encriptación
Uso de SHA256 mediante:
Seguridad.EncriptarSHA256()
Protección de contraseñas almacenadas
👤 Autorización

Se implementa un patrón Singleton mediante la clase Sesion:

Almacena:
ID
Nombre
Usuario
Permite:
Control de acceso
Restricción de funcionalidades

Ejemplo:

Usuarios admin → acceso total
Usuarios comunes → menús restringidos
⚙️ Funcionalidades Principales
👥 ABM de Clientes
Alta, baja lógica y modificación
Validación de campos obligatorios
Persistencia en base de datos
📅 Gestión de Reservas
Registro de turnos
Asociación con clientes
Control de disponibilidad
💰 Caja y Reportes
Registro de pagos (Pagado = true/false)
Cálculo de ingresos diarios:
SUM(Total)
Cierre de caja diario
🎨 Interfaz de Usuario
Navegación mediante MenuStrip:
Archivo
Gestión
Seguridad
Uso de formularios modales:
ShowDialog()

✔ Ventajas:

Control del flujo
Prevención de acciones simultáneas inválidas
🧩 Patrones de Diseño Aplicados
Singleton
Clase Sesion
Gestión centralizada del usuario logueado
Separación de responsabilidades
UI / Lógica / Datos
Baja lógica (Soft Delete Pattern)
Preservación de información
📏 Métricas del Sistema

Algunas métricas relevantes consideradas:

🔹 Complejidad funcional:
Módulos independientes (Clientes, Reservas, Caja)
🔹 Acoplamiento:
Bajo entre UI y lógica de negocio
🔹 Cohesión:
Alta dentro de cada módulo
🔹 Seguridad:
Uso de hashing (SHA256)
Control de sesiones
⚠️ Riesgos Identificados
🔸 Seguridad básica
SHA256 sin salt → posible mejora futura
🔸 Escalabilidad limitada
Arquitectura WinForms no orientada a sistemas distribuidos
🔸 Concurrencia
Posibles conflictos en reservas simultáneas
🔸 Auditoría limitada
No hay logs detallados de acciones (solo baja lógica)
🔍 Auditoría y Control

El sistema permite auditoría básica mediante:

Baja lógica (Estado)
Registro de reservas y pagos
Control de usuarios activos

🔧 Mejoras futuras:

Logs de actividad
Historial de cambios
Tracking de operaciones críticas
🚀 Posibles Mejoras
Implementación de roles más avanzados
Migración a arquitectura web (ASP.NET / API)
Sistema de notificaciones
Integración con pagos online
Auditoría completa (logs + trazabilidad)
Uso de JWT o OAuth para seguridad moderna
🛠️ Tecnologías Utilizadas
C#
WinForms
SQL Server
ADO.NET
📂 Instalación y Ejecución
Clonar el repositorio:
git clone https://github.com/tu-usuario/futzone.git
Configurar la conexión a SQL Server en el proyecto
Ejecutar la base de datos:
Script de creación (FutZone_DB)
Ejecutar el proyecto desde Visual Studio
👨‍💻 Autor

Proyecto desarrollado por:

Lucas Jaime.

📄 Licencia

Uso académico – Proyecto universitario.
