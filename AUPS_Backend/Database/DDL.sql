use aups;

if object_id('object_of_labor_technological_procedure', 'U') is not null
	drop table object_of_labor_technological_procedure;

if object_id('technological_procedure', 'U') is not null
	drop table technological_procedure;

if object_id('production_order', 'U') is not null
	drop table production_order;

if object_id('employee', 'U') is not null
	drop table employee;

if object_id('organizational_unit', 'U') is not null
	drop table organizational_unit;

if object_id('workplace', 'U') is not null
	drop table workplace;

if object_id('plant', 'U') is not null
	drop table plant;

if object_id('technological_system', 'U') is not null
	drop table technological_system;

if object_id('technological_procedure', 'U') is not null
	drop table technological_procedure;

if object_id('production_plan', 'U') is not null
	drop table production_plan;

if object_id('object_of_labor_material', 'U') is not null
	drop table object_of_labor_material;

if object_id('material', 'U') is not null
	drop table material;

if object_id('object_of_labor', 'U') is not null
	drop table object_of_labor;

if object_id('warehouse', 'U') is not null
	drop table warehouse;

create table workplace (
	workplace_id uniqueidentifier not null primary key,
	workplace_name nvarchar(100) not null unique
);

create table organizational_unit (
	organizational_unit_id uniqueidentifier not null primary key,
	organizational_unit_name nvarchar(100) not null unique
);

create table employee (
	employee_id uniqueidentifier not null primary key,
	first_name nvarchar(50) not null,
	last_name nvarchar(50) not null,
	email nvarchar(100) not null unique,
	jmbg nvarchar(13) not null unique,
	phone_number nvarchar(15) not null,
	address nvarchar(100) not null,
	city nvarchar(50) not null,
	sallary numeric(10, 2) not null,
	date_of_employment date not null,
	workplace_id uniqueidentifier not null foreign key references workplace(workplace_id) on delete no action,
	organizational_unit_id uniqueidentifier not null foreign key references organizational_unit(organizational_unit_id) on delete no action
);

create table warehouse (
	warehouse_id uniqueidentifier not null primary key,
	address nvarchar(100) not null,
	city nvarchar(50) not null,
	capacity int not null
);

create table object_of_labor (
	object_of_labor_id uniqueidentifier not null primary key,
	object_of_labor_name nvarchar(100) not null unique,
	description nvarchar(max) not null,
	price numeric(10, 2) not null,
	stock_quantity int not null,
	warehouse_id uniqueidentifier not null foreign key references warehouse(warehouse_id) on delete cascade
);

create table production_order (
	production_order_id uniqueidentifier not null primary key,
	start_date date not null,
	end_date date not null,
	quantity int not null,
	note nvarchar(max) not null,
	current_technological_procedure int not null,
	current_technological_procedure_executed bit not null,
	employee_id uniqueidentifier not null foreign key references employee(employee_id) on delete cascade,
	object_of_labor_id uniqueidentifier not null foreign key references object_of_labor(object_of_labor_id) on delete cascade
);

create table production_plan (
	production_plan_id uniqueidentifier not null primary key,
	production_plan_name nvarchar(100) not null,
	description nvarchar(max) not null,
	object_of_labor_id uniqueidentifier not null foreign key references object_of_labor(object_of_labor_id) on delete cascade
);

create table plant (
	plant_id uniqueidentifier not null primary key,
	plant_name nvarchar(100) not null unique
);

create table technological_system (
	technological_system_id uniqueidentifier not null primary key,
	technological_system_name nvarchar(100) not null unique
);

create table technological_procedure (
	technological_procedure_id uniqueidentifier not null primary key,
	technological_procedure_name nvarchar(100) not null unique,
	duration int not null,
	organizational_unit_id uniqueidentifier not null foreign key references organizational_unit(organizational_unit_id) on delete cascade,
	plant_id uniqueidentifier not null foreign key references plant(plant_id) on delete cascade,
	technological_system_id uniqueidentifier not null foreign key references technological_system(technological_system_id) on delete cascade
);

create table object_of_labor_technological_procedure (
	object_of_labor_technological_procedure_id uniqueidentifier not null primary key,
	order_of_execution int not null,
	object_of_labor_id uniqueidentifier not null foreign key references object_of_labor(object_of_labor_id) on delete cascade,
	technological_procedure_id uniqueidentifier not null foreign key references technological_procedure(technological_procedure_id) on delete cascade
);

create table material (
	material_id uniqueidentifier not null primary key,
	material_name nvarchar(200) not null,
	stock_quantity int not null
);

create table object_of_labor_material (
	object_of_labor_material_id uniqueidentifier not null primary key,
	quantity int not null,
	object_of_labor_id uniqueidentifier not null foreign key references object_of_labor(object_of_labor_id) on delete cascade,
	material_id uniqueidentifier not null foreign key references material(material_id) on delete cascade,
);