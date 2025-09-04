using System.Data.SqlClient;
using System.Data;
using System;

public class BasedeDatos_456VG
{
    public static string dataSource = "DESKTOP-Q714KGU\\SQLEXPRESS";
    public static string dbName = "EnviosYA_456VG";
    public static string conexionMaster =
        $"Data Source={dataSource};" +
        "Initial Catalog=master;" +
        "Integrated Security=True;" +
        "MultipleActiveResultSets=True;";
    public static string conexionDB =
        $"Data Source={dataSource};" +
        $"Initial Catalog={dbName};" +
        "Integrated Security=True;" +
        "MultipleActiveResultSets=True;";
    public SqlConnection Connection;
    public SqlCommand Command;
    public BasedeDatos_456VG(bool apuntarAMaster = false)
    {
        string cs = apuntarAMaster ? conexionMaster : conexionDB;
        Connection = new SqlConnection(cs);
    }
    public bool Conectar456VG()
    {
        if (Connection.State == ConnectionState.Closed)
        {
            Connection.Open();
            return true;
        }
        return false;
    }
    public bool Desconectar456VG()
    {
        if (Connection.State == ConnectionState.Open)
        {
            Connection.Close();
            return true;
        }
        return false;
    }
    public bool ejecutarQuery456VG(string query)
    {
        try
        {
            Conectar456VG();
            Command = new SqlCommand(query, Connection);
            Command.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            Desconectar456VG();
        }
    }
    public void scriptInicio456VG()
    {
        var bdEnMaster = new BasedeDatos_456VG(apuntarAMaster: true);
        bool bdCreada = bdEnMaster.ejecutarQuery456VG($"IF DB_ID('{dbName}') IS NULL CREATE DATABASE {dbName};");
        if (bdCreada)
        {
            var dbReal = new BasedeDatos_456VG(apuntarAMaster: false);
            dbReal.ejecutarQuery456VG(
                "USE EnviosYA_456VG; " +
                "CREATE TABLE Rol_456VG (" +
                    "CodRol_456VG INT PRIMARY KEY IDENTITY, " +
                    "Nombre_456VG VARCHAR(50) NOT NULL UNIQUE" +
                ");" +
                "CREATE TABLE Permiso_456VG (" +
                    "CodPermiso_456VG INT PRIMARY KEY IDENTITY, " +
                    "Nombre_456VG VARCHAR(50) NOT NULL, " +
                    "IsFamilia_456VG BIT NOT NULL" +
                ");" +
                "CREATE TABLE FamiliaPermiso_456VG (" +
                    "CodFamilia_456VG INT NOT NULL, " +
                    "CodPermiso_456VG INT NOT NULL, " +
                    "PRIMARY KEY (CodFamilia_456VG, CodPermiso_456VG), " +
                    "FOREIGN KEY (CodFamilia_456VG) REFERENCES Permiso_456VG(CodPermiso_456VG), " +
                    "FOREIGN KEY (CodPermiso_456VG) REFERENCES Permiso_456VG(CodPermiso_456VG)" +
                ");" +
                "CREATE TABLE Rol_Permiso_456VG (" +
                    "CodRol_456VG INT NOT NULL, " +
                    "CodPermiso_456VG INT NOT NULL, " +
                    "PRIMARY KEY (CodRol_456VG, CodPermiso_456VG), " +
                    "FOREIGN KEY (CodRol_456VG) REFERENCES Rol_456VG(CodRol_456VG), " +
                    "FOREIGN KEY (CodPermiso_456VG) REFERENCES Permiso_456VG(CodPermiso_456VG)" +
                ");");
            dbReal.ejecutarQuery456VG(
                "USE EnviosYA_456VG; " +
                "CREATE TABLE Usuario_456VG (" +
                    "dni_456VG VARCHAR(20) PRIMARY KEY," +
                    "nombre_456VG VARCHAR(50) NOT NULL," +
                    "apellido_456VG VARCHAR(50) NOT NULL," +
                    "email_456VG VARCHAR(50) NOT NULL," +
                    "telefono_456VG VARCHAR(20) NOT NULL," +
                    "nombreusuario_456VG VARCHAR(50) NOT NULL," +
                    "contraseña_456VG VARCHAR(100) NOT NULL," +
                    "salt_456VG VARCHAR(24) NOT NULL," +
                    "domicilio_456VG VARCHAR(100) NOT NULL," +
                    "rol_456VG VARCHAR(100) NULL," +
                    "bloqueado_456VG BIT NOT NULL DEFAULT 0," +
                    "activo_456VG BIT NOT NULL DEFAULT 1," +
                    "idioma_456VG VARCHAR(50) NOT NULL DEFAULT 'ES'," +
                    "CodRol_456VG INT NULL, " +
                    "FOREIGN KEY (CodRol_456VG) REFERENCES Rol_456VG(CodRol_456VG) ON DELETE SET NULL" +
                ");"
            );
            dbReal.ejecutarQuery456VG(
                "USE EnviosYA_456VG; CREATE TABLE Clientes_456VG (" +
                    "dni_456VG VARCHAR(20) PRIMARY KEY," +
                    "nombre_456VG VARCHAR(100) NOT NULL," +
                    "apellido_456VG VARCHAR(100) NOT NULL," +
                    "telefono_456VG VARCHAR(100) NOT NULL," +
                    "domicilio_456VG VARCHAR(100) NOT NULL," +
                    "fechanacimiento_456VG DATE NOT NULL," +
                    "activo_456VG BIT NOT NULL DEFAULT 1" +
                ");");
            dbReal.ejecutarQuery456VG(
                "USE EnviosYA_456VG; CREATE TABLE Paquetes_456VG (" +
                    "codpaq_456VG VARCHAR(20) PRIMARY KEY," +
                    "dni_456VG VARCHAR(20) NOT NULL," +
                    "peso_456VG FLOAT NOT NULL," +
                    "ancho_456VG FLOAT NOT NULL," +
                    "alto_456VG FLOAT NOT NULL," +
                    "largo_456VG FLOAT NOT NULL," +
                    "enviado_456VG BIT NOT NULL DEFAULT 0," +
                    "CONSTRAINT fk_paquete_cliente FOREIGN KEY (dni_456VG) REFERENCES Clientes_456VG(dni_456VG)" +
                ");");
            dbReal.ejecutarQuery456VG(
                "USE EnviosYA_456VG; CREATE TABLE Envios_456VG (" +
                    "codenvio_456VG VARCHAR(20) PRIMARY KEY," +
                    "dni_cli_456VG VARCHAR(20) NOT NULL," +
                    "dni_dest_456VG VARCHAR(100) NOT NULL," +
                    "nombre_dest_456VG VARCHAR(100) NOT NULL," +
                    "apellido_dest_456VG VARCHAR(100) NOT NULL," +
                    "telefono_dest_456VG VARCHAR(100) NOT NULL," +
                    "provincia_456VG VARCHAR(100) NOT NULL," +
                    "localidad_456VG VARCHAR(100) NOT NULL," +
                    "domicilio_456VG VARCHAR(100) NOT NULL," +
                    "codpostal_456VG FLOAT NOT NULL," +
                    "tipoenvio_456VG VARCHAR(20) NOT NULL," +
                    "importe_456VG DECIMAL(10,2) NOT NULL," +
                    "pagado_456VG BIT NOT NULL DEFAULT 0," +
                    "CONSTRAINT fk_envio_cliente FOREIGN KEY (dni_cli_456VG) REFERENCES Clientes_456VG(dni_456VG)" +
                ");");
            dbReal.ejecutarQuery456VG(
                "USE EnviosYA_456VG; CREATE TABLE EnviosPaquetes_456VG (" +
                    "codenvio_456VG VARCHAR(20) NOT NULL," +
                    "codpaq_456VG VARCHAR(20) NOT NULL," +
                    "PRIMARY KEY (codenvio_456VG, codpaq_456VG)," +
                    "CONSTRAINT fk_ep_envio FOREIGN KEY (codenvio_456VG) REFERENCES Envios_456VG(codenvio_456VG)," +
                    "CONSTRAINT fk_ep_paquete FOREIGN KEY (codpaq_456VG) REFERENCES Paquetes_456VG(codpaq_456VG)" +
                ");");
            dbReal.ejecutarQuery456VG(
                "USE EnviosYA_456VG; CREATE TABLE Facturas_456VG (" +
                    "codfactura_456VG VARCHAR(30) PRIMARY KEY, " +
                    "codenvio_456VG VARCHAR(20) NOT NULL, " +
                    "dni_cli_456VG VARCHAR(20) NOT NULL, " +
                    "fechaemision_456VG DATE NOT NULL, " +
                    "horaemision_456VG TIME NOT NULL, " +
                    "impreso_456VG BIT NOT NULL DEFAULT 0, " +
                    "CONSTRAINT fk_factura_envio FOREIGN KEY (codenvio_456VG) REFERENCES Envios_456VG(codenvio_456VG), " +
                    "CONSTRAINT fk_factura_cliente FOREIGN KEY (dni_cli_456VG) REFERENCES Clientes_456VG(dni_456VG)" +
                ");");
            dbReal.ejecutarQuery456VG(
                "USE EnviosYA_456VG; CREATE TABLE DatosPago_456VG (" +
                    "dni_cliente_456VG VARCHAR(20) PRIMARY KEY, " +
                    "medio_pago_456VG    VARCHAR(50) NOT NULL, " +
                    "numtarjeta_456VG    VARCHAR(20) NOT NULL, " +
                    "titular_456VG       VARCHAR(100) NOT NULL, " +
                    "fechavencimiento_456VG DATE NOT NULL, " +
                    "cvc_456VG           VARCHAR(4) NOT NULL, " +
                    "CONSTRAINT fk_datospago_cliente FOREIGN KEY (dni_cliente_456VG) REFERENCES Clientes_456VG(dni_456VG)" +
                ");");
            dbReal.ejecutarQuery456VG(
                "USE EnviosYA_456VG; CREATE TABLE HistorialContraseñas_456VG (" +
                    "dni_456VG VARCHAR(20) NOT NULL, " +
                    "contraseñahash_456VG VARCHAR(100) NOT NULL, " +
                    "salt_456VG VARCHAR(24) NOT NULL, " +
                    "fechacambio_456VG DATETIME NOT NULL DEFAULT GETDATE(), " +
                    "hashsimple_456VG VARCHAR(100) NOT NULL, " +
                    "PRIMARY KEY (dni_456VG, fechacambio_456VG)" +
                ");");
            dbReal.ejecutarQuery456VG(
                "USE EnviosYA_456VG; " +
                "ALTER TABLE HistorialContraseñas_456VG " +
                "ADD CONSTRAINT FK_HistorialContraseñas_Usuario_456VG " +
                "FOREIGN KEY (dni_456VG) REFERENCES Usuario_456VG(dni_456VG)");
            dbReal.ejecutarQuery456VG(
                "USE EnviosYA_456VG; " +
                "IF OBJECT_ID('dbo.BitacoraEvento_456VG','U') IS NULL " +
                "BEGIN " +
                "    CREATE TABLE dbo.BitacoraEvento_456VG ( " +
                "        CodBitacora_456VG INT IDENTITY(1,1) NOT NULL PRIMARY KEY, " +
                "        dni_456VG        VARCHAR(20) NOT NULL, " +
                "        Fecha_456VG      DATETIME    NOT NULL DEFAULT (GETDATE()), " +
                "        Modulo_456VG     VARCHAR(50) NOT NULL, " +
                "        Evento_456VG     VARCHAR(50) NOT NULL, " +
                "        Criticidad_456VG INT         NOT NULL, " +
                "        CONSTRAINT FK_Bitacora_Usuario_456VG " +
                "            FOREIGN KEY (dni_456VG) REFERENCES dbo.Usuario_456VG(dni_456VG) " +
                "    ); " +
                "END;"
            );

            dbReal.insertarDatosIniciales456VG();
        }
        else
        {
            throw new Exception("No fue posible crear la base de datos EnviosYA_456VG. Verifica permisos o si ya existe.");
        }
    }
    public void insertarDatosIniciales456VG()
    {
        var dbReal = new BasedeDatos_456VG(apuntarAMaster: false);
        dbReal.ejecutarQuery456VG(
            "USE EnviosYA_456VG; " +
            "IF NOT EXISTS(SELECT 1 FROM Rol_456VG WHERE Nombre_456VG = 'Cajero') " +
            "INSERT INTO Rol_456VG (Nombre_456VG) VALUES ('Cajero');" +
            "IF NOT EXISTS(SELECT 1 FROM Rol_456VG WHERE Nombre_456VG = 'Empleado Administrativo') " +
            "INSERT INTO Rol_456VG (Nombre_456VG) VALUES ('Empleado Administrativo');" +
            "IF NOT EXISTS(SELECT 1 FROM Rol_456VG WHERE Nombre_456VG = 'Cargador') " +
            "INSERT INTO Rol_456VG (Nombre_456VG) VALUES ('Cargador');" +
            "IF NOT EXISTS(SELECT 1 FROM Rol_456VG WHERE Nombre_456VG = 'Operador Deposito') " +
            "INSERT INTO Rol_456VG (Nombre_456VG) VALUES ('Operador Deposito');" +
            "IF NOT EXISTS(SELECT 1 FROM Rol_456VG WHERE Nombre_456VG = 'Entregador Envíos') " +
            "INSERT INTO Rol_456VG (Nombre_456VG) VALUES ('Entregador Envíos');" +
            "IF NOT EXISTS(SELECT 1 FROM Rol_456VG WHERE Nombre_456VG = 'Administrador') " +
            "INSERT INTO Rol_456VG (Nombre_456VG) VALUES ('Administrador');"
        );
        dbReal.ejecutarQuery456VG(
            "USE EnviosYA_456VG; " +
            "INSERT INTO Usuario_456VG (" +
                "dni_456VG, nombre_456VG, apellido_456VG, email_456VG, telefono_456VG, " +
                "nombreusuario_456VG, contraseña_456VG, salt_456VG, domicilio_456VG, " +
                "rol_456VG, bloqueado_456VG, activo_456VG, idioma_456VG, CodRol_456VG" +
            ") VALUES " +
            "('45984456', 'Valentin', 'Giraldes', 'valentinGiraldes@gmail.com', '1127118942', " +
            "'Valenurieel', '67784301a1409e30ef093a65c81332fd8590e4f60745a2d8c92c6c95cc60e5db', " +
            "'XwICLo018ug50ej8EVnZng==', 'Jose Martí 1130', 'Administrador', 0, 1, 'ES', " +
            "(SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Administrador'))," +

            "('12345678', 'Rogelio', 'Martinez', 'rogemartinez@gmail.com', '1234567890', " +
            "'Rogelin123', '67784301a1409e30ef093a65c81332fd8590e4f60745a2d8c92c6c95cc60e5db', " +
            "'XwICLo018ug50ej8EVnZng==', 'Martin 2346', 'Empleado Administrativo', 0, 1, 'EN', " +
            "(SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Empleado Administrativo'))," +

            "('26212621', 'Angel', 'Gutierrez', 'angelgutierrez@gmail.com', '1127119824', " +
            "'Angel Guti', '67784301a1409e30ef093a65c81332fd8590e4f60745a2d8c92c6c95cc60e5db', " +
            "'XwICLo018ug50ej8EVnZng==', 'Almirante 2346', 'Operador Deposito', 0, 1, 'ES', " +
            "(SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Operador Deposito'))," +

            "('26222622', 'Miguel', 'Posadas', 'migueposadas@gmail.com', '1127119824', " +
            "'Miguelin', '67784301a1409e30ef093a65c81332fd8590e4f60745a2d8c92c6c95cc60e5db', " +
            "'XwICLo018ug50ej8EVnZng==', 'Calderon 2346', 'Cargador', 0, 1, 'ES', " +
            "(SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Cargador'))," +

            "('26232623', 'Pablo', 'Huerta', 'pablohuerta@gmail.com', '1127119824', " +
            "'Pablin Huertin', '67784301a1409e30ef093a65c81332fd8590e4f60745a2d8c92c6c95cc60e5db', " +
            "'XwICLo018ug50ej8EVnZng==', 'Zuviria 2346', 'Entregador Envíos', 0, 1, 'ES', " +
            "(SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Entregador Envíos'))," +

            "('26202620', 'Venus', 'Ginna', 'venusginna@gmail.com', '1127119824', " +
            "'Chinnelon', '67784301a1409e30ef093a65c81332fd8590e4f60745a2d8c92c6c95cc60e5db', " +
            "'XwICLo018ug50ej8EVnZng==', 'Jose Martinez 1140', 'Cajero', 0, 1, 'ES', " +
            "(SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Cajero'));"
        );
        dbReal.ejecutarQuery456VG(
            "USE EnviosYA_456VG; " +
            "INSERT INTO HistorialContraseñas_456VG (dni_456VG, contraseñahash_456VG, salt_456VG, fechacambio_456VG, hashsimple_456VG) " +
            "VALUES " +
            "('45984456', '3a11feef3ccc351c8c9cad5adebdc26aaada19e32ed68361ab0d4f5aec8ccff2', 'y1/gWmtSuqEGbku6dOjasQ==', '2025-05-21 16:24:08.150', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3')," +
            "('45984456', 'add2edfc88b30590e2db973f0df825a406597f09100d12ed4576af0805947818', 'M7wSHlkRAEPLkLEBuaiFGg==', '2025-05-21 16:22:27.397', '8354ffe30f3c1fde68fdf0723c14aff6db9a1b05f947c4059b8041484de0a6b5')," +
            "('12345678', '67784301a1409e30ef093a65c81332fd8590e4f60745a2d8c92c6c95cc60e5db', 'XwICLo018ug50ej8EVnZng==', '2025-05-21 16:25:14.293', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3')," +
            "('12345678', '5058e0c8ccf88b14e647fd2818f482c68d309ec3eeb6f5d198e475768f172d63', 'X781YT6M92Sw49EnecGZlw==', '2025-05-21 16:23:03.263', '1ccbfab998c38440481442508bcd161f8b90d67e9fca14e48dfaa472324de7ee')," +
            "('26202620', '67784301a1409e30ef093a65c81332fd8590e4f60745a2d8c92c6c95cc60e5db', 'XwICLo018ug50ej8EVnZng==', '2025-05-21 16:25:14.293', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3')," +
            "('26202620', '5058e0c8ccf88b14e647fd2818f482c68d309ec3eeb6f5d198e475768f172d63', 'X781YT6M92Sw49EnecGZlw==', '2025-05-21 16:23:03.263', '1ccbfab998c38440481442508bcd161f8b90d67e9fca14e48dfaa472324de7ee');"
        );
        dbReal.ejecutarQuery456VG(
            "USE EnviosYA_456VG; " +
            "INSERT INTO Clientes_456VG (dni_456VG, nombre_456VG, apellido_456VG, telefono_456VG, domicilio_456VG, fechanacimiento_456VG, activo_456VG) VALUES " +
            "('87654321', 'Lucía', 'Fernández', '1122334455', 'grnz2nXJeEXZzQxF+TQijC+Jpsif9hO8B64LimW7jOk=', '1990-05-15', 1)," +
            "('20262026', 'Marcos', 'Pereyra', '1166778899', 'GQFNqxUb+Ua8rCxDQxtcbOUo9dVwZk5UTn3gNS2X74g=', '1985-08-22', 1);"
        );
        dbReal.ejecutarQuery456VG(
        "USE EnviosYA_456VG; " +
        "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'MenuRecepción') INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('MenuRecepción', 0); " +
        "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'CobrarEnvío') INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('CobrarEnvío', 0); " +
        "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'MenuAyuda') INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('MenuAyuda', 0); " +
        "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'MenuSalir') INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('MenuSalir', 0); " +
        "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'MenuUsuarios') INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('MenuUsuarios', 0); " +
        "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'IniciarSesión') INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('IniciarSesión', 0); " +
        "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'CerrarSesión') INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('CerrarSesión', 0); " +
        "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'CambiarIdioma') INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('CambiarIdioma', 0); " +
        "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'CambiarContraseña') INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('CambiarContraseña', 0); " +
        "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'MenuReportes') INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('MenuReportes', 0); " +
        "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'FacturasIMP') INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('FacturasIMP', 0); " +
        "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'MenuEnvíos') INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('MenuEnvíos', 0); " +
        "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'CrearEnvío') INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('CrearEnvío', 0); " +
        "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'MenuMaestro') INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('MenuMaestro', 0); " +
        "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'GestióndeClientes') INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('GestióndeClientes', 0); " +
        "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'MenuAdministrador') INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('MenuAdministrador', 0); " +
        "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'GestióndeUsuarios') INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('GestióndeUsuarios', 0); " +
        "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'GestióndePerfiles') INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('GestióndePerfiles', 0); " +
        "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'SeguimientoEnvío') INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('SeguimientoEnvío', 0); " +
        "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'BitacoraEventos') INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('BitacoraEventos', 0); " +
        "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'BackupRestore') INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('BackupRestore', 0); " +
        "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'GestióndeTransportes') INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('GestióndeTransportes', 0); " +
        "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'GestióndeChoferes') INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('GestióndeChoferes', 0); " +
        "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'AsignacióndeEnvío') INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('AsignacióndeEnvío', 0); " +
        "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'ListaCarga') INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('ListaCarga', 0); " +
        "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'EntregadeEnvío') INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('EntregadeEnvío', 0);"
        );
        dbReal.ejecutarQuery456VG(
            "USE EnviosYA_456VG; " +
            "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'Seguridad') " +
            "INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('Seguridad', 1); " +
            "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'Cobranza') " +
            "INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('Cobranza', 1); " +
            "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'Recepciones') " +
            "INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('Recepciones', 1);" +
            "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'EntregaEnvíos') " +
            "INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('EntregaEnvíos', 1);" +
            "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'Cargador') " +
            "INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('Cargador', 1);" +
            "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'OperadorDeposito') " +
            "INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('OperadorDeposito', 1);" +
            "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'FAdmin') " +
            "INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('FAdmin', 1);" +
            "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'Menu Usuario') " +
            "INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('Menu Usuario', 1);" +
            "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'Menu Salir') " +
            "INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('Menu Salir', 1);" +
            "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'Menu Ayuda') " +
            "INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('Menu Ayuda', 1);" +
            "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'Menu Admin') " +
            "INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('Menu Admin', 1);" +
            "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'Menu Recepcion') " +
            "INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('Menu Recepcion', 1);" +
            "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'Menu Maestros') " +
            "INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('Menu Maestros', 1);" +
            "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'Menu Envíos') " +
            "INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('Menu Envíos', 1);" +
            "IF NOT EXISTS (SELECT 1 FROM Permiso_456VG WHERE Nombre_456VG = 'Menu Reportes') " +
            "INSERT INTO Permiso_456VG (Nombre_456VG, IsFamilia_456VG) VALUES ('Menu Reportes', 1);"
        );
        dbReal.ejecutarQuery456VG(
            "USE EnviosYA_456VG; " +
            "DECLARE @seg INT = (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Seguridad'); " +
            "DECLARE @cob INT = (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Cobranza'); " +
            "DECLARE @rec INT = (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Recepciones'); " +
            "DECLARE @fadm INT = (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Fadmin'); " +
            "DECLARE @muser INT = (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Menu Usuario'); " +
            "DECLARE @msalir INT = (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Menu Salir'); " +
            "DECLARE @mayuda INT = (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Menu Ayuda'); " +
            "DECLARE @madmin INT = (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Menu Admin'); " +
            "DECLARE @mrecep INT = (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Menu Recepcion'); " +
            "DECLARE @mmaest INT = (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Menu Maestros'); " +
            "DECLARE @menv INT = (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Menu Envíos'); " +
            "DECLARE @mrepor INT = (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Menu Reportes'); " +
            "DECLARE @eenv INT = (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'EntregaEnvíos'); " +
            "DECLARE @opd INT = (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'OperadorDeposito'); " +
            "DECLARE @carenv INT = (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Cargador'); " +
            //Menu Usuarios
            "INSERT INTO FamiliaPermiso_456VG (CodFamilia_456VG, CodPermiso_456VG) " +
            "SELECT @muser, CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG IN ( " +
            "'MenuUsuarios', " +
            "'IniciarSesión', " +
            "'CerrarSesión', " +
            "'CambiarIdioma', " +
            "'CambiarContraseña');" +
            //Menu Salir
            "INSERT INTO FamiliaPermiso_456VG (CodFamilia_456VG, CodPermiso_456VG) " +
            "SELECT @msalir, CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG IN ( " +
            "'MenuSalir');" +
            //Menu Ayuda
            "INSERT INTO FamiliaPermiso_456VG (CodFamilia_456VG, CodPermiso_456VG) " +
            "SELECT @mayuda, CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG IN ( " +
            "'MenuAyuda');" +
            //Menu Admin
            "INSERT INTO FamiliaPermiso_456VG (CodFamilia_456VG, CodPermiso_456VG) " +
            "SELECT @madmin, CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG IN ( " +
            "'MenuAdministrador', " +
            "'GestióndeUsuarios', " +
            "'BitacoraEventos', " +
            "'BackupRestore', " +
            "'GestióndePerfiles');" +
            //Menu Recepcion
            "INSERT INTO FamiliaPermiso_456VG (CodFamilia_456VG, CodPermiso_456VG) " +
            "SELECT @mrecep, CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG IN ( " +
            "'MenuRecepción', " +
            "'CrearEnvío');" +
            //Menu Maestros
            "INSERT INTO FamiliaPermiso_456VG (CodFamilia_456VG, CodPermiso_456VG) " +
            "SELECT @mmaest, CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG IN ( " +
            "'MenuMaestro', " +
            "'GestióndeTransportes', " +
            "'GestióndeChoferes', " +
            "'GestióndeClientes');" +
            //Menu Envíos
            "INSERT INTO FamiliaPermiso_456VG (CodFamilia_456VG, CodPermiso_456VG) " +
            "SELECT @menv, CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG IN ( " +
            "'MenuEnvíos', " +
            "'CobrarEnvío');" +
            //Menu Reportes
            "INSERT INTO FamiliaPermiso_456VG (CodFamilia_456VG, CodPermiso_456VG) " +
            "SELECT @mrepor, CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG IN ( " +
            "'MenuReportes', " +
            "'FacturasIMP', " +
            "'SeguimientoEnvío');" +
            //FSeguridad
            "INSERT INTO FamiliaPermiso_456VG (CodFamilia_456VG, CodPermiso_456VG) " +
            "SELECT @seg, CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG IN ( " +
            "'Menu Usuario', " +
            "'Menu Ayuda', " +
            "'Menu Salir'); " +
            //FCobranza
            "INSERT INTO FamiliaPermiso_456VG (CodFamilia_456VG, CodPermiso_456VG) " +
            "SELECT @cob, CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG IN ( " +
            "'Menu Envíos', " +
            "'Menu Reportes'); " +
            //FRecepciones
            "INSERT INTO FamiliaPermiso_456VG (CodFamilia_456VG, CodPermiso_456VG) " +
            "SELECT @rec, CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG IN ( " +
            "'Menu Recepcion', " +
            "'Menu Maestros');" +
            //RFN2 ----------------------------------------
            //FEntregaEnvios   ---------- Asignar FLIA a USUARIO.
            "INSERT INTO FamiliaPermiso_456VG (CodFamilia_456VG, CodPermiso_456VG) " +
            "SELECT @eenv, CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG IN ( " +
            "'MenuEnvíos', " +
            "'EntregadeEnvío');" +
            //FOperadorDeposito   ---------- Asignar FLIA a USUARIO.
            "INSERT INTO FamiliaPermiso_456VG (CodFamilia_456VG, CodPermiso_456VG) " +
            "SELECT @opd, CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG IN ( " +
            "'AsignacióndeEnvío', " +
            "'MenuRecepción');" +
            //FCargador   ---------- Asignar FLIA a USUARIO.
            "INSERT INTO FamiliaPermiso_456VG (CodFamilia_456VG, CodPermiso_456VG) " +
            "SELECT @carenv, CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG IN ( " +
            "'ListaCarga', " +
            "'MenuReportes');" +
            //FAdmin
            "INSERT INTO FamiliaPermiso_456VG (CodFamilia_456VG, CodPermiso_456VG) " +
            "SELECT @fadm, CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG IN ( " +
            "'Menu Admin');"
        );
        dbReal.ejecutarQuery456VG(
            "USE EnviosYA_456VG; " +
            //User Cajero
            "IF NOT EXISTS (SELECT 1 FROM Rol_Permiso_456VG WHERE CodRol_456VG = (SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Cajero') AND CodPermiso_456VG = (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Seguridad')) " +
            "INSERT INTO Rol_Permiso_456VG VALUES ((SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Cajero'), (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Seguridad')); " +
            "IF NOT EXISTS (SELECT 1 FROM Rol_Permiso_456VG WHERE CodRol_456VG = (SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Cajero') AND CodPermiso_456VG = (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Cobranza')) " +
            "INSERT INTO Rol_Permiso_456VG VALUES ((SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Cajero'), (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Cobranza')); " +
            //User Emp Administrativo
            "IF NOT EXISTS (SELECT 1 FROM Rol_Permiso_456VG WHERE CodRol_456VG = (SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Empleado Administrativo') AND CodPermiso_456VG = (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Seguridad')) " +
            "INSERT INTO Rol_Permiso_456VG VALUES ((SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Empleado Administrativo'), (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Seguridad')); " +
            "IF NOT EXISTS (SELECT 1 FROM Rol_Permiso_456VG WHERE CodRol_456VG = (SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Empleado Administrativo') AND CodPermiso_456VG = (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Recepciones')) " +
            "INSERT INTO Rol_Permiso_456VG VALUES ((SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Empleado Administrativo'), (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Recepciones')); " +
            //-- User Cargador
            "IF NOT EXISTS (SELECT 1 FROM Rol_Permiso_456VG WHERE CodRol_456VG = (SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Cargador') AND CodPermiso_456VG = (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Seguridad')) " +
            "INSERT INTO Rol_Permiso_456VG VALUES ((SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Cargador'), (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Seguridad')); " +
            "IF NOT EXISTS (SELECT 1 FROM Rol_Permiso_456VG WHERE CodRol_456VG = (SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Cargador') AND CodPermiso_456VG = (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Cargador')) " +
            "INSERT INTO Rol_Permiso_456VG VALUES ((SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Cargador'), (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Cargador')); " +
            //-- User Operador Deposito
            "IF NOT EXISTS (SELECT 1 FROM Rol_Permiso_456VG WHERE CodRol_456VG = (SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Operador Deposito') AND CodPermiso_456VG = (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Seguridad')) " +
            "INSERT INTO Rol_Permiso_456VG VALUES ((SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Operador Deposito'), (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Seguridad')); " +
            "IF NOT EXISTS (SELECT 1 FROM Rol_Permiso_456VG WHERE CodRol_456VG = (SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Operador Deposito') AND CodPermiso_456VG = (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'OperadorDeposito')) " +
            "INSERT INTO Rol_Permiso_456VG VALUES ((SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Operador Deposito'), (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'OperadorDeposito')); " +
            //-- User Entregador Envíos
            "IF NOT EXISTS (SELECT 1 FROM Rol_Permiso_456VG WHERE CodRol_456VG = (SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Entregador Envíos') AND CodPermiso_456VG = (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Seguridad')) " +
            "INSERT INTO Rol_Permiso_456VG VALUES ((SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Entregador Envíos'), (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Seguridad')); " +
            "IF NOT EXISTS (SELECT 1 FROM Rol_Permiso_456VG WHERE CodRol_456VG = (SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Entregador Envíos') AND CodPermiso_456VG = (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'EntregaEnvíos')) " +
            "INSERT INTO Rol_Permiso_456VG VALUES ((SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Entregador Envíos'), (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'EntregaEnvíos')); " +
            //User Admin
            "IF NOT EXISTS (SELECT 1 FROM Rol_Permiso_456VG WHERE CodRol_456VG = (SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Administrador') AND CodPermiso_456VG = (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Seguridad')) " +
            "INSERT INTO Rol_Permiso_456VG VALUES ((SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Administrador'), (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Seguridad')); " +
            "IF NOT EXISTS (SELECT 1 FROM Rol_Permiso_456VG WHERE CodRol_456VG = (SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Administrador') AND CodPermiso_456VG = (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Cobranza')) " +
            "INSERT INTO Rol_Permiso_456VG VALUES ((SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Administrador'), (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Cobranza')); " +
            "IF NOT EXISTS (SELECT 1 FROM Rol_Permiso_456VG WHERE CodRol_456VG = (SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Administrador') AND CodPermiso_456VG = (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Recepciones')) " +
            "INSERT INTO Rol_Permiso_456VG VALUES ((SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Administrador'), (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Recepciones')); " +
            "IF NOT EXISTS (SELECT 1 FROM Rol_Permiso_456VG WHERE CodRol_456VG = (SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Administrador') AND CodPermiso_456VG = (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'EntregaEnvíos')) " +
            "INSERT INTO Rol_Permiso_456VG VALUES ((SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Administrador'), (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'EntregaEnvíos')); " +
            "IF NOT EXISTS (SELECT 1 FROM Rol_Permiso_456VG WHERE CodRol_456VG = (SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Administrador') AND CodPermiso_456VG = (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'OperadorDeposito')) " +
            "INSERT INTO Rol_Permiso_456VG VALUES ((SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Administrador'), (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'OperadorDeposito')); " +
            "IF NOT EXISTS (SELECT 1 FROM Rol_Permiso_456VG WHERE CodRol_456VG = (SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Administrador') AND CodPermiso_456VG = (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Cargador')) " +
            "INSERT INTO Rol_Permiso_456VG VALUES ((SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Administrador'), (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'Cargador')); " +
            "IF NOT EXISTS (SELECT 1 FROM Rol_Permiso_456VG WHERE CodRol_456VG = (SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Administrador') AND CodPermiso_456VG = (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'FAdmin')) " +
            "INSERT INTO Rol_Permiso_456VG VALUES ((SELECT CodRol_456VG FROM Rol_456VG WHERE Nombre_456VG = 'Administrador'), (SELECT CodPermiso_456VG FROM Permiso_456VG WHERE Nombre_456VG = 'FAdmin'));"
        );
    }
}
