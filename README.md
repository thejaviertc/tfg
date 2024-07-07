# TFG

## Requisitos de Instalación

- **Instalar .NET 8**

  [Descargar .NET 8](https://dotnet.microsoft.com/es-es/download/dotnet/thank-you/sdk-8.0.302-windows-x64-installer)

- **Instalar NodeJS 20**

  [Descargar NodeJS 20](https://nodejs.org/dist/v20.15.0/node-v20.15.0-x64.msi)

- **Instalar MySQL 8.0.38 (Server Only) con MySQL Installer**

  [Descargar MySQL 8.0.38](https://dev.mysql.com/get/Downloads/MySQLInstaller/mysql-installer-community-8.0.38.0.msi)

## Configuración de la Base de Datos

- **Crear una Base de Datos**

  Utiliza MySQL Command Line Client (incluido con MySQL Server) o MySQL Workbench para crear la base de datos:

  ```sql
  CREATE DATABASE nombre_bbdd_que_quieras;
  ```

## Configuración del Proyecto

- **Descargar Repositorio desde Github (Code -> Download ZIP)**

  [https://github.com/thejaviertc/tfg](https://github.com/thejaviertc/tfg)

- **Abrir un terminal en la carpeta `/src` del proyecto e inicializar los secretos de usuario**

  ```sh
  dotnet user-secrets init
  ```

- **Mover la carpeta 35b70688-c077-47af-befa-6500ff4a730b que está en la raíz del proyecto a %appdata%/Microsoft/UserSecrets.**

  También se puede realizar usando una extensión en Visual Studio Code para gestionar los secretos de una aplicación de .NET, o con Visual Studio 2022 nativamente. He decidido explicarlo así para evitar fallos.

  Quedará en `C:\Users\NOMBRE_USUARIO\AppData\Roaming\Microsoft\UserSecrets`.

- **Modificar el fichero secrets.json incluido en la carpeta 35b70688-c077-47af-befa-6500ff4a730b con los valores necesarios:**

  Cambia database por el nombre de la BBDD, uid por el nombre del usuario de la BBDD y pwd por la contraseña del usuario.

  El key que hay dentro de JWT es la clave que se usa para generar los JWT. Como esta aplicación no va a ser usada públicamente, da igual qué valor tenga, por lo que he dejado en la plantilla el que he usado.

- **Con el terminal utilizado anteriormente, inicia la aplicación utilizando**

  ```sh
  dotnet run
  ```

  Debería aparecer automáticamente la página web. Si no es así, en el terminal haz click en la ruta en la que se ha iniciado en localhost.

  Si aparece un aviso de seguridad es debido a que no tiene certificado HTTPS o porque está firmado por la página misma, por lo que habrá que pulsar en ignorar (o el botón que sea para ignorar el aviso que dependerá del navegador usado).
