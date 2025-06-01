using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Markup;

namespace _456VG_DAL
{
    public class BasedeDatos_456VG
    {
        public static string dataSource = "DESKTOP-Q714KGU\\SQLEXPRESS";
        public static string dbName = "EnviosYA_456VG";
        public static string conexionMaster = $"Data source={dataSource};Initial Catalog=master;Integrated Security=True;";
        public SqlConnection Connection = new SqlConnection(conexionMaster);
        public SqlCommand Command = new SqlCommand();
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
            catch
            {
                return false;
            }
            finally
            {
                Desconectar456VG();
            }
        }
        public void insertarDatosIniciales456VG()
        {
            ejecutarQuery456VG("USE EnviosYA_456VG; " +
                "INSERT INTO Usuario_456VG (dni_456VG, nombre_456VG, apellido_456VG, email_456VG, telefono_456VG, nombreusuario_456VG, contraseña_456VG, salt_456VG, domicilio_456VG, rol_456VG, bloqueado_456VG, activo_456VG, idioma_456VG) " +
                "VALUES " +
                "('45984456', 'Valentin', 'Giraldes', 'valentingiraldes@gmail.com', '1127118942', 'Valenurieel', '3a11feef3ccc351c8c9cad5adebdc26aaada19e32ed68361ab0d4f5aec8ccff2', 'y1/gWmtSuqEGbku6dOjasQ==', 'Jose Martí 1130', 'Administrador', 0, 1, 'ES')," +
                "('12345678', 'Rogelio', 'Martinez', 'rogemartinez@gmail.com', '1234567890', 'Rogelin123', '67784301a1409e30ef093a65c81332fd8590e4f60745a2d8c92c6c95cc60e5db', 'XwICLo018ug50ej8EVnZng==', 'Martin 2346', 'Empleado Administrativo', 0, 1, 'ES');");
            ejecutarQuery456VG("USE EnviosYA_456VG; " +
                "INSERT INTO HistorialContraseñas_456VG (dni_456VG, contraseñahash_456VG, salt_456VG, fechacambio_456VG, hashsimple_456VG) " +
                "VALUES " +
                "('45984456', '3a11feef3ccc351c8c9cad5adebdc26aaada19e32ed68361ab0d4f5aec8ccff2', 'y1/gWmtSuqEGbku6dOjasQ==', '2025-05-21 16:24:08.150', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3')," +
                "('45984456', 'add2edfc88b30590e2db973f0df825a406597f09100d12ed4576af0805947818', 'M7wSHlkRAEPLkLEBuaiFGg==', '2025-05-21 16:22:27.397', '8354ffe30f3c1fde68fdf0723c14aff6db9a1b05f947c4059b8041484de0a6b5')," +
                "('12345678', '67784301a1409e30ef093a65c81332fd8590e4f60745a2d8c92c6c95cc60e5db', 'XwICLo018ug50ej8EVnZng==', '2025-05-21 16:25:14.293', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3')," +
                "('12345678', '5058e0c8ccf88b14e647fd2818f482c68d309ec3eeb6f5d198e475768f172d63', 'X781YT6M92Sw49EnecGZlw==', '2025-05-21 16:23:03.263', '1ccbfab998c38440481442508bcd161f8b90d67e9fca14e48dfaa472324de7ee');");
            ejecutarQuery456VG("USE EnviosYA_456VG; " +
                "INSERT INTO Clientes_456VG (dni_456VG, nombre_456VG, apellido_456VG, telefono_456VG, domicilio_456VG, fechanacimiento_456VG, activo_456VG) VALUES " +
                "('987654321', 'Lucía', 'Fernández', '1122334455', 'Av. Rivadavia 1234', '1990-05-15', 1)," +
                "('262026202', 'Marcos', 'Pereyra', '1166778899', 'Calle Falsa 123', '1985-08-22', 1);");
        }
        public void scriptInicio456VG()
        {
            bool bdCreada = ejecutarQuery456VG("CREATE DATABASE EnviosYA_456VG;");
            if (bdCreada)
            {
                ejecutarQuery456VG("USE EnviosYA_456VG; CREATE TABLE Usuario_456VG (" +
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
                ejecutarQuery456VG("USE EnviosYA_456VG; CREATE TABLE Clientes_456VG (" +
                    "dni_456VG VARCHAR(20) PRIMARY KEY," +
                    "nombre_456VG VARCHAR(100) NOT NULL," +
                    "apellido_456VG VARCHAR(100) NOT NULL," +
                    "telefono_456VG VARCHAR(100) NOT NULL," +
                    "domicilio_456VG VARCHAR(100) NOT NULL," +
                    "fechanacimiento_456VG DATE NOT NULL," +
                    "activo_456VG BIT NOT NULL DEFAULT 1" +
                ");");
                ejecutarQuery456VG("USE EnviosYA_456VG; CREATE TABLE Paquetes_456VG (" +
                    "id_paquete_456VG INT IDENTITY(1,1) PRIMARY KEY," +
                    "dni_456VG VARCHAR(20) NOT NULL," +
                    "peso_456VG FLOAT NOT NULL," +
                    "ancho_456VG FLOAT NOT NULL," +
                    "alto_456VG FLOAT NOT NULL," +
                    "largo_456VG FLOAT NOT NULL," +
                    "enviado_456VG BIT NOT NULL DEFAULT 0," +
                    "codpaq_456VG VARCHAR(20) NOT NULL," +
                    "CONSTRAINT fk_paquete_cliente FOREIGN KEY (dni_456VG) REFERENCES Clientes_456VG(dni_456VG)" +
                ");");
                ejecutarQuery456VG("USE EnviosYA_456VG; CREATE TABLE Envios_456VG (" +
                    "id_envio_456VG INT IDENTITY(1,1) PRIMARY KEY," +
                    "id_paquete_456VG INT NOT NULL," +
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
                    "CONSTRAINT fk_envio_cliente FOREIGN KEY (dni_cli_456VG) REFERENCES Clientes_456VG(dni_456VG)," +
                    "CONSTRAINT fk_envio_paquete FOREIGN KEY (id_paquete_456VG) REFERENCES Paquetes_456VG(id_paquete_456VG)" +
                ");");
                ejecutarQuery456VG(
                    "USE EnviosYA_456VG; CREATE TABLE Facturas_456VG (" +
                        "id_factura_456VG      INT IDENTITY(1,1) PRIMARY KEY, " +
                        "id_envio_456VG        INT NOT NULL, " +
                        "id_paquete_456VG      INT NOT NULL, " +
                        "dni_cli_456VG         VARCHAR(20) NOT NULL, " +
                        "fechaemision_456VG    DATETIME NOT NULL DEFAULT GETDATE(), " +
                        "CONSTRAINT fk_factura_envio    FOREIGN KEY (id_envio_456VG)   REFERENCES Envios_456VG(id_envio_456VG), " +
                        "CONSTRAINT fk_factura_paquete  FOREIGN KEY (id_paquete_456VG) REFERENCES Paquetes_456VG(id_paquete_456VG), " +
                        "CONSTRAINT fk_factura_cliente  FOREIGN KEY (dni_cli_456VG)    REFERENCES Clientes_456VG(dni_456VG)" +
                    ");"
                );
                ejecutarQuery456VG("USE EnviosYA_456VG; CREATE TABLE HistorialContraseñas_456VG (" +
                    "dni_456VG VARCHAR(20) NOT NULL, " +
                    "contraseñahash_456VG VARCHAR(100) NOT NULL, " +
                    "salt_456VG VARCHAR(24) NOT NULL, " +
                    "fechacambio_456VG DATETIME NOT NULL DEFAULT GETDATE(), " +
                    "hashsimple_456VG VARCHAR(100) NOT NULL, " +
                    "PRIMARY KEY (dni_456VG, fechacambio_456VG) " + 
                ");");
                ejecutarQuery456VG("USE EnviosYA_456VG; " +
                    "ALTER TABLE HistorialContraseñas_456VG " +
                    "ADD CONSTRAINT FK_HistorialContraseñas_Usuario_456VG " +
                    "FOREIGN KEY (dni_456VG) REFERENCES Usuario_456VG(dni_456VG)");
                insertarDatosIniciales456VG();
            }
        }
    }
}
