use aups;

insert into workplace(workplace_id, workplace_name)
values ('be90f70a-56a2-4aca-b2b1-354133ec82bd', 'Admin')
insert into workplace(workplace_id, workplace_name)
values ('77219056-df62-48d5-b023-cc4aa32fee73', 'Manager')
insert into workplace(workplace_id, workplace_name)
values ('b79f52eb-2fa0-4008-a6c5-da5855e72c1c', 'Radnik u proizvodnji')

insert into organizational_unit(organizational_unit_id, organizational_unit_name)
values ('7942c8af-bdd9-46b1-a1bd-defbdd249343', 'IT sektor')
insert into organizational_unit(organizational_unit_id, organizational_unit_name)
values ('fde43ec3-2aa3-4787-b010-c7ca616b0ec0', 'Proizvodnja kreveta')

insert into employee(employee_id, first_name, last_name, email, password, jmbg, phone_number, address, city, sallary, date_of_employment, workplace_id, organizational_unit_id)
values ('2072a1d7-0837-4217-b458-2480bd2677c6', 'Petar', 'Petrovic', 'petar@gmail.com', 'testtest', '1234567890123', '1234567893', 'Fruskogorska 1', 'Novi Sad', 130000, '2022-01-01', 'be90f70a-56a2-4aca-b2b1-354133ec82bd', '7942c8af-bdd9-46b1-a1bd-defbdd249343');
insert into employee(employee_id, first_name, last_name, email, password, jmbg, phone_number, address, city, sallary, date_of_employment, workplace_id, organizational_unit_id)
values ('f7a2ef81-03c9-49f4-aa48-7c6e3731d5d9', 'Marko', 'Markovic', 'marko@gmail.com', 'testtest', '1234567890124', '1234567894', 'Kralja Aleksandra 2', 'Beograd', 120000, '2022-02-02', '77219056-df62-48d5-b023-cc4aa32fee73', 'fde43ec3-2aa3-4787-b010-c7ca616b0ec0');
insert into employee(employee_id, first_name, last_name, email, password, jmbg, phone_number, address, city, sallary, date_of_employment, workplace_id, organizational_unit_id)
values ('02e71cc6-4cd8-4927-bae6-8063088653e2', 'Nikola', 'Nikolic', 'nikola@gmail.com', 'testtest', '1234567890125', '1234567895', 'Strazilovska 3', 'Novi Sad', 110000, '2022-03-03', '77219056-df62-48d5-b023-cc4aa32fee73', 'fde43ec3-2aa3-4787-b010-c7ca616b0ec0');

insert into warehouse(warehouse_id, address, city, capacity)
values ('876bbb02-6544-43c3-8c35-2052ce23e8c2', 'Bulevar Mihajla Pupina 12', 'Beograd', 1000)
insert into warehouse(warehouse_id, address, city, capacity)
values ('f30e8cd7-4bae-49ac-a964-528145d0daac', 'Bulevar Kralja Aleksandra', 'Zrenjanin', 2000)