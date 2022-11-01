# OnTheFlyAPI

Aplicação com a finalidade de compartilhar diversas informações entre as APIs dentro da mesma solução.

Projeto controla as vendas de passagens áreas de um aeroporto denominado ON THE FLY. Os módulos são referentes aos controles de:

1-Passageiros

2-Companhia Aéreas

3-Voo Agendados

4-Venda de Passagens

API Passageiros:

O aeroporto atende apenas pessoas físicas, os passageiros menores de 18 anos podem ser cadastrados, mas não podem comprar nenhuma passagem.


API Companhias:

Somente serão aceitos cadastros de pessoas jurídicas. Companhias aéreas podem ser cadastradas e devem conter pelo menos uma aeronave ativa no sistema. No caso de cadastros sem nome fantasia, o mesmo deve ser definido igual a razão social.


API Endereços:

Realiza consultas na API do VIACEP, a partir do CEP, devendo ser informado apenas o número e complemento, caso haja. Em casos de CEP municipais, o campo de logradouro poderá ser inicialmente vazio.


API Aeronaves:

Os cadastros são realizados informando um CNPJ de uma Companhia Aérea que deve estar ativa, para ser cadastrada. As aeronaves poderão ser cadastradas mesmo havendo a companhia cadastrada com restrição.


API Voos:

Cadastros dos voos e passagens disponibilizados pelas companhias aéreas atendidas pelo aeroporto. São operados, depois de cadastrado, somente para cancelamento do voo. Os destinos possíveis deverão ser consultados por uma API, possibilitando somente voos nacionais. Nenhuma companhia poderá cadastrar voo quando tiver o cadastro de restrição positivo.

API Vendas:

Após o cadastro de voos, as passagens ficam disponíveis para serem vendidas de acordo com o máximo de assentos definida pela capacidade do avião. A venda não poderá constar o mesmo CPF na lista de passageiros. Cadastro das passagens reservadas ou vendidas de cada voo disponível. Nenhum passageiro na lista de vendas de passagem pode constar na lista de restritos. Caso aconteça a venda não poderá ser registrada.


