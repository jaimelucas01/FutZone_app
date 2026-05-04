⚽ FutZone
Sistema de Gestión para Complejos de Fútbol
<p align="center"> <img src="https://img.shields.io/badge/C%23-WinForms-blue?style=for-the-badge&logo=csharp"> <img src="https://img.shields.io/badge/SQL%20Server-Database-red?style=for-the-badge&logo=microsoftsqlserver"> <img src="https://img.shields.io/badge/Status-En%20Desarrollo-yellow?style=for-the-badge"> <img src="https://img.shields.io/badge/License-Acad%C3%A9mico-lightgrey?style=for-the-badge"> </p>
📌 Overview

FutZone es un sistema de escritorio desarrollado en C# (WinForms) con SQL Server, diseñado para digitalizar la gestión operativa de complejos de fútbol.

Permite administrar clientes, reservas, pagos y control de caja, integrando mecanismos de seguridad, control de acceso y auditoría lógica.

📚 Proyecto desarrollado para:

Ingeniería de Software
Trabajo de Diploma
🏗️ Arquitectura

El sistema sigue una arquitectura en capas, enfocada en separación de responsabilidades:

📦 FutZone
 ┣ 📂 UI (WinForms)
 ┣ 📂 Lógica de Negocio
 ┗ 📂 Acceso a Datos
🔹 Características clave
Navegación mediante MenuStrip
Formularios modales (ShowDialog())
Separación UI / lógica / datos
Bajo acoplamiento, alta cohesión
🗄️ Base de Datos

Motor: SQL Server
Base: FutZone_DB

📊 Entidades principales
👤 Usuarios
👥 Grupos
🧍 Clientes
📅 Reservas
🔁 Soft Delete (Baja Lógica)
Estado = 'ACTIVO' / 'INACTIVO'

✔ Permite:

Auditoría
Recuperación de datos
Integridad del sistema
🔐 Seguridad
🔑 Autenticación
Login validado contra base de datos
🔒 Encriptación
Seguridad.EncriptarSHA256()
Protección de contraseñas
👤 Autorización

Implementación de Singleton (Sesion):

Usuario logueado en memoria
Control de permisos dinámico
Rol	Acceso
Admin	Completo
Usuario	Restringido
⚙️ Funcionalidades
👥 Gestión de Clientes
Alta / modificación / baja lógica
Validaciones de datos
📅 Gestión de Reservas
Registro de turnos
Asociación con clientes
Control básico de disponibilidad
💰 Caja & Reportes
Estado de pago (Pagado)
Cálculo de ingresos diarios:
SELECT SUM(Total) FROM Reservas
Cierre de caja
🎨 Interfaz
Menu principal con:
Archivo
Gestión
Seguridad
Formularios modales:
ShowDialog();

✔ Ventajas:

Flujo controlado
Prevención de errores
🧩 Patrones de Diseño
🧠 Singleton
Gestión de sesión (Sesion)
🧱 Separación de capas
UI / Negocio / Datos
♻️ Soft Delete
Persistencia segura
📏 Métricas de Ingeniería
🔹 Cohesión: Alta (módulos bien definidos)
🔹 Acoplamiento: Bajo
🔹 Seguridad: Media (hash SHA256)
🔹 Mantenibilidad: Alta
⚠️ Riesgos
❗ SHA256 sin salt → mejora pendiente
❗ No hay control de concurrencia en reservas
❗ Auditoría limitada (sin logs)
❗ Escalabilidad limitada (WinForms)
🔍 Auditoría

✔ Implementado:

Baja lógica
Registro de operaciones clave

🚧 Pendiente:

Logs de actividad
Historial de cambios
Tracking de usuarios
🚀 Roadmap / Mejoras Futuras
🔐 Roles avanzados (RBAC)
🌐 Migración a Web (ASP.NET / API)
💳 Integración con pagos online
📊 Dashboard de métricas
🧾 Sistema completo de auditoría
🔑 Autenticación moderna (JWT / OAuth)
🛠️ Stack Tecnológico
Tecnología	Uso
C#	Backend
WinForms	UI
SQL Server	Base de datos
ADO.NET	Acceso a datos
📂 Instalación
git clone https://github.com/tu-usuario/futzone.git
Pasos:
Configurar conexión a SQL Server
Ejecutar script de base de datos
Abrir en Visual Studio
Ejecutar proyecto
👨‍💻 Autor

Lucas Jaime

📄 Licencia

Uso académico — Proyecto universitario.

⭐ Notas Finales

Este proyecto aplica conceptos reales de:

Ingeniería de Software
Seguridad básica
Arquitectura en capas
Gestión de datos
