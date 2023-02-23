CREATE TABLE fabricante(
    id_fabricante BIGSERIAL NOT NULL PRIMARY KEY,   -- campo identity  autoincremento
    nome VARCHAR(150) NOT NULL
   );

insert into fabricante (nome) values ('LG');
insert into fabricante (nome) values ('Apple');
insert into fabricante (nome) values ('Samsung');
insert into fabricante (nome) values ('Motorola');
insert into fabricante (nome) values ('Xiomi');
insert into fabricante (nome) values ('Huawei');

   
CREATE SEQUENCE aparelho_id_aparelho_seq   -- objeto sequence para gerar campo autoincremento
START 1
MINVALUE 1
MAXVALUE 9223372036854775807
CACHE 1;
   
 CREATE TABLE aparelho(
      id_aparelho 		INT8 NOT NULL DEFAULT NEXTVAL('aparelho_id_aparelho_seq'),   -- campo identity  autoincremento
      id_fabricante 		INT8 NOT NULL,
      modelo 			VARCHAR(80) NOT NULL,
      altura 			numeric(5,2) NOT NULL,
      largura 			numeric(5,2) NOT NULL,
      espessura			numeric(5,2) NOT NULL,
      peso 			numeric(5,2) NOT NULL,
      preco 			numeric(11,2) NOT NULL,
      quantidade 		int NOT NULL,
      desconto 			numeric(5,2) NOT NULL DEFAULT 0,
      
      CONSTRAINT aparelho_pkey PRIMARY KEY (id_aparelho),
      CONSTRAINT aparelho_id_fabricante_fkey
			FOREIGN KEY (id_fabricante) 
      		REFERENCES fabricante (id_fabricante)
);



INSERT INTO aparelho(
            id_fabricante, modelo, altura, largura, espessura, 
            peso, preco, quantidade, desconto)
    VALUES (2, 'iPhone 6c Roger White', 15.4, 7.6, 1.85, 
            585, 1999.99, 20, 5);

INSERT INTO aparelho(
            id_fabricante, modelo, altura, largura, espessura, 
            peso, preco, quantidade, desconto)
    VALUES (3, 'J5 Black White', 12.4, 6.6, 3.85, 
            785, 999.99, 15, 8);

INSERT INTO aparelho(
            id_fabricante, modelo, altura, largura, espessura, 
            peso, preco, quantidade, desconto)
    VALUES (4, 'Motorola Silver White', 19.4, 9.6, 8.85, 
            985, 899.99, 22, 15);
  
CREATE SEQUENCE pedido_id_pedido_seq
START 1
MINVALUE 1
MAXVALUE 9223372036854775807
CACHE 1;
   
 CREATE TABLE pedido(
      id_pedido 		INT8 NOT NULL DEFAULT NEXTVAL('pedido_id_pedido_seq'),
      id_aparelho	 	INT8 NOT NULL,
      datahorapedido	TIMESTAMP NOT NULL,    -- tipo data/hora
	observacao text null,
     
      CONSTRAINT pedido_pkey PRIMARY KEY (id_pedido),
      CONSTRAINT pedido_idaparelho_fkey
			FOREIGN KEY (id_aparelho) 
      		REFERENCES aparelho (id_aparelho)
);   


-- select  aparelho.id_aparelho, fabricante.nome, aparelho.modelo, aparelho.preco, aparelho.quantidade
	from aparelho inner join fabricante 
	   on aparelho.id_fabricante = fabricante.id_fabricante
order by fabricante.nome;