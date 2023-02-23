create table c_sharp_ecommerce_usuario
(
    
    id_usu bigserial not null primary key,
    nome varchar not null,
    senha varchar not null,
    cpf varchar,
    email varchar,
    exc varchar(1) not null
);



insert into c_sharp_ecommerce_usuario (nome, senha, cpf, email, exc) values
('adm', 'adm123', '852.583.594-56', 'cesar@cleber.com', 'n');



create table c_sharp_ecommerce_produto
(
    id_prod bigserial not null primary key,
    nome varchar not null,
    descr varchar,
    val varchar,
    exc varchar(1) not null
);



insert into c_sharp_ecommerce_produto (nome, descr, val, exc) values
('Calabresa Premium', 'Calabresa, cream cheese, cebola, mussarela, manjericão e alho frito', '32,99', 'n'),
('Lombo Bacon', 'Mussarela, bacon, lombo e catupiry', '32,99', 'n'),
('Marguerita', 'Mussarela, manjericão e tomate', '32,99', 'n'),
('Milho com catupiry', 'Mussarela, milho, catupiry e tomate', '32,99', 'n'),
('Frango com Catupiry', 'Mussarela, frango e catupiry', '32,99', 'n');
