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
                    "rol_456VG VARCHAR(100) NOT NULL," +
                    "bloqueado_456VG BIT NOT NULL DEFAULT 0," +
                    "activo_456VG BIT NOT NULL DEFAULT 1," +
                    "idioma_456VG VARCHAR(50) NOT NULL DEFAULT 'ES'" +
                ");");
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
            // 1) Tabla de componentes/permisos
            dbReal.ejecutarQuery456VG(@"
                USE EnviosYA_456VG;
                IF OBJECT_ID('dbo.PermisosComp_456VG', 'U') IS NULL
                CREATE TABLE PermisosComp_456VG (
                    id_permiso_456VG           INT IDENTITY(1,1) PRIMARY KEY,
                    nombre_456VG               NVARCHAR(100) NOT NULL,
                    nombre_formulario_456VG    NVARCHAR(100) NULL,
                    isPerfil_456VG             BIT NOT NULL DEFAULT 0
                );
                ");
            // 2) Tabla de relación padre→hijo (jerarquía)
            dbReal.ejecutarQuery456VG(@"
                USE EnviosYA_456VG;
                IF OBJECT_ID('dbo.PermisoPermiso_456VG','U') IS NULL
                CREATE TABLE PermisoPermiso_456VG (
                    id_permisopadre_456VG INT NOT NULL,
                    id_permisohijo_456VG  INT NOT NULL,
                    CONSTRAINT PK_PermisoPermiso_456VG PRIMARY KEY(id_permisopadre_456VG, id_permisohijo_456VG),
                    -- Cascada sólo cuando borres el padre
                    CONSTRAINT FK_PP_Padre_456VG FOREIGN KEY(id_permisopadre_456VG)
                        REFERENCES PermisosComp_456VG(id_permiso_456VG) ON DELETE CASCADE,
                    -- Al borrar el hijo, NO cascades (se manejará manualmente si hace falta)
                    CONSTRAINT FK_PP_Hijo_456VG FOREIGN KEY(id_permisohijo_456VG)
                        REFERENCES PermisosComp_456VG(id_permiso_456VG) ON DELETE NO ACTION
                );
        ");
            // 3) Tabla de asignación usuario→permiso
            dbReal.ejecutarQuery456VG(@"
                USE EnviosYA_456VG;
                IF OBJECT_ID('dbo.UsuarioPermiso_456VG', 'U') IS NULL
                CREATE TABLE UsuarioPermiso_456VG (
                    dni_456VG            VARCHAR(20) NOT NULL,
                    id_permiso_456VG     INT         NOT NULL,
                    CONSTRAINT PK_UsuarioPermiso_456VG PRIMARY KEY(dni_456VG, id_permiso_456VG),
                    CONSTRAINT FK_UP_Usuario_456VG FOREIGN KEY(dni_456VG)
                        REFERENCES Usuario_456VG(dni_456VG) ON DELETE CASCADE,
                    CONSTRAINT FK_UP_Permiso_456VG FOREIGN KEY(id_permiso_456VG)
                        REFERENCES PermisosComp_456VG(id_permiso_456VG) ON DELETE CASCADE
                );
                IF NOT EXISTS (
                    SELECT 1 FROM sys.indexes
                    WHERE name = 'IDX_UsuarioPermiso_Permiso_456VG'
                      AND object_id = OBJECT_ID('dbo.UsuarioPermiso_456VG')
                )
                CREATE INDEX IDX_UsuarioPermiso_Permiso_456VG
                    ON UsuarioPermiso_456VG(id_permiso_456VG);
            ");
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
            "INSERT INTO Usuario_456VG (dni_456VG, nombre_456VG, apellido_456VG, email_456VG, telefono_456VG, nombreusuario_456VG, contraseña_456VG, salt_456VG, domicilio_456VG, rol_456VG, bloqueado_456VG, activo_456VG, idioma_456VG) " +
            "VALUES " +
            "('45984456', 'Valentin', 'Giraldes', 'valentinGiraldes@gmail.com', '1127118942', 'Valenurieel', '3a11feef3ccc351c8c9cad5adebdc26aaada19e32ed68361ab0d4f5aec8ccff2', 'y1/gWmtSuqEGbku6dOjasQ==', 'Jose Martí 1130', 'Administrador', 0, 1, 'ES')," +
            "('12345678', 'Rogelio', 'Martinez', 'rogemartinez@gmail.com', '1234567890', 'Rogelin123', '67784301a1409e30ef093a65c81332fd8590e4f60745a2d8c92c6c95cc60e5db', 'XwICLo018ug50ej8EVnZng==', 'Martin 2346', 'Empleado Administrativo', 0, 1, 'EN')," +
            "('26202620', 'Venus', 'Ginna', 'venusginna@gmail.com', '1127119824', 'Chinnelon', '67784301a1409e30ef093a65c81332fd8590e4f60745a2d8c92c6c95cc60e5db', 'XwICLo018ug50ej8EVnZng==', 'Jose Martinez 1140', 'Cajero', 0, 1, 'ES');"
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
            "('987654321', 'Lucía', 'Fernández', '1122334455', 'grnz2nXJeEXZzQxF+TQijC+Jpsif9hO8B64LimW7jOk=', '1990-05-15', 1)," +
            "('262026202', 'Marcos', 'Pereyra', '1166778899', 'GQFNqxUb+Ua8rCxDQxtcbOUo9dVwZk5UTn3gNS2X74g=', '1985-08-22', 1);"
        );
        dbReal.ejecutarQuery456VG(@"
                USE EnviosYA_456VG;
                -- Perfiles
                IF NOT EXISTS(SELECT 1 FROM PermisosComp_456VG WHERE nombre_456VG='Cajero'                   AND isPerfil_456VG=1)
                    INSERT INTO PermisosComp_456VG(nombre_456VG,nombre_formulario_456VG,isPerfil_456VG)
                    VALUES('Cajero','usuarioToolStripMenuItem456VG',1);

                IF NOT EXISTS(SELECT 1 FROM PermisosComp_456VG WHERE nombre_456VG='Empleado Administrativo' AND isPerfil_456VG=1)
                    INSERT INTO PermisosComp_456VG(nombre_456VG,nombre_formulario_456VG,isPerfil_456VG)
                    VALUES('Empleado Administrativo','usuarioToolStripMenuItem456VG',1);

                IF NOT EXISTS(SELECT 1 FROM PermisosComp_456VG WHERE nombre_456VG='Administrador'                   AND isPerfil_456VG=1)
                    INSERT INTO PermisosComp_456VG(nombre_456VG,nombre_formulario_456VG,isPerfil_456VG)
                    VALUES('Administrador','usuarioToolStripMenuItem456VG',1);

                -- Permisos generales
                DECLARE @lista TABLE(nombre NVARCHAR(100), formulario NVARCHAR(100));
                INSERT INTO @lista VALUES
                    ('Recepción','recepcionToolStripMenuItem'),
                    ('Cobrar Envío','cobrarEnvíoToolStripMenuItem'),
                    ('Ayuda','ayudaToolStripMenuItem456VG'),
                    ('Salir','salirToolStripMenuItem456VG'),
                    ('Usuarios','usuarioToolStripMenuItem456VG'),
                    ('Iniciar Sesión','iniciarSesiónToolStripMenuItem456VG'),
                    ('Cerrar Sesión','cerrarSesiónToolStripMenuItem456VG'),
                    ('Cambiar Idioma','cambiarIdiomaToolStripMenuItem456VG'),
                    ('Cambiar Contraseña','cambiarClaveToolStripMenuItem456VG'),
                    ('Reportes','reportesToolStripMenuItem456VG'),
                    ('Facturas - IMP','facturasIMPToolStripMenuItem456VG'),
                    ('Envíos','envíosToolStripMenuItem456VG'),
                    ('Crear Envío','crearenvíoToolStripMenuItem456VG'),
                    ('Maestro','maestroToolStripMenuItem456VG'),
                    ('Gestión de Clientes','clientesToolStripMenuItem456VG'),
                    ('Administrador','administradorToolStripMenuItem456VG'),
                    ('Gestión de Usuarios','usuariosToolStripMenuItem456VG'),
                    ('Gestión de Perfiles','perfilesToolStripMenuItem456VG');

                INSERT INTO PermisosComp_456VG(nombre_456VG,nombre_formulario_456VG,isPerfil_456VG)
                SELECT l.nombre, l.formulario, 0
                FROM @lista l
                WHERE NOT EXISTS(
                    SELECT 1 FROM PermisosComp_456VG pc
                     WHERE pc.nombre_456VG = l.nombre
                       AND pc.nombre_formulario_456VG = l.formulario
                );
            ");
        dbReal.ejecutarQuery456VG(@"
                USE EnviosYA_456VG;
                DECLARE @rel TABLE(padre INT, hijo INT);
                INSERT INTO @rel VALUES
                    (1,4),(1,5),(1,6),(1,7),(1,8),(1,9),(1,10),(1,11),(1,12),(1,13),(1,14),
                    (2,4),(2,6),(2,7),(2,8),(2,9),(2,10),(2,11),(2,12),(2,15),(2,16),(2,17),(2,18),
                    (3,3),(3,4),(3,5),(3,6),(3,7),(3,8),(3,9),(3,10),(3,11),(3,12),(3,13),(3,14),
                    (3,15),(3,16),(3,17),(3,18),(3,19),(3,20),(3,21);

                INSERT INTO PermisoPermiso_456VG(id_permisopadre_456VG,id_permisohijo_456VG)
                SELECT r.padre, r.hijo
                FROM @rel r
                WHERE NOT EXISTS(
                    SELECT 1 FROM PermisoPermiso_456VG pp
                     WHERE pp.id_permisopadre_456VG = r.padre
                       AND pp.id_permisohijo_456VG  = r.hijo
                );
            ");
                dbReal.ejecutarQuery456VG(
                  "USE EnviosYA_456VG; " +
                  "INSERT INTO UsuarioPermiso_456VG (dni_456VG, id_permiso_456VG) VALUES " +
                    "('45984456', 3), " +
                    "('12345678', 2), " +
                    "('26202620', 1);"
                );
                dbReal.ejecutarQuery456VG(@"
        USE EnviosYA_456VG;

        /* 1) Insertar las 3 familias y capturar sus IDs */
        DECLARE @idSeguridad   INT, @idCobranza INT, @idRecepciones INT;

        INSERT INTO PermisosComp_456VG(nombre_456VG, nombre_formulario_456VG, isPerfil_456VG)
        VALUES
          ('Seguridad',    NULL, 0),
          ('Cobranza',     NULL, 0),
          ('Recepciones',  NULL, 0);

        -- Ahora obtenemos los IDs recién generados (suponiendo que no hay más inserciones concurrentes)
        SELECT 
          @idSeguridad   = MAX(CASE WHEN nombre_456VG = 'Seguridad'   THEN id_permiso_456VG END),
          @idCobranza    = MAX(CASE WHEN nombre_456VG = 'Cobranza'    THEN id_permiso_456VG END),
          @idRecepciones = MAX(CASE WHEN nombre_456VG = 'Recepciones' THEN id_permiso_456VG END)
        FROM PermisosComp_456VG
        WHERE isPerfil_456VG = 0;

        /* 2) Asociar permisos a cada familia */
        INSERT INTO PermisoPermiso_456VG(id_permisopadre_456VG, id_permisohijo_456VG)
        VALUES
          /* Seguridad: 6,7,8,9,10,11,12 */
          (@idSeguridad,  6),(@idSeguridad,  7),(@idSeguridad,  8),
          (@idSeguridad,  9),(@idSeguridad, 10),(@idSeguridad, 11),
          (@idSeguridad, 12),

          /* Cobranza: 4,5 */
          (@idCobranza,   4),(@idCobranza,   5),

          /* Recepciones: 4,16,17,18,15,14,13 */
          (@idRecepciones, 4),(@idRecepciones,16),(@idRecepciones,17),
          (@idRecepciones,18),(@idRecepciones,15),(@idRecepciones,14),
          (@idRecepciones,13);
        ");
    }
}
