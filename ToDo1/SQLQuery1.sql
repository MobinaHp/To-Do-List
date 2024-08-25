create database ToDoApp;

INSERT INTO Lists (Name) VALUES ('Shopping'), ('Work'), ('Personal');

INSERT INTO Tasks (Name, DueDate, ListId, Starred, Checked)
VALUES ('Task Name 1', '2024-07-05', 1, 1, 1),
       ('Task Name 2', '2024-07-06', 2, 0, 0),
       ('Task Name 3', '2024-07-07', 1, 1, 1);

SELECT * FROM Tasks;
SELECT * FROM Lists;

