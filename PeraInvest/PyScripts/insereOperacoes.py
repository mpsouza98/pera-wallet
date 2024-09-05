import mysql.connector
from mysql.connector import Error
import uuid
from random import uniform
from datetime import datetime, timedelta

def generate_binary_uuid():
    """Generates a binary(16) UUID."""
    return uuid.uuid4().bytes

def random_date(start, end):
    """Generates a random datetime between two dates."""
    return start + timedelta(
        seconds=uniform(0, (end - start).total_seconds())
    )

def insert_data(connection, data):
    """Inserts data into the database."""
    try:
        cursor = connection.cursor()

        # SQL query for inserting data
        insert_query = """
        INSERT INTO operacoes_carteira (id, ativo_id, carteira_id, valor_investido, valor_acumulado, data_valorizacao, data_compra)
        VALUES (%s, %s, %s, %s, %s, %s, %s);
        """
        
        # Execute many queries in one batch
        cursor.executemany(insert_query, data)
        connection.commit()
        print(f"{cursor.rowcount} records inserted successfully.")

    except Error as e:
        print(f"Error: {e}")
        connection.rollback()

def generate_data():
    """Generates 100 rows of sample data."""
    data = []
    for _ in range(1000):
        id = generate_binary_uuid()
        ativo_id = uuid.UUID('80fe6c49-3711-db48-9a3b-4d6f79d39b3d').bytes
        carteira_id = uuid.UUID('6593af15-4a1c-1146-9bdd-5255aa45cc39').bytes
        valor_investido = round(uniform(1000, 10000), 2)
        valor_acumulado = round(uniform(5000, 15000), 2)

        # Random date generation between two ranges
        data_valorizacao = random_date(
            datetime(2022, 1, 1), datetime(2024, 12, 31)
        )
        data_compra = random_date(
            datetime(2020, 1, 1), datetime(2024, 12, 31)
        )

        # Append the tuple of values
        data.append(
            (
                id,
                ativo_id,
                carteira_id,
                valor_investido,
                valor_acumulado,
                data_valorizacao,
                data_compra,
            )
        )
    return data

def main():
    # Establishing the connection to the MySQL database
    try:
        connection = mysql.connector.connect(
            host='localhost',
            database='perainvest',
            user='perauser',
            password='perapass'
        )

        if connection.is_connected():
            print("Connected to the database")

            # Generate 100 rows of data
            data = generate_data()

            # Insert data into the table
            insert_data(connection, data)

    except Error as e:
        print(f"Error: {e}")
    finally:
        if connection.is_connected():
            connection.close()
            print("MySQL connection closed.")

if __name__ == "__main__":
    main()
