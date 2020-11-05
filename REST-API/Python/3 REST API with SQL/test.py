import sqlite3
# Setting SQL database
connection = sqlite3.connect('data.db')

cursor = connection.cursor()

# Create database column
create_table = "CREATE TABLE users (id int, username text, password text)"
cursor.execute(create_table)

# Adding data to database
user = (1, 'jose', 'asdf')
insert_query = "INSERT INTO users VALUES (?, ?, ?)"
cursor.execute(insert_query, user)

# Adding many users
users = [
    (2, 'rolf', 'asdf'),
    (3, 'anne', 'xyz')
]
cursor.executemany(insert_query, users)

# Getting information from database
select_query = "SELECT * FROM users"
for row in cursor.execute(select_query):
    print(row)

# Ending work with database
connection.commit()
connection.close()
