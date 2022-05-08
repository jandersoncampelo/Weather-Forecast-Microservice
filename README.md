# Weather-Forecast-Microservice
## Objetivos
Desenvolver uma solução Microservices que permite cadastrar e consultar previsão do tempo.  

![image](https://user-images.githubusercontent.com/6729346/167315861-89364c2b-41ed-4521-9715-b71906552b2f.png)

## Especificações
**Microservice1** - 
 - Responsável por expor API Rest para cadastrar previsão do tempo 
 - Deve produzir uma mensagem para o broker requisitando a operação de Consulta de Previsão  de Tempo. 
 - Deve escutar a resposta do broker com a previsão de tempo e armazenar em um Banco de  Dados 
**Microservice2** 
 - Responsável por integrar com API [Open Weather](http://openweathermap.org/api)
 - Deve se registrar no broker para escutar mensagens de Consulta de Previsão de Tempo 
 - Efetuar um HTTP para api do Open Weather, a reposta da API deve ser publicada no broker. 

![WeatherForecastMicroservice drawio](https://user-images.githubusercontent.com/6729346/167316019-87280017-cfc7-482d-8684-47d9d8ce57e5.png)

## TO-DO
[X] Criar Microsserviço que se comunicará com o Open Weather
[X] Fazer a comunicação entre APIs via RestSharp
[X] Criar o Microsserviço que vai persisir os dados no Banco
[X] Comunicar com o banco MongoDB
[X] Orquestrar Container com o Microsserviço e Banco
[ ] Fazer comunicação entre os Microsserviços via RabbitMQ
[ ] Adicionar Microsserviço 2 e o RabbitMQ ao Container
[ ] Refatorar

## Skills Necessárias
- Arquitetura de Microsserviços
- Orquestração de Containers com Docker
- Comunicação entre Microsservições
- Boas Práticas na Criação de Microsserviços
