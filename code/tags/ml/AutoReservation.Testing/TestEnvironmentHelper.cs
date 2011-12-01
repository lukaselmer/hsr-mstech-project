using System.Data.SqlClient;

namespace AutoReservation.Testing
{
    public static class TestEnvironmentHelper
    {
        private const string ConnectionString = "Data Source=.;Initial Catalog=AutoReservation;Integrated Security=True";

        private static void InsertKunden()
        {
            using (SqlConnection myConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = myConnection;

                // Open the connection.
                myConnection.Open();

                myCommand.CommandText = "INSERT INTO Kunde (Nachname, Vorname, Geburtsdatum) VALUES ('Nass', 'Anna', '05/05/1961')";
                myCommand.ExecuteNonQuery();

                myCommand.CommandText = "INSERT INTO Kunde (Nachname, Vorname, Geburtsdatum) VALUES ('Beil', 'Timo', '09/09/1980')";
                myCommand.ExecuteNonQuery();

                myCommand.CommandText = "INSERT INTO Kunde (Nachname, Vorname, Geburtsdatum) VALUES ('Pfahl', 'Martha', '07/03/1950')";
                myCommand.ExecuteNonQuery();

                myCommand.CommandText = "INSERT INTO Kunde (Nachname, Vorname, Geburtsdatum) VALUES ('Zufall', 'Rainer', '11/11/1944')";
                myCommand.ExecuteNonQuery();
            }
        }

        private static void InsertAutos()
        {
            using (SqlConnection myConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = myConnection;

                // Open the connection.
                myConnection.Open();

                myCommand.CommandText = "INSERT INTO Auto (Marke, AutoKlasse, Tagestarif, Basistarif) " +
                                        " VALUES ('Fiat Punto', 2, 50, 0)";
                myCommand.ExecuteNonQuery();

                myCommand.CommandText = "INSERT INTO Auto (Marke, AutoKlasse, Tagestarif, Basistarif) " +
                                        " VALUES ('VW Golf', 1, 120, 0)";
                myCommand.ExecuteNonQuery();

                myCommand.CommandText = "INSERT INTO Auto (Marke, AutoKlasse, Tagestarif, Basistarif) " +
                                        " VALUES ('Audi S6', 0, 180, 50)";
                myCommand.ExecuteNonQuery();
            }
        }

        private static void InsertReservationen()
        {
            using (SqlConnection myConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = myConnection;

                // Open the connection.
                myConnection.Open();

                myCommand.CommandText = "INSERT INTO Reservation (AutoId, KundeId, Von, Bis)  " +
                                        "   SELECT  a.Id  , k.Id, '1/10/2020', '1/20/2020' " +
                                        "   FROM    Auto a, Kunde k " +
                                        "   WHERE   a.Marke = 'Audi S6' AND k.Nachname = 'Nass'";
                myCommand.ExecuteNonQuery();
            }
        }

        private static void DeleteAll()
        {
            using (SqlConnection myConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = myConnection;

                // Open the connection.
                myConnection.Open();

                myCommand.CommandText = "DELETE FROM Reservation";
                myCommand.ExecuteNonQuery();

                myCommand.CommandText = "DELETE FROM Auto";
                myCommand.ExecuteNonQuery();

                myCommand.CommandText = "DELETE FROM Kunde";
                myCommand.ExecuteNonQuery();
            }
        }

        public static void InitializeTestData()
        {
            DeleteAll();
            InsertAutos();
            InsertKunden();
            InsertReservationen();
        }
    }
}
